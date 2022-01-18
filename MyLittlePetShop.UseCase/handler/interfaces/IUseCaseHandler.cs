using System;
using System.Collections.Generic;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.UseCase.handler.interfaces
{
    public interface IUseCaseHandler
    {
        void DeleteOwnerById(int id);
        Owner FindOwnerByContact(string contactType, string contactValue);
        Owner FindOwnerById(int id);
        List<Owner> FindOwnerByName(string name, int pageSize, int page);
        Owner SaveOwner(Owner owner);
        Owner UpdateOwner(Owner owner);
    }
}
