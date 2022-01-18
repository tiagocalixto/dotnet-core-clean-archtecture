using System;


namespace MyLittlePetShop.Api.Models.dto
{
    public class PetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string ChipId { get; set; }
    }
}
