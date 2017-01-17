using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
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
    public class StateLicenseViewModel
    {
        public int StateLicenseInformationID { get; set; }

        [Required(ErrorMessage = "Please enter the State License Number.")]
        [StringLength(10, MinimumLength = 7, ErrorMessage = "State License number should be between 7 and 10 characters")]
        [RegularExpression(@"[a-zA-Z0-9]*$", ErrorMessage = "Please enter valid State License Number.Only alphabets,numbers and hyphens accepted")]
        [Display(Name = "State License Number *")]
        public string LicenseNumber { get; set; }

        [Required(ErrorMessage = "Please select the State License Type..")]
        [Display(Name = "State License Type *")]
        public int ProviderTypeID { get; set; }

        [Required(ErrorMessage = "Please select the state License Status.")]
        [Display(Name = "State License Status *")]
        public int StateLicenseStatusID { get; set; }

        [Required(ErrorMessage = "Please select the State in which the License was Issued")]
        [Display(Name = "Issue State *")]
        public string IssueState { get; set; }

        #region IsPresentlyStaying

        [Display(Name = "Are you currently practicing in this state? *")]
        public string IsCurrentPracticeState
        {
            get
            {
                return this.CurrentPracticeStateYesNoOption.ToString();
            }
            private set
            {
                this.CurrentPracticeStateYesNoOption = (YesNoOption)Enum.Parse(typeof(YesNoOption), value);
            }
        }

        [Required(ErrorMessage = "Please specify whether you are currently practicing in this state ?")]
        [Display(Name = "Are you currently practicing in this state? *")]
        public YesNoOption CurrentPracticeStateYesNoOption { get; set; }
        #endregion

        [Required(ErrorMessage = "Please enter the Issue Date.")]
        [DateStart(IsRequired = true, MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "Issue Date should not be greater than current date")]
        [Display(Name = "Issue Date *")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "Please enter the Expiration Date.")]
        [GreaterThan("IssueDate", ErrorMessage = "Expiration Date should not be less than Issue date.")]
        [Display(Name = "Expiry Date *")]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "Please enter the Current Issue Date.")]
        [DateStart(IsRequired = true, MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "Current Issue Date should not be greater than current date")]
        [LessThanOrEqualTo("ExpiryDate", ErrorMessage = "Current Issue Date should not be greater than Expiration Date.")]
        [Display(Name = "Current Issue Date *")]
        public DateTime CurrentIssueDate { get; set; }


        #region LicenseInGoodStanding

        [Display(Name = "License in good standing? *")]
        public string LicenseInGoodStanding{get;set;}


        [Required(ErrorMessage = "Please specify whether your License is in Good Standing ?")]
        [Display(Name = "License in good standing? *")]
        public YesNoOption GoodStandingYesNoOption { get; set; }
        #endregion

      
        [Display(Name = "Document Preview *")]


        public string StateLicenseDocumentPath { get; set; }
      
        [Display(Name = "Document Preview *")]
       // [RequiredIfEmpty("StateLicenseDocumentPath", ErrorMessage = "Upload a supporting Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,doc,jpg,docx,jpeg,png,bitmap", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bitmap, .doc, .docx")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "State License Document should be less than 10mb in size.")]
        public HttpPostedFileBase StateLicenseDocumentFile { get; set; }

    }
}
