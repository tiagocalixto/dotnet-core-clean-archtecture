using System.Collections.Generic;
using System.Linq;
using MyLittlePetShop.Api.Models.dto;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.Api.mapper
{
    public static class TokenDtoMapper
    {

        public static AuthenticationTokenDto ConvertEntityToDto(AuthenticationToken entity)
        {
            if (entity is null)
                return null;
            
            return new AuthenticationTokenDto()
            {
                AccessToken = entity.AccessToken,
                TokenType = entity.TokenType,
                AccessTokenExpiresIn = entity.AccessTokenExpiresIn
            };
        }
    }
}
