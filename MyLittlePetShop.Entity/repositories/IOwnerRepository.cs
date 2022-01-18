using System;
using System.Collections.Generic;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.Entity.repositories
{
    public interface IOwnerRepository
    {
        Owner FindById(int id);
        List<Owner> FindByName(string name, int pageSize, int page);
        Owner FindByContact(string contactType, string contactValue);
        Owner FindByPetChip(string chipId);
        Owner Save(Owner owner);
        Owner Update(Owner owner);
        void DeleteById(int id);
        bool ExistsById(int id);
    }
}
