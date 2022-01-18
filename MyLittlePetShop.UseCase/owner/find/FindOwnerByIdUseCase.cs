using System;
using System.Collections.Generic;
using MyLittlePetShop.Entity.entities;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.UseCase.owner.find.interfaces;

namespace MyLittlePetShop.UseCase.owner.find
{
    public class FindOwnerByIdUseCase : IFindOwnerByIdUseCase
    {
        private readonly IOwnerRepository _repository;

        public FindOwnerByIdUseCase(IOwnerRepository repository)
        {
            this._repository = repository;
        }


        public Owner Execute(int id)
        {
            Owner owner = this._repository.FindById(id);

            if (owner is null)
            {
                throw new KeyNotFoundException("Owner not found for id " + id);
            }

            return owner;
        }
    }
}
