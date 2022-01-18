using System;
using System.Collections.Generic;
using System.Linq;
using MyLittlePetShop.DataProvider.models;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.DataProvider.mapper
{
    public static class OwnerDbMapper
    {

        public static Owner ConvertDbToEntity(OwnerDb db)
        {
            if (db is null)
            {
                return null;
            }

            return new Owner()
            {
                Id = db.OwnerId,
                Name = db.Name,
                Contacts = db.Contacts
                            .Select(i => ConvertContactDbToContact(i))
                            .ToList(),
                Pets = db.Pets.Select(i => ConvertPetDbToPet(i))
                .ToList()
            };

        }

        public static List<Owner> ConvertDbToEntity(List<OwnerDb> db)
        {
            if (db is null || db.Count == 0)
            {
                return new List<Owner>();
            }

            return db.Select(i => ConvertDbToEntity(i))
                .ToList();
        }

        public static OwnerDb ConvertEntityToDb(Owner owner)
        {
            if (owner is null)
            {
                return null;
            }

            OwnerDb db = new OwnerDb()
            {
                OwnerId = owner.Id,
                Name = owner.Name,
                Contacts = owner.Contacts
                            .Select(i => ConvertContacEntitytToContactDb(i))
                            .ToList(),
                Pets = owner.Pets
                        .Select(i => ConvertPetEntityToPetDb(i))
                        .ToList()
            };

            db.Contacts
                .Select(i => { i.OwnerId = db.OwnerId; i.Owner = db; return i; })
                .ToList();

            db.Pets
                .Select(i => { i.OwnerId = db.OwnerId; i.Owner = db; return i; })
                .ToList();

            return db;
        }

        public static List<OwnerDb> ConvertEntityToDb(List<Owner> owner)
        {
            if (owner is null || owner.Count == 0)
            {
                return new List<OwnerDb>();
            }

            return owner.Select(i => ConvertEntityToDb(i))
                .ToList();
        }

        private static Contact ConvertContactDbToContact(ContactDb db)
        {
            return new Contact()
            {
                Id = db.ContactId,
                Type = db.Type,
                Value = db.Value
            };
        }

        private static Pet ConvertPetDbToPet(PetDb db)
        {
            return new Pet()
            {
                Id = db.PetId,
                Name = db.Name,
                Type = db.Type,
                Breed = db.Breed,
                ChipId = db.ChipId
            };
        }

        private static ContactDb ConvertContacEntitytToContactDb(Contact contact)
        {
            return new ContactDb()
            {
                ContactId = contact.Id,
                Value = contact.Value,
                Type = contact.Type
            };
        }

        private static PetDb ConvertPetEntityToPetDb(Pet pet)
        {
            return new PetDb()
            {
                PetId = pet.Id,
                Name = pet.Name,
                Breed = pet.Breed,
                Type = pet.Type,
                ChipId = pet.ChipId
            };
        }
    }
}
