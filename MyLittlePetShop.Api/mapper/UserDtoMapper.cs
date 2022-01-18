using System.Collections.Generic;
using System.Linq;
using MyLittlePetShop.Api.Models.dto;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.Api.mapper
{
    public static class UserDtoMapper
    {

        public static User ConvertDtoToEntity(UserDto dto, string role)
        {
            if (dto is null)
                return null;
            
            return new User()
            {
                Id = 0,
                Email = dto.Email,
                Password = dto.Password,
                Role = role
            };
        }
    }

}
