using System;
using System.Collections.Generic;
using MyLittlePetShop.Entity.entities;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.UseCase.owner.find.interfaces;

namespace MyLittlePetShop.UseCase.owner.find
{
    public class FindOwnerByNameUseCase : IFindOwnerByNameUseCase
    {
        private readonly IOwnerRepository _repository;

        public FindOwnerByNameUseCase(IOwnerRepository repository)
        {
            this._repository = repository;
        }


        public List<Owner> Execute(string name, int pageSize, int page)
        {
            List<Owner> owners = this._repository.FindByName(name, pageSize, page);

            return owners;
        }
    }
}
