using System;
namespace MyLittlePetShop.Api.Models.constants
{
    public class Constants
    {
        //OWNER VALIDATION MESSAGES
        public const string OWNER_NAME_REQUIRED = "Owner name and Lastname is required!";
        public const string OWNER_LASTNAME_REQUIRED = "Owner Lastname is required!";
        public const string OWNER_NAME_SPECIAL_CHAR = "Special Char detected in OWNER name! Use only letters(a-z A-Z)";
        public const string OWNER_ID_INVALID_RANGE = "Owner id must be greater than 0";

        //CONTACT VALIDATION MESSAGES
        public const string CONTACT_REQUIRED = "At Least One Contact is Required";
        public const string CONTACT_DUPLICATED = "Duplicated contact(same type and value) detected!";
        public const string CONTACT_TYPE_NOT_IN_LIST = "Incorrect contact type detected, contact type " +
            "must be within one of the following values: [mobile, cellphone, celular, phone, fixo, whatsapp, whats, email]";
        public const string CONTACT_TYPE_REQUIRED = "Contact type is required!";
        public const string CONTACT_VALUE_REQUIRED = "Contact value is required!";
        public const string EMAIL_INVALID_VALUE = "Email is invalid! invalid value: ";

        //PET VALIDATION MESSAGES
        public const string PET_REQUIRED = "At Least One Pet is Required";
        public const string PET_CHIP_DUPLICATED = "Duplicated pet chip id detect!";
        public const string PET_DUPLICATED = "Duplicated Pet(Name, Type and Breed) detected!";
        public const string PET_NAME_REQUIRED = "Pet name is required!";
        public const string PET_NAME_TOO_SMALL = "Pet name is to short! Required at least 3 characters";
        public const string PET_NAME_SPECIAL_CHAR = "Special Char detected in PET name! Use only letters(a-z A-Z)";
        public const string PET_CHIP_SPECIAL_CHAR = "Special Char detect in pet chip id! Use only letters(a-z A-Z), numbers(0-9) or hyphen char(-)";
        public const string PET_TYPE_REQUIRED = "Pet Type is required!";
        public const string PET_TYPE_TOO_SMALL = "Pet type is to short! Required at least 3 characters";
        public const string PET_TYPE_SPECIAL_CHAR = "Special Char detected in PET type! Use only letters(a-z A-Z)";
        public const string PET_BREED_TOO_SMALL = "Pet breed is to short! Required at least 3 characters";
        public const string PET_BREED_SPECIAL_CHAR = "Special Char detected in PET breed! Use only letters(a-z A-Z)";

        //USER MESSAGES
        public const string EMAIL_REQUIRED = "Email is Required!";
        public const string EMAIL_INVALID = "Invalid Email Format!";
        public const string PASSWORD_REQUIRED = "Password is Required!";
        public const string PASSWORD_INVALID_SIZE = "Invalid Password size! Size should be between 8 and 40 characters.";
        public const string PASSWORD_INVALID_FORMAT = "Password invalid! Password must have uppercase letter, lowercase letter " +
                                                      "number and special char";

        //OTHER MESSAGES
        public const string PAGE_INVALID_RANGE = "Page starts at 1!";
        public const string PAGE_REQUIRED = "Page is required!";
        public const string PAGE_SIZE_REQUIRED = "Page size is required!";
        public const string PAGE_SIZE_INVALID_RANGE = "Page size starts at 1!";
    }
}
