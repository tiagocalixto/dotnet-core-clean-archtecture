using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyLittlePetShop.Auth.user.auth.interfaces;
using MyLittlePetShop.Entity.entities;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.Util;

namespace MyLittlePetShop.Auth.user.auth
{
    public class GenerateAccessTokenByEmailAndPassword : IGenerateAccessTokenByEmailAndPassword
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        
        public GenerateAccessTokenByEmailAndPassword(IUserRepository repository, 
            IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public AuthenticationToken Execute(string email, string password)
        {
            User user = this._repository.FindByEmail(email);

            if (user is null)
                throw new KeyNotFoundException("Invalid password or Email!");

            password = password.Trim();
            password = Cryptography.HashStringMd5MultipleTimes(password, password.Length);
            
            if (!Cryptography.Verify(password, user.Password))
                throw new UnauthorizedAccessException("Invalid password or Email!");

            return GenerateToken(user);
        }

        private AuthenticationToken GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSecretKey"]);
            var tokenExpires = Int32.Parse(_configuration["JwtExpiresSeconds"]);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                
                Expires = DateTime.UtcNow.AddSeconds(tokenExpires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return new AuthenticationToken()
            {
                AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
                AccessTokenExpiresIn = tokenExpires,
                TokenType = "Bearer"
            };
        }
    }
}
