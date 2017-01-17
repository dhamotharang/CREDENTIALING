using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class PracticePaymentAndRemittanceViewModel
    {
        public int PracticePaymentAndRemittanceID { get; set; }

        public int PracticeLocationDetailID { get; set; }
        
       // public int PaymentAndRemittancePersonId { get; set; }

        public FacilityEmployeeViewModel PaymentAndRemittancePerson { get; set; }

        //[Display(Name = "Electronic Billing Capabilities *")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        //public string ElectronicBillingCapability { get; private set; }

        [Display(Name = "Electronic Billing Capabilities *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        public YesNoOption? ElectronicBillingCapabilityYesNoOption { get; set; }

        [Display(Name = "Billing Department( If Hospital - Based)")]
        public string BillingDepartment { get; set; }

        [Display(Name = "Check Payable To *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        public string CheckPayableTo { get; set; }

        [Display(Name = "Office *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        public string Office { get; set; }

    }
}