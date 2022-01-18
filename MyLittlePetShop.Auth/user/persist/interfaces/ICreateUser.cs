using System;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.Auth.user.persist.interfaces
{
    public interface ICreateUser
    {
        void Execute(User user);
    }
}
