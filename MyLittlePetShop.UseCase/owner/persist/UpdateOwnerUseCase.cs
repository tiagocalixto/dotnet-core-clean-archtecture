using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyLittlePetShop.Entity.entities;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.UseCase.owner.persist.interfaces;

namespace MyLittlePetShop.UseCase.owner.persist
{
    public class UpdateOwnerUseCase : IUpdateOwnerUseCase
    {
        private readonly IOwnerRepository _repository;

        public UpdateOwnerUseCase(IOwnerRepository repository)
        {
            this._repository = repository;
        }

        public Owner Execute(Owner owner)
        {

            if (!this._repository.ExistsById(owner.Id))
            {
                throw new KeyNotFoundException("Owner not found for id " +
                    owner.Id.ToString());
            }

            VerifyContactBelongsToOtherOwner(owner);
            VerifyPetBelongsToOtherOwner(owner);

            return this._repository.Update(owner);
        }

        private void VerifyContactBelongsToOtherOwner(Owner owner)
        {
            List<Owner> contactsExistents = owner.Contacts
                            .Where(i => i != null)
                            .Select(i => this._repository.FindByContact(i.Type, i.Value))
                            .ToList();

            List<Owner> ownersWithDifferentId = contactsExistents
                .Where(i => i != null &&  i.Id != owner.Id)
                .ToList();

            if (ownersWithDifferentId.Count > 0)
            {
                List<Contact> contactsForDifferentOwners = new List<Contact>();
                ownersWithDifferentId.ForEach(i => contactsForDifferentOwners.AddRange(i.Contacts));

                throw new DataException("follow contacts already exists for another owner: [ " +
                    contactsForDifferentOwners.ToString() + "]");
            }

        }

        private void VerifyPetBelongsToOtherOwner(Owner owner)
        {
            List<Owner> petsExisntes = owner.Pets
                            .Where(i => i != null)
                            .Select(i => this._repository.FindByPetChip(i.ChipId))
                            .ToList();

            List<Owner> ownersWithDifferentId = petsExisntes
                .Where(i => i != null && i.Id != owner.Id)
                .ToList();

            if (ownersWithDifferentId.Count > 0)
            {
                List<Pet> petsForDifferentOwners = new List<Pet>();
                ownersWithDifferentId.ForEach(i => petsForDifferentOwners.AddRange(i.Pets));

                throw new DataException("follow pets already exists for another owner: [ " +
                    petsForDifferentOwners.ToString() + "]");
            }

        }
    }
}
