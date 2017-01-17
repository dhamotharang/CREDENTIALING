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
    public class CDSCInformationViewModel
    {
      
        public int CDSCInformationID { get; set; }

        [Required(ErrorMessage = "Please enter the CDS Number.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "CDS number should have 1 alphabet and 5 digits.")]
        [RegularExpression(@"[a-zA-Z]{1}[0-9]{5}$", ErrorMessage = "Please enter valid CDS Number.CDS number should have 1 alphabet and 5 digits.")]
        [Display(Name = "CDS Number *")]
        public string CertNumber { get; set; }

        [Required(ErrorMessage = "Please select the Issue State.")]
        [Display(Name = "Issue State *")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter date in mm/dd/yyyy format.")]
        [DateStart(IsRequired = true, MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "Issue Date should not be greater than current date.")]
        [Display(Name = "Issue Date *")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "Please enter date in mm/dd/yyyy format.")]
        [GreaterThan("IssueDate",ErrorMessage = "Expire Date should not be less than start date.")]
        [Display(Name = "Expiry Date *")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Document Preview *")]
        public string CDSCCerificatePath { get; set; }
       // [RequiredIfEmpty("CDSCCerificatePath",ErrorMessage = "Upload a supporting Document")]

        [Display(Name = "Document Preview *")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,doc,jpg,docx,jpeg,png,bitmap", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bitmap, .doc, .docx")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "CDS Document should be less than 10mb in size.")]
        public HttpPostedFileBase CDSCCerificateFile { get; set; }
    

    }
}
