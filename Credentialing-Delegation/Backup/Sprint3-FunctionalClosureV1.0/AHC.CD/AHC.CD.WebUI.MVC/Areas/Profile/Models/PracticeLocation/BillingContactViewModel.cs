using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class BillingContactViewModel
    {
        public int BillingContactID { get; set; }

        [Required]
        [Display(Name = "First Name *")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name ")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name *")]
        public string LastName { get; set; }


        public PracticeAddressViewModel PracticeAddress { get; set; }

        [Required]
        [Display(Name = "P.O Box Address *")]
        public string POBoxAddress { get; set; }

        [Required]
        [Display(Name = "Telephone *")]
        public string Telephone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "E-Mail Address")]
        public string EmailID { get; set; }

        #region ElectronicBillingCapability

        [Required]
        [Display(Name = "Electronic Billing Capabilities *")]
        public string ElectronicBillingCapability { get; private set; }

        //[NotMapped]
        //public YesNoOption ElectronicBillingCapabilityYesNoOption
        //{
        //    get
        //    {
        //        return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ElectronicBillingCapability);
        //    }
        //    set
        //    {
        //        this.ElectronicBillingCapability = value.ToString();
        //    }
        //}

        #endregion

        [Display(Name = "Billing Department (If Hospital- Based)")]
        public string BillingDepartment { get; set; }

        [Required]
        [Display(Name = "Check Payable To *")]
        public string CheckPayableTo { get; set; }

        [Required]
        [Display(Name = "Office *")]
        public string Office { get; set; }
    }
}