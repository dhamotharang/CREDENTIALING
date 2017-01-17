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

        [Required(ErrorMessage = "Please enter the Medicaid Number.")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Medicaid Number should be between 5 and 10 characters.")]
        [RegularExpression(@"[0-9-]*$", ErrorMessage = "Please enter valid Medicaid Number.Only numbers and hyphen accepted.")]
        [Display(Name = "Medicaid Number *")]
        public string LicenseNumber { get; set; }

        [Required(ErrorMessage = "Please enter the Issue State.")]
        [Display(Name = "Issue State *")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter the Issue Date.")]
        [Display(Name = "Issue Date *")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "Issue Date should not be greater than current date")]
        public DateTime IssueDate { get; set; }

        public string CertificatePath { get; set; }

        //  [RequiredIfEmpty("CertificatePath",ErrorMessage = "Upload a supporting Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,doc,jpg,docx,jpeg,png,bitmap", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bitmap, .doc, .docx")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Medicaid Document should be less than 10mb in size.")]
        public HttpPostedFileBase CertificateFile { get; set; }
    }
}