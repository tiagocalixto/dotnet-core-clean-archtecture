using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.Entity.repositories
{
    public interface IUserRepository
    {
        User FindByEmail(string email);
        User Save(User user);
        User Update(User user);
        void Delete(int id);
    }
}
