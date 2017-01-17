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

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class PhoneDetailViewModel
    {
        public int? PhoneDetailID { get; set; }

        //[RequiredIf("PhoneTypeEnum", (int)PhoneTypeEnum.Mobile, ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = ValidationErrorMessage.PHONE_FAX_NUMBER)]
        [Display(Name = "Mobile Number")]
        //[ProfileRemote("Validation", "IsContactNumberDoesNotExists", true, "CountryCode,PhoneDetailID", ErrorMessage = "Phone Number Used!!")]
        public string Number { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Country Code")]
        //[ProfileRemote("Validation", "IsContactNumberDoesNotExists", true, "Number,PhoneDetailID", ErrorMessage = "Phone Number Used!!")]
        public string CountryCode { get; set; }

        #region PhoneType

        public PhoneTypeEnum PhoneTypeEnum { get; set; }

        #endregion


        #region Preference

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Phone Preference")]
        public PreferenceType PreferenceType { get; set; }
        
        #endregion  

        
        #region Status

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Status")]
        public StatusType StatusType { get; set; }
        
        #endregion 
    }
}