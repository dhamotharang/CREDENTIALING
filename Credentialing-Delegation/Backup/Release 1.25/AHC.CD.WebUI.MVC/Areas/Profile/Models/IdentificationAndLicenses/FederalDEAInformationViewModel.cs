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
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class FederalDEAInformationViewModel
    {
        public int FederalDEAInformationID { get; set; }

     
        [Display(Name = "DEA Number *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(12, MinimumLength = 9, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_NUMBERS_HYPHEN)]
        public string DEANumber { get; set; }

        [Display(Name = "State of Registration *")]
   [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        public string StateOfReg { get; set; }

        [Display(Name = "Issue Date")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
      //[DateStart(IsRequired = false, MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? IssueDate { get; set; }

        [Display(Name = "Expiry Date")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [DateEnd(DateStartProperty = "IssueDate", IsRequired = false, ErrorMessage = "Expiry Date should not be less than Issue Date.")]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? ExpiryDate { get; set; }

        [Required]
        public ICollection<DEAScheduleInfoViewModel> DEAScheduleInfoes { get; set; }

        #region LicenseInGoodStanding

        //[Display(Name = "License in good standing? *")]
        //public string IsInGoodStanding { get; set; }

        [Display(Name = "License in good standing?")]
       
        public YesNoOption? GoodStandingYesNoOption { get; set; }
        #endregion


        [Display(Name = "DEA Certificate")]
        public string DEALicenceCertPath { get; set; }
        
        [Display(Name = "DEA Certificate")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
//        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase DEALicenceCertFile { get; set; }
    }

}
