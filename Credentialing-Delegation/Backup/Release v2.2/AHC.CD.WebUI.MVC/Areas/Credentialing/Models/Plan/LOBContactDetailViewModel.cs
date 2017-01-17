using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Plan
{
    public class LOBContactDetailViewModel
    {
        public int? LOBContactDetailID { get; set; } 

        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }

        public ContactDetailViewModel ContactDetail { get; set; }   

        //[Display(Name = "Email Address")]
        //[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$", ErrorMessage = ValidationErrorMessage.INVALID_EMAIL_FORMAT)]
        //public string EmailAddress { get; set; }

        //[Display(Name = "Mobile Number")]
        //[RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = ValidationErrorMessage.PHONE_FAX_NUMBER)]
        //public string PhoneNumber { get; set; }

        //public string PhoneCountryCode { get; set; }

        //[Display(Name = "Fax Number")]
        //[RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = ValidationErrorMessage.PHONE_FAX_NUMBER)]
        //public string FaxNumber { get; set; }

        //public string FaxCountryCode { get; set; }

        //[Display(Name = "Is Primary")]
        //public bool IsPrimary { get; set; }

        #region Status

        public StatusType? StatusType { get; set; }

        #endregion  
    }
}
