using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Castle.Core.Internal;
using FluentValidation;
using MyLittlePetShop.Api.Models.constants;
using MyLittlePetShop.Api.Models.dto;

namespace MyLittlePetShop.Api.validator
{
    public class OwnerValidator : AbstractValidator<OwnerDto>
    {
        public OwnerValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(Constants.OWNER_NAME_REQUIRED)
                .Must(NameValidate).WithMessage(Constants.OWNER_LASTNAME_REQUIRED)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage(Constants.OWNER_NAME_SPECIAL_CHAR);

            //CONTACTS VALIDATION -> Child object for owner
            RuleFor(x => x.Contacts)
                .Must(collection => !collection.IsNullOrEmpty())
                .WithMessage(Constants.CONTACT_REQUIRED);

            RuleFor(x => x.Contacts)
                .Must(ContactDuplicated)
                .WithMessage(Constants.CONTACT_DUPLICATED);

            RuleForEach(x => x.Contacts).SetValidator(new ContactValidator());

            //PETS VALIDATION -> Child object for owner
            RuleFor(x => x.Pets)
                .Must(collection => !collection.IsNullOrEmpty())
                .WithMessage(Constants.PET_REQUIRED);

            RuleFor(x => x.Pets)
                .Must(PetChipIdDuplicated)
                .WithMessage(Constants.PET_CHIP_DUPLICATED);

            RuleFor(x => x.Pets)
                .Must(PetDuplicated)
                .WithMessage(Constants.PET_DUPLICATED);

            RuleForEach(x => x.Pets).SetValidator(new PetValidator());
        }


        private bool NameValidate(string name)
        {
            return name.Trim().Split(" ").Count() > 1;
        }

        private bool ContactDuplicated(List<ContactDto> contacts)
        {
            if (contacts.IsNullOrEmpty())
                return true;

            HashSet<String> setContacts = contacts
                .Select(item => item.Type.ToUpper().Trim() +
                                item.Value.ToUpper().Trim())
                .ToHashSet();

            return setContacts.Count() == contacts.Count();
        }

        private bool PetChipIdDuplicated(List<PetDto> pets)
        {
            if (pets.IsNullOrEmpty())
                return true;

            HashSet<String> setPets = pets
                .Where(i => i.ChipId != null && i.ChipId.Trim() != "")
                .Select(item => item.ChipId.ToUpper().Trim())
                .ToHashSet();

            return setPets.Count() == pets
                .Where(i => i.ChipId != null && i.ChipId.Trim() != "")
                .Count();
        }

        private bool PetDuplicated(List<PetDto> pets)
        {
            if (pets.IsNullOrEmpty())
                return true;

            HashSet<String> setPets = pets
                .Select(item => item.Name.ToUpper().Trim() + item.Type.ToUpper().Trim() +
                                (item.Breed is null ? "": item.Breed.ToUpper().Trim()))
                .ToHashSet();

            return setPets.Count() == pets
                .Count();
        }
    }
}
