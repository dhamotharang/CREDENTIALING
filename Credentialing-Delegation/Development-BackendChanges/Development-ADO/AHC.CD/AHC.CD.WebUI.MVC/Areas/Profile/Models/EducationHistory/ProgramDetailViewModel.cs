using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class ProgramDetailViewModel
    {

        public int ProgramDetailID { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Program Type *")]
        public ResidencyInternshipProgramType? ResidencyInternshipProgramType { get; set; }

        [Display(Name = "Department")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_NUMBER_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_NUMBERS_SPACE_HYPHEN)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string Department { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Specialty")]
        public int? SpecialtyID { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Preference Type")]
        public PreferenceType? PreferenceType { get; set; }

        [Display(Name = "Affiliated University/Hospital")]
        public string HospitalName { get; set; }

        [Display(Name = "Director Name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
     //   [RegularExpression(RegularExpression.ALPHA_SPACE, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE)]
        public string DirectorName { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DateEnd(DateStartProperty = "StartDate", MaxYear = "0", IsRequired = false, ErrorMessage = ValidationErrorMessage.STOP_DATE_RANGE)]
        public DateTime? EndDate { get; set; }

        public EducationAddressViewModel SchoolInformation { get; set; }

        [Display(Name = "Did you complete your training at this school ?")]
        public YesNoOption? CompletedYesNoOption { get; set; }

        [Display(Name = "If No, Give the reason")]
      //  [StringLength(100, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        //[RegularExpression(RegularExpression.ALPHA_SPACE, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE)]
        public string InCompleteReason { get; set; }

        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase ProgramDocumentPath { get; set; }

        [Display(Name = "Supporting Document")]
        public string DocumentPath { get; set; }

        public StatusType? StatusType { get; set; }
    }
}