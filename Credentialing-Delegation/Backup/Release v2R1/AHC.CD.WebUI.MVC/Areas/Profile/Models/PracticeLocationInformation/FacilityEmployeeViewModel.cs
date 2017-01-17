using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class FacilityEmployeeViewModel
    {
        public int EmployeeID { get; set; }

        public int PracticeLocationDetailID { get; set; }

        #region Name

        [Display(Name = "Last Name *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string MiddleName { get; set; }

        [Display(Name = "First Name *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string FirstName { get; set; }

        #endregion

        #region Address
        [Display(Name = "Building")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string Building { get; set; }

        [Display(Name = "Street/P. O. Box No: ")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string Street { get; set; }

        [MaxLength(100, ErrorMessage = "Country name must be between 2 and 50 characters")]
        [MinLength(1, ErrorMessage = "Country name must be between 2 and 50 characters")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [Display(Name = "Country ")]
        public string Country { get; set; }

        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [MaxLength(100, ErrorMessage = "State must be between 1 and 100 characters")]
        [MinLength(1, ErrorMessage = "State must be between 1 and 100 characters")]
        [Display(Name = "State ")]
        public string State { get; set; }

        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [MaxLength(100, ErrorMessage = "County must be between 1 and 100 characters")]
        [MinLength(1, ErrorMessage = "County must be between 1 and 100 characters")]
        [Display(Name = "County")]
        public string County { get; set; }

        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [MaxLength(100, ErrorMessage = "City must be between 1 and 100 characters")]
        [MinLength(1, ErrorMessage = "City must be between 1 and 100 characters")]
        [Display(Name = "City ")]
        public string City { get; set; }

        [Display(Name = "Zip Code")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_NUMBER_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_NUMBERS_HYPHEN)]
        [MaxLength(10, ErrorMessage = "ZipCode must be between 5 and 10 characters.")]
        [MinLength(5, ErrorMessage = "ZipCode must be between 5 and 10 characters.")]
        public string ZipCode { get; set; }

        [Display(Name = "P.O Box Address")]
        public string POBoxAddress { get; set; }

        #endregion

        #region Telephone

        [Display(Name = "Telephone")]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = "Please enter only numbers in Telephone number. ")]
        [MaxLength(10, ErrorMessage = "Telephone number must be of 10 digits")]
        [MinLength(10, ErrorMessage = "Telephone number must be of 10 digits")]
        public string Telephone { get; set; }

        public string CountryCodeTelephone { get; set; }

        #endregion

        #region Fax

        [Display(Name = "Fax")]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = "Please enter only numbers in Fax number. ")]
        [MaxLength(10, ErrorMessage = "Fax number must be of 10 digits")]
        [MinLength(10, ErrorMessage = "Fax number must be of 10 digits")]
        public string Fax { get; set; }

        public string CountryCodeFax { get; set; }

        #endregion

        #region Email

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$", ErrorMessage = ValidationErrorMessage.INVALID_EMAIL_FORMAT)]
        [Display(Name = "E-Mail Address ")]
        public string EmailAddress { get; set; }

        #endregion

        public ICollection<EmployeeDepartmentViwModel> Departments { get; set; }

        public ICollection<EmployeeDesignationViewModel> Designations { get; set; }
    }
}
