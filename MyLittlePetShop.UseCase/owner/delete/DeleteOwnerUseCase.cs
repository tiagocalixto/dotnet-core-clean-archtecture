using System;
using System.Collections.Generic;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.UseCase.owner.delete.interfaces;

namespace MyLittlePetShop.UseCase.owner.delete
{
    public class DeleteOwnerUseCase : IDeleteOwnerUseCase
    {

        private readonly IOwnerRepository _repository;

        public DeleteOwnerUseCase(IOwnerRepository repository)
        {
            this._repository = repository;
        }


        public void Execute(int idOwner)
        {
            if (!this._repository.ExistsById(idOwner))
            {
                throw new KeyNotFoundException("Owner not found for id " + idOwner.ToString());
            }

            this._repository.DeleteById(idOwner);
        }
    }
}
