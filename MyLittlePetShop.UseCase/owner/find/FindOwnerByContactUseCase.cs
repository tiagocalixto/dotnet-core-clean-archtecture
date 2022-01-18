using System;
using System.Collections.Generic;
using MyLittlePetShop.Entity.entities;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.UseCase.owner.find.interfaces;

namespace MyLittlePetShop.UseCase.owner.find
{
    public class FindOwnerByContactUseCase : IFindOwnerByContactUseCase
    {
        private readonly IOwnerRepository _repository;

        public FindOwnerByContactUseCase(IOwnerRepository repository)
        {
            this._repository = repository;
        }


        public Owner Execute(string contactType, string contactValue)
        {
            Owner owner = this._repository.FindByContact(contactType, contactValue);

            if (owner is null)
            {
                throw new KeyNotFoundException("Owner not found for contact type " +
                    contactType + " and contact value " + contactValue);
            }

            return owner;
        }
    }
}
