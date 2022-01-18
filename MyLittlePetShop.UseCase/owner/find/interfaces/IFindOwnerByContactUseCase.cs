using System;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.UseCase.owner.find.interfaces
{
    public interface IFindOwnerByContactUseCase
    {
        Owner Execute(string contactType, string contactValue);
    }
}
