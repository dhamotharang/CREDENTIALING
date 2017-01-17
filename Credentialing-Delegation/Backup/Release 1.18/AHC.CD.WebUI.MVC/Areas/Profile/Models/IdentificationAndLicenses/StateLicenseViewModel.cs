using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
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
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class StateLicenseViewModel
    {
        public int StateLicenseInformationID { get; set; }

        //#region Renewal

        //[Display(Name = "Do you want to renew your license?")]
        //public YesNoOption? RenewYesNoOption { get; set; }
        //#endregion
      
        [Display(Name = "State License Number *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(15, MinimumLength = 7, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_HYPHEN_DOT_SPACE, ErrorMessage = ValidationErrorMessage.ALPHABETS_NUMBER_HYPHEN_DOT_SPACE)]
        public string LicenseNumber { get; set; }

        [Display(Name = "State License Type")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        public int? ProviderTypeID { get; set; }

        [Display(Name = "State License Status")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        public int? StateLicenseStatusID { get; set; }

        [Display(Name = "Issue State")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        public string IssueState { get; set; }

        #region IsPresentlyStaying

        //[Display(Name = "Are you currently practicing in this state? *")]
        //public string IsCurrentPracticeState
        //{
        //    get
        //    {
        //        return this.CurrentPracticeStateYesNoOption.ToString();
        //    }
        //    private set
        //    {
        //        this.CurrentPracticeStateYesNoOption = (YesNoOption)Enum.Parse(typeof(YesNoOption), value);
        //    }
        //}

        [Display(Name = "Are you currently practicing in this state?")]
        //[Required(ErrorMessage = "Please specify whether you are currently practicing in this state ?")]
        public YesNoOption? CurrentPracticeStateYesNoOption { get; set; }
        #endregion

        [Display(Name = "Issue Date")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [DateStart(IsRequired = false, MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        public DateTime? IssueDate { get; set; }

        [Display(Name = "Expiration Date")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [DateEnd(DateStartProperty = "CurrentIssueDate", IsRequired = false, IsGreaterThan=true, ErrorMessage = "Expiry Date should not be less than or equal to Current Issue Date.")]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Current Issue Date")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [DateStart(IsRequired = false, MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        [DateEnd(DateStartProperty = "IssueDate", IsRequired = false, ErrorMessage="Current Issue Date should not be less than Issue Date.")]
        //[LessThan("ExpiryDate", ErrorMessage = ValidationErrorMessage.DATE_NOT_GREATER_THAN)]
        public DateTime? CurrentIssueDate { get; set; }


        #region LicenseInGoodStanding

        //[Display(Name = "License in good standing? *")]
        //public string LicenseInGoodStanding{get;set;}

        //[Required(ErrorMessage = "Please specify whether your License is in Good Standing ?")]
        [Display(Name = "License in good standing?")]
        public YesNoOption? GoodStandingYesNoOption { get; set; }
        #endregion
      
        [Display(Name = "State License Document")]
        public string StateLicenseDocumentPath { get; set; }

        [Display(Name = "State License Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase StateLicenseDocumentFile { get; set; }

    }
}
