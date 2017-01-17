using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class BusinessOfficeContactPersonViewModel
    {
        public int BusinessOfficeContactPersonID { get; set; }

        public int PracticeLocationDetailID { get; set; }
       
        [Display(Name = "First Name *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string MiddleName { get; set; }

       
        [Display(Name = "Last Name *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string LastName { get; set; }

        [Display(Name = "Telephone")]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = ValidationErrorMessage.PHONE_FAX_NUMBER)]
        public string Telephone { get; set; }

        public string PhoneCountryCode { get; set; }

        [Display(Name = "Fax")]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        public string Fax { get; set; }

        public string FaxCountryCode { get; set; }

        [Display(Name = "E-Mail Address")]
        [RegularExpression(@"^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$", ErrorMessage = ValidationErrorMessage.INVALID_EMAIL_FORMAT)]
        public string EmailID { get; set; }
    }
}