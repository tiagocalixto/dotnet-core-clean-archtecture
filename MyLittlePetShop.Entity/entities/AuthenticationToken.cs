using System;
namespace MyLittlePetShop.Entity.entities
{
    public class AuthenticationToken
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int AccessTokenExpiresIn { get; set; }
    }
}
