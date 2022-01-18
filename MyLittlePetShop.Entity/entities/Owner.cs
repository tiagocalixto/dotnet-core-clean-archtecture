using System;
using System.Collections.Generic;

namespace MyLittlePetShop.Entity.entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<Pet> Pets { get; set; } = new List<Pet>();
    }
}
