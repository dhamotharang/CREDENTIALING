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
    public class MedicareInformationViewModel
    {
        public int MedicareInformationID { get; set; }

        [Required(ErrorMessage = "Please enter the Medicare Number.")]
        [StringLength(7, MinimumLength = 5, ErrorMessage = "Medicaid Number should be between 5 and 7 characters.")]
        [RegularExpression(@"[a-zA-Z]{1}[0-9]{5}$", ErrorMessage = "Please enter valid Medicare Number.Alphanumeric characters accepted.")]
        [Display(Name = "Medicare Number *")]
        public string LicenseNumber { get; set; }

        [Required(ErrorMessage = "Please enter the Issue date.")]
        [DateStart(IsRequired = true, MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "Issue Date should not be greater than current date.")]
        [Display(Name = "Issue Date *")]
        public DateTime IssueDate { get; set; }

        public string CertificatePath { get; set; }
        [Display(Name = "Document Preview *")]


        // [RequiredIfEmpty("CertificatePath",ErrorMessage = "Upload a supporting Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,doc,jpg,docx,jpeg,png,bitmap", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bitmap, .doc, .docx")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Medicare Document should be less than 10mb in size.")]
        public HttpPostedFileBase CertificateFile { get; set; }
    }
}