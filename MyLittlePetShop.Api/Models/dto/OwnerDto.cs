using System;
using System.Collections.Generic;


namespace MyLittlePetShop.Api.Models.dto
{

    public class OwnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ContactDto> Contacts { get; set; } = new List<ContactDto>();
        public List<PetDto> Pets { get; set; } = new List<PetDto>();
    }
}
