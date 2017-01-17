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
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string MiddleName { get; set; }

        [Display(Name = "First Name *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string FirstName { get; set; }

        #endregion

        #region Address

        [Display(Name = "Building")]
        [MaxLength(50, ErrorMessage = "Suite/Building must be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "Suite/Building must be between 2 and 50 characters")]
        [RegularExpression("([a-zA-Z0-9+ ?|#|.,'-]+)", ErrorMessage = "Please enter valid Building.Only alpha-numerals, space, comma, hyphen ,hash accepted")]
        public string Building { get; set; }

       // [Required(ErrorMessage = "Please enter the Street")]
        [MaxLength(100, ErrorMessage = "Street must be between 1 and 100 characters")]
        [MinLength(1, ErrorMessage = "Street must be between 1 and 100 characters")]
        [RegularExpression("([a-zA-Z0-9+ ?|#|.,'-]+)", ErrorMessage = "Please enter valid Street.Only alpha-numerals, space, comma, hyphen ,hash accepted")]
        [Display(Name = "Street *")]
        public string Street { get; set; }

       // [Required(ErrorMessage = "Please enter the Country")]
        [MaxLength(100, ErrorMessage = "Country name must be between 2 and 50 characters")]
        [MinLength(1, ErrorMessage = "Country name must be between 2 and 50 characters")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [Display(Name = "Country *")]
        public string Country { get; set; }

       // [Required(ErrorMessage = "Please enter the State")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [MaxLength(100, ErrorMessage = "State must be between 1 and 100 characters")]
        [MinLength(1, ErrorMessage = "State must be between 1 and 100 characters")]
        [Display(Name = "State *")]
        public string State { get; set; }

        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [MaxLength(100, ErrorMessage = "County must be between 1 and 100 characters")]
        [MinLength(1, ErrorMessage = "County must be between 1 and 100 characters")]
        [Display(Name = "County")]
        public string County { get; set; }

        //[Required(ErrorMessage = "Please enter the City")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [MaxLength(100, ErrorMessage = "City must be between 1 and 100 characters")]
        [MinLength(1, ErrorMessage = "City must be between 1 and 100 characters")]
        [Display(Name = "City *")]
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
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter only numbers in Telephone number. ")]
        [MaxLength(10, ErrorMessage = "Telephone number must be of 10 digits")]
        [MinLength(10, ErrorMessage = "Telephone number must be of 10 digits")]
        public string Telephone { get; set; }

        public string CountryCodeTelephone { get; set; }

        #endregion

        #region Fax

        [Display(Name = "Fax")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter only numbers in Fax number. ")]
        [MaxLength(10, ErrorMessage = "Fax number must be of 10 digits")]
        [MinLength(10, ErrorMessage = "Fax number must be of 10 digits")]
        public string Fax { get; set; }

        public string CountryCodeFax { get; set; }

        #endregion

        #region Email

        [Required(ErrorMessage = "Please enter the E-Mail Address *")]
        [EmailAddress(ErrorMessage = "Please enter the valid email address")]
        [Display(Name = "E-Mail Address *")]
        public string EmailAddress { get; set; }
        
        #endregion

       public ICollection<EmployeeDepartmentViwModel> Departments { get; set; }

       public ICollection<EmployeeDesignationViewModel> Designations { get; set; }
    }
}
