using AHC.CD.Entities.MasterData;
using AHC.CD.Entities.MasterData.Enums;
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
    public class ResidencyInternshipDetailViewModel
    {
        
        public int ResidencyInternshipDetailID { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Program Type*")]
        public ResidencyInternshipProgramType ResidencyInternshipProgramType { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Preference Type*")]
        public PreferenceType PreferenceType { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_NUMBER_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_NUMBERS_SPACE_HYPHEN)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Specialty*")]
        public int? SpecialtyID { get; set; }


        //[Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.ALPHA_SPACE, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE)]
        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Start Date")]
        //[DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = false, ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? StartDate { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "End Date")]
        [DateEnd(DateStartProperty = "StartDate", MaxYear = "0", IsRequired = false, ErrorMessage = ValidationErrorMessage.STOP_DATE_RANGE)]
        //[DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = false, ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Supporting Document")]
        public string DocumentPath { get; set; }


        [Display(Name = "Supporting Document")]
        //[RequiredIfEmpty("DocumentPath", ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
//        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase ResidecncyCertificateDocumentFile { get; set; }

        
    }
}
