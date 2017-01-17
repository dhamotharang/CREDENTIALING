using AHC.CD.Entities.MasterData;
using AHC.CD.Entities.MasterData.Tables;
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
    public class CMECertificationViewModel
    {
        
        public int CMECertificationID { get; set; }

        [Required]
        [Display(Name="Degree Awarded*")]
        public string QualificationDegree { get; set; }

        [Required]
        [Display(Name = "Post Graduation Training/CME Name*")]
        public string Certification { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        //[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Enter only alphabets")]
        [Display(Name = "Sponsor Name*")]
        public string SponsorName { get; set; }

        [Required]
        [Display(Name = "Expiration Date*")]
        [DateEnd(DateStartProperty = "EndDate", IsRequired = true, ErrorMessage = "Date should be greater than Completion Date")]        
        public DateTime ExpiryDate { get; set; }
        
        [Required]
        [Range(0,double.MaxValue)]
        [Display(Name = "Credit Hours*")]
        public double CreditHours { get; set; }

        public EducationAddressViewModel SchoolInformation { get; set; }

        [Required]
        [Display(Name = "Start Date*")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = true, ErrorMessage = "Date should be in between past 100 years from now!!")]        
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Completion Date*")]
        [DateEnd(DateStartProperty = "StartDate", IsRequired = true, ErrorMessage = "Date should be greater than Start Date")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = true, ErrorMessage = "Date should be in between past 100 years from now!!")] 
        public DateTime EndDate { get; set; }

        [Display(Name = "Supporting Document")]
        public string CertificatePath { get; set; }       
        
       
        
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = "Upload pdf, jpeg, jpg, png, bmp files only")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "File too large..!!")]
        [Display(Name = "Supporting Document")]
        public HttpPostedFileBase CertificateDocumentFile { get; set; }

       
    }
}
