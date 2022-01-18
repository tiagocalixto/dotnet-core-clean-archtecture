using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLittlePetShop.Api.mapper;
using MyLittlePetShop.Api.Models.dto;
using MyLittlePetShop.Api.validator.filter;
using MyLittlePetShop.Auth.handler.interfaces;
using MyLittlePetShop.Entity.entities;


namespace MyLittlePetShop.Api.Controllers
{
    [ValidateModelStateAttribute]
    public class AuthController :  Controller
    {
        private readonly IAuthHandler _handler;

        public AuthController(IAuthHandler handler)
        {
            _handler = handler;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("v1/auth/createUser")]
        public ActionResult CreateUser([FromBody] UserDto user)
        {
            _handler.CreateUser(UserDtoMapper.ConvertDtoToEntity(user, "USER"));
            return Created("", "");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("v1/auth/authenticate")]
        public ActionResult<AuthenticationTokenDto> Authenticate([FromBody] UserDto user)
        {
            var result = _handler.GenerateAccessTokenByEmailAndPassword(user.Email, user.Password);
            return Ok(TokenDtoMapper.ConvertEntityToDto(result));
        }
    }
}