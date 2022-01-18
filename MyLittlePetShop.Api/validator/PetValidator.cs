using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using MyLittlePetShop.Api.Models.constants;
using MyLittlePetShop.Api.Models.dto;

namespace MyLittlePetShop.Api.validator
{
    public class PetValidator : AbstractValidator<PetDto>
    {
        public PetValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage(Constants.PET_NAME_REQUIRED)
                .NotEmpty().WithMessage(Constants.PET_NAME_REQUIRED)
                .MinimumLength(3).WithMessage(Constants.PET_NAME_TOO_SMALL)
                .Matches(@"^[a-zA-Z\s']*$").WithMessage(Constants.PET_NAME_SPECIAL_CHAR);

            RuleFor(x => x.ChipId)
                .Matches(@"^[A-Za-z0-9-]*$").WithMessage(Constants.PET_CHIP_SPECIAL_CHAR);

            RuleFor(x => x.Type)
                .NotNull().WithMessage(Constants.PET_TYPE_REQUIRED)
                .NotEmpty().WithMessage(Constants.PET_TYPE_REQUIRED)
                .MinimumLength(3).WithMessage(Constants.PET_TYPE_TOO_SMALL)
                .Matches(@"^[a-zA-Z]*$").WithMessage(Constants.PET_TYPE_SPECIAL_CHAR);

            RuleFor(x => x.Breed)
                .MinimumLength(3).WithMessage(Constants.PET_BREED_TOO_SMALL)
                .Matches(@"^[a-zA-Z\s]*$").WithMessage(Constants.PET_BREED_SPECIAL_CHAR);

        }   
    }
}
