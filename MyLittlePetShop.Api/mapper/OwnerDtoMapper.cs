using System.Collections.Generic;
using System.Linq;
using MyLittlePetShop.Api.Models.dto;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.Api.mapper
{
    public static class OwnerDtoMapper
    {

        public static Owner ConvertDtoToEntity(OwnerDto dto)
        {
            if (dto is null)
            {
                return null;
            }

            return new Owner()
            {
                Id = dto.Id,
                Name = dto.Name,
                Contacts = dto.Contacts
                            .Select(i => ConvertContactDtoToContact(i))
                            .ToList(),
                Pets = dto.Pets.Select(i => ConvertPetDtoToPet(i))
                .ToList()
            };
        }

        public static List<Owner> ConvertDtoToEntity(List<OwnerDto> dto)
        {
            if (dto is null || dto.Count == 0)
            {
                return new List<Owner>();
            }

            return dto.Select(i => ConvertDtoToEntity(i))
                .ToList();
        }

        public static OwnerDto ConvertEntityToDto(Owner owner)
        {
            if (owner is null)
            {
                return null;
            }

            OwnerDto dto = new OwnerDto()
            {
                Id = owner.Id,
                Name = owner.Name,
                Contacts = owner.Contacts
                            .Select(i => ConvertContacEntitytToContactDto(i))
                            .ToList(),
                Pets = owner.Pets
                        .Select(i => ConvertPetEntityToPetDto(i))
                        .ToList()
            };

            return dto;
        }

        public static List<OwnerDto> ConvertEntityToDto(List<Owner> owner)
        {
            if (owner is null || owner.Count == 0)
            {
                return new List<OwnerDto>();
            }

            return owner.Select(i => ConvertEntityToDto(i))
                .ToList();
        }

        public static PageableDto<List<OwnerDto>> ConvertEntityToPageableDto(List<Owner> owner, int page, int pageSize)
        {
            List<OwnerDto> ownerList;

            if (owner is null || owner.Count == 0)
            {
                ownerList = new List<OwnerDto>();
            }
            else
            {
                ownerList = owner.Select(i => ConvertEntityToDto(i))
                .ToList();
            }

            return new PageableDto<List<OwnerDto>>()
            {
                Data = ownerList,
                Page = page,
                PageSize = pageSize
            };
        }

        private static Contact ConvertContactDtoToContact(ContactDto dto)
        {
            return new Contact()
            {
                Id = dto.Id,
                Type = dto.Type,
                Value = dto.Value
            };
        }

        private static Pet ConvertPetDtoToPet(PetDto dto)
        {
            return new Pet()
            {
                Id = dto.Id,
                Name = dto.Name,
                Type = dto.Type,
                Breed = dto.Breed,
                ChipId = dto.ChipId
            };
        }

        private static ContactDto ConvertContacEntitytToContactDto(Contact contact)
        {
            return new ContactDto()
            {
                Id = contact.Id,
                Value = contact.Value,
                Type = contact.Type
            };
        }

        private static PetDto ConvertPetEntityToPetDto(Pet pet)
        {
            return new PetDto()
            {
                Id = pet.Id,
                Name = pet.Name,
                Breed = pet.Breed,
                Type = pet.Type,
                ChipId = pet.ChipId
            };
        }

    }

}
