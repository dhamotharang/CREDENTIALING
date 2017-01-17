using AHC.CD.Entities.MasterData;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Resources.Messages;
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
        [Display(Name = "US Graduate ?*")]
        public YesNoOption USGraduateYesNoOption { get; set; }

        [RequiredIf("EducationQualificationType", (int)EducationQualificationType.Graduate, ErrorMessage = "Please specify your graduation type")]
        [Display(Name = "Graduate Type*")]
        public EducationGraduateType GraduateType { get; set; }        

          
        public EducationQualificationType EducationQualificationType { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Degree Awarded*")]
        public string QualificationDegree { get; set; }
        
        public EducationAddressViewModel SchoolInformation { get; set; }

        [Required(ErrorMessage = "Please specify if you completed your Education in this school ?")]
        [Display(Name = "Did you complete your education at this school ?*")]
        public YesNoOption CompletedYesNoOption { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Start Date*")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = true, ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "End Date*")]
        [DateEnd(DateStartProperty = "StartDate", MaxYear= "0", IsRequired = true, ErrorMessage = ValidationErrorMessage.STOP_DATE_RANGE)]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = true, ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)] 
        public DateTime EndDate { get; set; }
                

        [Display(Name = "Supporting Document")]
        public string CertificatePath { get; set; }

        
        [Display(Name = "Supporting Document")]
        //[RequiredIfEmpty("CertificatePath", ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [RequiredIf("EducationQualificationType", (int)EducationQualificationType.Graduate, ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase CertificateDocumentFile { get; set; }       
        
    }
}
