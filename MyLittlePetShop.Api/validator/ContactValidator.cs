using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentValidation;
using MyLittlePetShop.Api.Models.constants;
using MyLittlePetShop.Api.Models.dto;

namespace MyLittlePetShop.Api.validator
{
    public class ContactValidator : AbstractValidator<ContactDto>
    {
        private string _messageContactValue = "";

        public ContactValidator()
        {
            RuleFor(x => x.Type)
                .NotNull().WithMessage(Constants.CONTACT_TYPE_REQUIRED)
                .NotEmpty().WithMessage(Constants.CONTACT_TYPE_REQUIRED)
                .Must(ValidateContactType).WithMessage(Constants.CONTACT_TYPE_NOT_IN_LIST);

            RuleFor(x => x.Value)
                .NotNull().WithMessage(Constants.CONTACT_VALUE_REQUIRED)
                .NotEmpty().WithMessage(Constants.CONTACT_VALUE_REQUIRED)
                .Custom(ValidateContactValue);
        }

        private bool ValidateContactType(string contactType)
        {
            List<string> listTypes = CreateContactTypeList();

            return listTypes.Any(i => i.Contains(contactType.ToLower().Trim()));
        }

        private List<string> CreateContactTypeList()
        {
            List<string> listTypes = new List<string>();
            listTypes.AddRange(CreateMobileContactTypeList());
            listTypes.AddRange(CreatePhoneContactTypeList());
            listTypes.Add("email");

            return listTypes;
        }

        private List<string> CreateMobileContactTypeList()
        {
            List<string> listMobileTypes = new List<string>();
            listMobileTypes.Add("mobile");
            listMobileTypes.Add("cellphone");
            listMobileTypes.Add("celular");
            listMobileTypes.Add("whatsapp");
            listMobileTypes.Add("whats");

            return listMobileTypes;
        }

        private List<string> CreatePhoneContactTypeList()
        {
            List<string> listPhoneTypes = new List<string>();
            listPhoneTypes.Add("phone");
            listPhoneTypes.Add("fixo");

            return listPhoneTypes;
        }

        private void ValidateContactValue(string value, ValidationContext<ContactDto> context)
        {
            if (context.InstanceToValidate.Type.Trim().ToLower() == "email")
                ValidateEmail(value, context);

            if (CreateMobileContactTypeList().Any(i =>
                    i.Contains(context.InstanceToValidate.Type.Trim().ToLower())))
                ValidateMobile(value, context);

            if (CreatePhoneContactTypeList().Any(i =>
                    i.Contains(context.InstanceToValidate.Type.Trim().ToLower())))
                ValidatePhone(value, context);
        }

        private void ValidateEmail(string email, ValidationContext<ContactDto> context)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (!regex.Match(email.Trim().ToLower()).Success)
                context.AddFailure(Constants.EMAIL_INVALID_VALUE + " - " +  email.Trim().ToLower());
        }

        private void ValidateMobile(string mobile, ValidationContext<ContactDto> context)
        {
            if (ValidateSpecialCharPhoneNumber(mobile, context.InstanceToValidate))
            {
                if (!ValidatePhoneSize(11, 13, mobile, context.InstanceToValidate))
                    context.AddFailure(this._messageContactValue);
            } else
            {
                context.AddFailure(this._messageContactValue);
            }
        }

        private void ValidatePhone(string phone, ValidationContext<ContactDto> context)
        {
            if (!ValidateSpecialCharPhoneNumber(phone, context.InstanceToValidate))
                context.AddFailure(this._messageContactValue);

            if (!ValidatePhoneSize(10, 12, phone, context.InstanceToValidate))
                context.AddFailure(this._messageContactValue);
        }

        private bool ValidateSpecialCharPhoneNumber(String phone, ContactDto contact)
        {
            Regex regex = new Regex(@"^[0-9]*$");

            if (!regex.Match(FormatPhoneNumber(phone)).Success)
            {
                _messageContactValue = contact.Type.ToUpper() + " contains letters or special chars! " +
                    "only numbers and following chars are accepted " +
                    "['(', ')', '-', '+'] -  invalid value: " + phone;

                return false;
            }

            return true;
        }

        private string FormatPhoneNumber(String phoneNumber)
        {
            return phoneNumber.Replace(" ", "").Replace("(", "").Replace(")", "")
                .Replace("-", "").Replace("+", "").Trim();
        }

        private bool ValidatePhoneSize(int beginSize, int endSize, String phone, ContactDto contact)
        {
            if (FormatPhoneNumber(phone).Length < beginSize)
            {
                _messageContactValue = "DDD is required for " + contact.Type.ToUpper() + 
                    "! - invalid value: " + phone;

                return false;
            }

            if (FormatPhoneNumber(phone).Length > endSize)
            {
                _messageContactValue = contact.Type.ToUpper() +
                    "is too bigger! - invalid value: " + phone;

                return false;
            }
            
            return true;
        }
    }
}
