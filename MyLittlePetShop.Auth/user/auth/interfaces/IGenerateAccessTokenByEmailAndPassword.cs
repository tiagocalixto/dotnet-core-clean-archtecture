using System;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.Auth.user.auth.interfaces
{
    public interface IGenerateAccessTokenByEmailAndPassword
    {
        AuthenticationToken Execute(string email, string password);
    }
}
