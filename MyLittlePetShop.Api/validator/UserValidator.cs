using FluentValidation;
using MyLittlePetShop.Api.Models.constants;
using MyLittlePetShop.Api.Models.dto;

namespace MyLittlePetShop.Api.validator
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        
        public UserValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage(Constants.EMAIL_REQUIRED)
                .NotEmpty().WithMessage(Constants.EMAIL_REQUIRED)
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                    .WithMessage(Constants.EMAIL_INVALID);

            RuleFor(x => x.Password)
                .NotNull().WithMessage(Constants.PASSWORD_REQUIRED)
                .NotEmpty().WithMessage(Constants.PASSWORD_REQUIRED)
                .MinimumLength(8).WithMessage(Constants.PASSWORD_INVALID_SIZE)
                .MaximumLength(40).WithMessage(Constants.PASSWORD_INVALID_SIZE)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$!%*?&])")
                    .WithMessage(Constants.PASSWORD_INVALID_FORMAT);
        }
    }
}