using System;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.UseCase.owner.persist.interfaces
{
    public interface IUpdateOwnerUseCase
    {
        Owner Execute(Owner owner);
    }
}
