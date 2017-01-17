using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class MedicaidInformationViewModel
    {
        public int MedicaidInformationID { get; set; }

        [Display(Name = "Medicaid Number *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        //[StringLength(10, MinimumLength = 5, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_NUMBER_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS_HYPHEN)]
        public string LicenseNumber { get; set; }

        [Display(Name = "Issue State *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        public string State { get; set; }

        [Display(Name = "Issue Date")]
      //  [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        public DateTime? IssueDate { get; set; }

        [Display(Name = "Medicaid Certificate document")]
        public string CertificatePath { get; set; }

        [Display(Name = "Medicaid Certificate document")]

        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase CertificateFile { get; set; }
    }
}