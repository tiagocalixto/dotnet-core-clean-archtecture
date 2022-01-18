using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.Auth.handler.interfaces
{
    public interface IAuthHandler
    {
        AuthenticationToken GenerateAccessTokenByEmailAndPassword(string email, string password);
        void CreateUser(User user);
    }
}