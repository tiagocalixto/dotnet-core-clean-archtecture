using System;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.UseCase.owner.find.interfaces
{
    public interface IFindOwnerByIdUseCase
    {
        Owner Execute(int id);
    }
}
