using AHC.CD.Entities.MasterData;
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

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class EducationDetailViewModel
    {
        public int EducationDetailID { get; set; }

        [Required(ErrorMessage = "Please specify if you are a US Graduate ?")]
        [Display(Name = "US Graduate ?")]
        public YesNoOption USGraduateYesNoOption { get; set; }

        [RequiredIf("EducationQualificationType", (int)EducationQualificationType.Graduate, ErrorMessage="Field is required")]
        [Display(Name = "Graduate Type*")]
        public EducationGraduateType GraduateType { get; set; }        

          
        public EducationQualificationType EducationQualificationType { get; set; }

        [Required(ErrorMessage = "Please select the Degree awarded")]
        [Display(Name = "Degree Awarded*")]
        public string QualificationDegree { get; set; }
        
        public EducationAddressViewModel SchoolInformation { get; set; }

        [Required]
        [Display(Name = "Did you complete your education at this school ?*")]
        public YesNoOption CompletedYesNoOption { get; set; }

        [Required(ErrorMessage = "Please enter a Start Date")]
        [Display(Name = "Start Date*")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = true, ErrorMessage = "Date should be in between past 100 years from now!!")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please enter a End Date")]
        [Display(Name = "End Date*")]
        [DateEnd(DateStartProperty = "StartDate", IsRequired = true, ErrorMessage = "End Date should be greater than start date")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = true, ErrorMessage = "Date should be in between past 100 years from now!!")] 
        public DateTime EndDate { get; set; }
                

        [Display(Name = "Supporting Document")]
        public string CertificatePath { get; set; }

        
        [Display(Name = "Supporting Document*")]
        [RequiredIfEmpty("CertificatePath", ErrorMessage = "The file is required")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = "Upload pdf, jpeg, jpg, png, bmp files only")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "File too large..!!")]
        public HttpPostedFileBase CertificateDocumentFile { get; set; }       
        
    }
}
