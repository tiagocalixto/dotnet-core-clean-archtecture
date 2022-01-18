using System;
using System.Collections.Generic;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.UseCase.owner.find.interfaces
{
    public interface IFindOwnerByNameUseCase
    {
        List<Owner> Execute(string name, int pageSize, int page);
    }
}
