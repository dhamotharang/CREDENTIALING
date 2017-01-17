using AHC.CD.Entities.MasterData;
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
using System.Threading.Tasks;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class CMECertificationViewModel
    {
        
        public int CMECertificationID { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name="Degree Awarded*")]
        public string QualificationDegree { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Post Graduation Training/CME Name*")]
        public string Certification { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]        
        [Display(Name = "Sponsor Name*")]
        public string SponsorName { get; set; }

        [Required]
        [Display(Name = "Expiration Date*")]
        [DateEnd(DateStartProperty = "EndDate", IsRequired = true, ErrorMessage = "Date should be greater than Completion Date")]
        [DateStart(MaxPastYear = "100", MinPastYear = "0", IsRequired = true, ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)] 
        public DateTime ExpiryDate { get; set; }
        
        [Required(ErrorMessage=(ValidationErrorMessage.REQUIRED_ENTER))]
        [RegularExpression(RegularExpression.PERCENT_TWO_DECIMAL_PLACES, ErrorMessage=ValidationErrorMessage.PERCENT_TWO_DECIMAL_PLACES)]
        [Display(Name = "Credit Hours*")]
        public double CreditHours { get; set; }

        public EducationAddressViewModel SchoolInformation { get; set; }

        [Required]
        [Display(Name = "Start Date*")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = true, ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]        
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Completion Date*")]
        [DateEnd(DateStartProperty = "StartDate", MaxYear="0", IsRequired = true, ErrorMessage = ValidationErrorMessage.STOP_DATE_RANGE)]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = true, ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)] 
        public DateTime EndDate { get; set; }

        [Display(Name = "Supporting Document")]
        public string CertificatePath { get; set; }       
        
       
        
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        [Display(Name = "Supporting Document")]
        public HttpPostedFileBase CertificateDocumentFile { get; set; }

       
    }
}
