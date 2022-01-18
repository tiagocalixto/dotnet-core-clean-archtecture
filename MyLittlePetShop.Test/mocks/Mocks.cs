using System;
using System.Collections.Generic;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.Test.mocks
{
    public static class Mocks
    {


        public static Owner CreateMockOwner(int id = 0, int petId = 0, int contactId = 0)
        {
            return new Owner()
            {
                Id = id,
                Name = "John Wick",
                Contacts = CreateMockContactList(contactId),
                Pets = CreateMockPetList(petId)
            };
        }

        public static List<Owner> CreateMockOwnerList(int id = 0, int petId = 0, int contactId = 0)
        {
            var list = new List<Owner>
            {
                CreateMockOwner(id, contactId, petId)
            };

            return list;
        }

        public static Contact CreateMockContact(int id = 0)
        {
            return new Contact()
            {
                Id = id,
                Type = "email",
                Value = "johnwick@criminalorganization.com"
            };
        }

        public static List<Contact> CreateMockContactList(int id = 0)
        {
            var list = new List<Contact>
            {
                CreateMockContact(id)
            };

            return list;
        }

        public static Pet CreateMockPet(int id = 0)
        {
            return new Pet()
            {
                Id = id,
                Type = "dog",
                Name = "no named",
                Breed = "Beagle",
                ChipId = "251248ABC39"
            };
        }

        public static List<Pet> CreateMockPetList(int id = 0)
        {
            var list = new List<Pet>
            {
                CreateMockPet(id)
            };

            return list;
        }
    }
}
