using System;
using System.Collections.Generic;
using System.Data;
using MyLittlePetShop.Entity.entities;
using MyLittlePetShop.Entity.repositories;
using MyLittlePetShop.UseCase.owner.persist.interfaces;

namespace MyLittlePetShop.UseCase.owner.persist
{
    public class SaveOwnerUseCase : ISaveOwnerUseCase
    {
        private readonly IOwnerRepository _repository;

        public SaveOwnerUseCase(IOwnerRepository repository)
        {
            this._repository = repository;
        }

        public Owner Execute(Owner owner)
        {
            ValidateContactsAlreadyExists(owner.Contacts);
            ValidatePetAlreadyExists(owner.Pets);
            return this._repository.Save(owner);
        }

        private void ValidateContactsAlreadyExists(List<Contact> contacts)
        {
            contacts.ForEach(i => ContactAlreadyExists(i));
        }

        private void ValidatePetAlreadyExists(List<Pet> pets)
        {
            pets.ForEach(i => PetIdAlreadyExists(i));
        }

        private void ContactAlreadyExists(Contact contact)
        {
            if (this._repository.FindByContact(contact.Type, contact.Value)
                != null)
            {
                throw new DataException("Already exits contact type " +
                    contact.Type + " and contact value " + contact.Value +
                    " for another owner");
            }
        }

        private void PetIdAlreadyExists(Pet pet)
        {
            if (this._repository.FindByPetChip(pet.ChipId)
                != null)
            {
                throw new DataException("Already exits pet with id = " +
                    pet.ChipId + " for another owner");
            }
        }
    }
}
