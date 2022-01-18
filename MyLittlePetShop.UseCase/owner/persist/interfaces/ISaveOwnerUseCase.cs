using System;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.UseCase.owner.persist.interfaces
{
    public interface ISaveOwnerUseCase
    {
        Owner Execute(Owner owner);
    }
}
