using MyLittlePetShop.DataProvider.models;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.DataProvider.mapper
{
    public static class UserDbMapper
    {
        public static User ConvertDbToEntity(UserDb db)
        {
            if (db is null)
                return null;

            return new User()
            {
                Id = db.Id,
                Email = db.Email,
                Password = db.Password,
                Role = db.Role
            };
        }
        
        public static UserDb ConvertEntityToDb(User entity)
        {
            if (entity is null)
                return null;

            return new UserDb()
            {
                Id = entity.Id,
                Email = entity.Email,
                Password = entity.Password,
                Role = entity.Role
            };
        }
    }
}