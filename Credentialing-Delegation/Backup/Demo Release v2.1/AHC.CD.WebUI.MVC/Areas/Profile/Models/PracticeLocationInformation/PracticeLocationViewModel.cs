using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class PracticeLocationViewModel
    {

        public int FacilityID { get; set; }

        [Required(ErrorMessage = "Please enter Facility Name")]
        [RegularExpression("([a-zA-Z0-9-,. ]+)", ErrorMessage = "Please enter valid Facility Name.Only alpha-numerals, space, hyphen, comma and dot accepted")]
        [MaxLength(50, ErrorMessage = "Facility Name must be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "Facility Name must be between 2 and 50 characters")]
        [Display(Name = "Facility Name *")]
        public string FacilityName { get; set; }

        [Required(ErrorMessage = "Please enter Corporate or Practice Name")]
        [RegularExpression("([a-zA-Z0-9-,. ]+)", ErrorMessage = "Please enter valid Corporate or Practice Name.Only alpha-numerals, space, hyphen, comma and dot accepted")]
        [MaxLength(50, ErrorMessage = "Corporate or Practice Name must be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "Corporate or Practice Name must be between 2 and 50 characters")]
        [Display(Name = "Corporate or Practice Name *")]
        public string Name { get; set; }

        #region Address

        [Display(Name = "Building")]
        [MaxLength(50, ErrorMessage = "Suite/Building must be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "Suite/Building must be between 2 and 50 characters")]
        //[RegularExpression("([a-zA-Z0-9+ ?|#|.,'-]+)", ErrorMessage = "Please enter valid Building.Only alpha-numerals, space, comma, hyphen ,hash accepted")]
        public string Building { get; set; }

        [Required(ErrorMessage = "Please enter the Street")]
        [MaxLength(100, ErrorMessage = "Street must be between 1 and 100 characters")]
        [MinLength(1, ErrorMessage = "Street must be between 1 and 100 characters")]
        //[RegularExpression("([a-zA-Z0-9+ ?|#|.,'-]+)", ErrorMessage = "Please enter valid Street.Only alpha-numerals, space, comma, hyphen ,hash accepted")]
        [Display(Name = "Street/P. O. Box No: *")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Please enter the Country")]
        [MaxLength(100, ErrorMessage = "Country name must be between 2 and 50 characters")]
        [MinLength(1, ErrorMessage = "Country name must be between 2 and 50 characters")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [Display(Name = "Country *")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter the State")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [MaxLength(100, ErrorMessage = "State must be between 1 and 100 characters")]
        [MinLength(1, ErrorMessage = "State must be between 1 and 100 characters")]
        [Display(Name="State *")]
        public string State { get; set; }

        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [MaxLength(100, ErrorMessage = "County must be between 1 and 100 characters")]
        [MinLength(1, ErrorMessage = "County must be between 1 and 100 characters")]
        [Display(Name = "County")]
        public string County { get; set; }

        [Required(ErrorMessage = "Please enter the City")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [MaxLength(100, ErrorMessage = "City must be between 1 and 100 characters")]
        [MinLength(1, ErrorMessage = "City must be between 1 and 100 characters")]
        [Display(Name = "City *")]
        public string City { get; set; }

        [Display(Name = "Zip Code")]
        [RegularExpression("([a-zA-Z0-9+?-]+)", ErrorMessage = "Please enter valid Zipcode. Only alphabets, numbers and hyphens accepted")]
        [MaxLength(10, ErrorMessage = "ZipCode must be between 5 and 10 characters.")]
        [MinLength(5, ErrorMessage = "ZipCode must be between 5 and 10 characters.")]
        public string ZipCode { get; set; }

        #endregion

        [Display(Name = "Telephone")]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = "Please enter only numbers in Telephone number. ")]
        [MaxLength(10, ErrorMessage = "Telephone number must be of 10 digits")]
        [MinLength(10, ErrorMessage = "Telephone number must be of 10 digits")]
        public string Telephone { get; set; }

        public string CountryCodeTelephone { get; set; }

        [Display(Name = "Fax")]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = "Please enter only numbers in Fax number. ")]
        [MaxLength(10, ErrorMessage = "Fax number must be of 10 digits")]
        [MinLength(10, ErrorMessage = "Fax number must be of 10 digits")]
        public string Fax { get; set; }

        public string CountryCodeFax { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$", ErrorMessage = ValidationErrorMessage.INVALID_EMAIL_FORMAT)]
        [Display(Name = "Office E-mail Address")]
        public string EmailAddress { get; set; }

        public FacilityDetailViewModel FacilityDetail { get; set; }

        public string Status { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public int OrginizationId { get; set; }

    }
}