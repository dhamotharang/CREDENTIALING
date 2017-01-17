using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class EducationAddressViewModel
    {        

        public int SchoolInformationID { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "School Name*")]
        public string SchoolName { get; set; }

        
        [RegularExpression(@"^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$", ErrorMessage=ValidationErrorMessage.INVALID_EMAIL_FORMAT)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)] 
        [StringLength(10, MinimumLength=10, ErrorMessage= ValidationErrorMessage.STRING_LENGTH_FIXED)]
        [Display(Name = "Telephone Number")]
        public string Phone { get; set; }

        public string PhoneCountryCode { get; set; }

        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        [Display(Name = "Fax Number")]
        public string Fax { get; set; }

        public string FaxCountryCode { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]        
        [Display(Name = "Suite/Building")]
        public string Building { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_NUMBER_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_NUMBERS_SPACE_HYPHEN)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [Display(Name = "Street*")]
        public string Street { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [Display(Name = "State*")]
        public string State { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        //[RegularExpression(RegularExpression.FOR_ALPHABETS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [StringLength(100, MinimumLength=1, ErrorMessage=ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [Display(Name = "City*")]
        public string City { get; set; }

        
        [Display(Name = "County")]
        //[RegularExpression(RegularExpression.FOR_ALPHABETS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string County { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [Display(Name = "Country*")]
        public string Country { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_NUMBERS_HYPHEN)]
        [StringLength(10, MinimumLength=5, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [Display(Name = "Zip code")]
        public string ZipCode { get; set; }        
       
    }
}
