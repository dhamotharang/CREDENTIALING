using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class FederalDEAInformationViewModel
    {
        public int FederalDEAInformationID { get; set; }

        [Required(ErrorMessage = "Please enter the DEA Number.")]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "DEA number should be between 9 and 12 characters.")]
        [RegularExpression(@"[a-zA-Z0-9]*$", ErrorMessage = "Please enter valid DEA Number.Only numbers, alphabets and hyphens accepted")]
        [Display(Name = "DEA Number *")]
        public string DEANumber { get; set; }

        [Required(ErrorMessage = "Please select the State of Registration.")]
        [Display(Name = "State of Registration *")]
        public string StateOfReg { get; set; }

        [Required(ErrorMessage = "Please enter a Issue Date..")]
        [DateStart(IsRequired = true, MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "Issue Date should not be greater than current date.")]
        [Display(Name = "Issue Date *")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "Please enter a Expiration Date.")]
        [Display(Name = "Expiry Date *")]
        [GreaterThan("IssueDate", ErrorMessage = "Expiration Date should not be less than start date.")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public ICollection<DEAScheduleInfoViewModel> DEAScheduleInfoes { get; set; }

        #region LicenseInGoodStanding

        [Display(Name = "License in good standing? *")]
        public string IsInGoodStanding { get; set; }


        [Required(ErrorMessage = "Please specify whether your License is in Good Standing ?")]
        [Display(Name = "License in good standing? *")]
        public YesNoOption GoodStandingYesNoOption { get; set; }
        #endregion


        [Display(Name = "Certificate Preview *")]
        public string DEALicenceCertPath { get; set; }

        //  [RequiredIfEmpty("DEALicenceCertPath", ErrorMessage = "Upload a supporting Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,doc,jpg,docx,jpeg,png,bitmap", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bitmap, .doc, .docx")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "DEA Document should be less than 10mb in size.")]
        public HttpPostedFileBase DEALicenceCertFile { get; set; }


        public string Status { get; set; }

    }

}
