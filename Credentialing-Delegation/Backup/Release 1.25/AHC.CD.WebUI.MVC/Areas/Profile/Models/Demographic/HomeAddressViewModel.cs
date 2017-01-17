using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class HomeAddressViewModel
    {
        public int HomeAddressID { get; set; }

        [Display(Name = "Apartment/Unit Number")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string UnitNumber { get; set; }

        [Display(Name = "Street/P. O. Box No: *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string Street { get; set; }

        [Display(Name = "State *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        public string State { get; set; }

        [Display(Name = "City *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        public string City { get; set; }

        [Display(Name = "County")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        public string County { get; set; }

        [Display(Name = "Country *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        public string Country { get; set; }

        [Display(Name = "Zip Code *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_NUMBERS_HYPHEN)]
        [StringLength(10, MinimumLength = 5, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string ZipCode { get; set; }

        #region IsPresentlyStaying

        [Display(Name = "Is Present Home Address ?")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SPECIFY)]
        public YesNoOption? IsPresentlyStayingYesNoOption { get; set; }

        #endregion

        [Display(Name = "Living Here From")]
        //[Required(ErrorMessage = "Please specify since when have you been Living Here.")]
      //  [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? LivingFromDate { get; set; }

        [Display(Name = "Living Here To")]
        //[RequiredIf("IsPresentlyStayingYesNoOption", (int)YesNoOption.NO, ErrorMessage = "Please specify till when you Lived Here.")]
        [DateEnd(DateStartProperty = "LivingFromDate", MaxYear = "0", ErrorMessage = ValidationErrorMessage.STOP_DATE_RANGE)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? LivingEndDate { get; set; }

        [Display(Name = "Living Here From")]
        public int StartYear { get; set; }

        [Display(Name = "Living Here From")]
        public int StartMonth { get; set; }

        [Display(Name = "Living Here To")]
        public int EndYear { get; set; }

        [Display(Name = "Living Here To")]
        public int EndMonth { get; set; }

        #region AddressPreference

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SPECIFY)]
        [Display(Name = "Is Primary Home Address ?")]
        public PreferenceType? AddressPreferenceType { get; set; }

        #endregion

        //#region Status

        //[Required]
        //public StatusType StatusType { get; set; }
        
        //#endregion  
    }
}
