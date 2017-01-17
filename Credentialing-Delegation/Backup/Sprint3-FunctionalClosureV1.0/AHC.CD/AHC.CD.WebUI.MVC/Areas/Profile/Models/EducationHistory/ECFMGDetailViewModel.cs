using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class ECFMGDetailViewModel
    {      
        
        public int ECFMGDetailID { get; set; }

        [Required(ErrorMessage="Please enter the ECFMG Number.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Please enter valid ECFMG Number. Only numbers accepted.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "ECFMG number should be 8 digits.")]
        [Display(Name = "ECFMG Number*")]
        public string ECFMGNumber { get; set; }
        
        [Required(ErrorMessage="Please enter ECFMG Issue Date.")]
        [Display(Name = "ECFMG Issue Date*")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = true, ErrorMessage = "Issue Date should not be greater than current date.")]
        public DateTime ECFMGIssueDate { get; set; }

        [Display(Name = "Supporting Document")]
        public string ECFMGCertPath { get; set; }

        
        [Display(Name = "Supporting Document*")]
        //[RequiredIfEmpty("ECFMGCertPath", ErrorMessage = "The file is required")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = "Please select the file of type .pdf, .jpeg, .png, .jpg, .bmp .")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "ECFMG Certificate should be less than 10mb in size")]
        public HttpPostedFileBase ECFMGCertificateDocumentFile { get; set; }
    }
}
