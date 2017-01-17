using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class OtherIdentificationNumberViewModel
    {
        public int OtherIdentificationNumberID { get; set; }

        [Display(Name = "NPI Number *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        //[ProfileRemote("Validation", "IsNPINumberDoesNotExists", true, "profileId", ErrorMessage="NPI Number Used!!")]
        public string NPINumber { get; set; }

        [Display(Name = "CAQH Number")]
        //[StringLength(8, MinimumLength = 8, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        //[ProfileRemote("Validation", "IsCAQHNumberDoesNotExists", false, "profileId", ErrorMessage = "CAQH Number Used!!")]
        public string CAQHNumber { get; set; }
     
        #region NPI UserName & Password

        [Display(Name = "NPI User Name")]
        [RequiredIfNotEmpty("NPIPassword", ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        //[RegularExpression(RegularExpression.ALPHA_NUMERIC, ErrorMessage = ValidationErrorMessage.ALPHA_NUMERIC)]
        //[StringLength(40, MinimumLength = 8, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        //[ProfileRemote("Validation", "IsNPIUsernameDoesNotExists", false, "profileId", ErrorMessage = "NPI Username Used!!")]
        public string NPIUserName { get; set; }
        
        [Display(Name = "NPI Password")]
        //[RegularExpression(RegularExpression.ALPHA_NUMERIC, ErrorMessage = ValidationErrorMessage.ALPHA_NUMERIC)]
        //[StringLength(40, MinimumLength = 8, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string NPIPassword { get; set; }

        #endregion

        #region CAQH UserName & Password

        [Display(Name = "CAQH User Name")]
        [RequiredIfNotEmpty("CAQHPassword", ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        //[RegularExpression(RegularExpression.ALPHA_NUMERIC, ErrorMessage = ValidationErrorMessage.ALPHA_NUMERIC)]
        //[StringLength(15, MinimumLength = 8, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
       // [ProfileRemote("Validation", "IsCAQHUsernameDoesNotExists", false, "profileId", ErrorMessage = "CAQH Username Used!!")]
        public string CAQHUserName { get; set; }

        [Display(Name = "CAQH Password")]
        //[RegularExpression(RegularExpression.ALPHA_NUMERIC, ErrorMessage = ValidationErrorMessage.ALPHA_NUMERIC)]
        //[StringLength(15, MinimumLength = 8, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string CAQHPassword { get; set; }

        #endregion


        //[StringLength(6,MinimumLength = 6, ErrorMessage = "Please enter valid UPIN Number.Only numbers and alphabets accepted.")]
        //[RegularExpression(@"[a-zA-Z]{1}[0-9]{5}$", ErrorMessage = "UPIN number should be 1 alphabet followed by 5 digits")]
        [Display(Name = "UPIN Number")]
        //[StringLength(6, MinimumLength = 6, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        //[RegularExpression(@"[a-zA-Z]{1}[0-9]{5}$", ErrorMessage = "UPIN number should have 1 alphabet and 5 digits.")]
        [RegularExpression(RegularExpression.ALPHA_NUMERIC, ErrorMessage = ValidationErrorMessage.ALPHA_NUMERIC)]
        public string UPINNumber { get; set; }

        [Display(Name = "USMLE Number")]
        //[StringLength(8, MinimumLength = 8, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        public string USMLENumber { get; set; }

    }
}
