using System;
using System.Data;
using MyLittlePetShop.Auth.user.persist.interfaces;
using MyLittlePetShop.Entity.entities;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.Util;

namespace MyLittlePetShop.Auth.user.persist
{
    public class CreateUser : ICreateUser
    {
        private readonly IUserRepository _repository;
        
        public CreateUser(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Execute(User user)
        {
            if(_repository.FindByEmail(user.Email) != null)
                throw new DataException("Email already in use");

            user.Password = user.Password.Trim();
            user.Password = Cryptography.HashStringMd5MultipleTimes(user.Password, user.Password.Length);
            user.Password = Cryptography.HashString(user.Password);

            _repository.Save(user);
        }
    }
}
