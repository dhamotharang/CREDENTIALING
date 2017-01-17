using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class PracticeProviderViewModel
    {
        public int PracticeProviderID { get; set; }

        public int PracticeLocationID { get; set; }

        [Display(Name = "NPI Number")]
       //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        public string NPINumber { get; set; }

        [Display(Name = "First Name *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
     //   [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
     //   [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
     //   [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
     //   [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
      //  [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
      //  [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string LastName { get; set; }
        
        public PracticeType? PracticeType { get; set; }

        public StatusType? StatusType { get; set; }

        public RelationType? RelationType { get; set; }

        #region Address

        [Display(Name = "Building")]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string Building { get; set; }

        [Display(Name = "Street/P. O. Box No: *")]
        //[RequiredIf("PracticeType", AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague, ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
       // [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string Street { get; set; }

        [Display(Name = "Country *")]
        //[RequiredIf("PracticeType", AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague, ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
       // [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        //[RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        public string Country { get; set; }

        [Display(Name = "State *")]
        //[RequiredIf("PracticeType", AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague, ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
       // [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        //[RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        public string State { get; set; }

        [Display(Name = "County")]
       // [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        //[RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        public string County { get; set; }

        [Display(Name = "City *")]
        //[RequiredIf("PracticeType", AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague, ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        //[StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        //[RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        public string City { get; set; }

        [Display(Name = "Zip Code")]
        //[RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_NUMBERS_HYPHEN)]
        [StringLength(10, MinimumLength = 5, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string ZipCode { get; set; }

        #endregion

        #region Mobile Number  
        
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = "Please enter only numbers for Telephone number")]
        [Display(Name = "Telephone")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Telephone number must be of 10 digits")]
        public string Telephone { get; set; }
        
        public string CountryCodeTelephone { get; set; }

        #endregion

        [Display(Name = "Provider Type")]        
        public ICollection<PracticeProviderTypeViewModel> PracticeProviderTypes { get; set; }

        [Display(Name = "Specialty")]        
        public ICollection<PracticeProviderSpecialtyViewModel> PracticeProviderSpecialties { get; set; }
    }
}