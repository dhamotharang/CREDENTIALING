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
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class PersonalIdentificationViewModel
    {
        public int PersonalIdentificationID { get; set; }

        [Display(Name = "Social Security Number *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
       [RegularExpression(RegularExpression.FOR_NUMBERS_SPACES_HYPHEN_FRWD_SLASH, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        [StringLength(9, MinimumLength = 9, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        public string SSN { get; set; }

        [Display(Name = "Supporting Document")]
        public string SSNCertificatePath { get; set; }

        [Display(Name = "SSN Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
        //[PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase SSNCertificateFile { get; set; }

        [Display(Name = "Driver's License")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
     //[RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_SPACES_HYPHEN_FRWD_SLASH_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_NUMBERS_HYPHEN_FRWD_SLASH_DOT)]
     //[StringLength(16, MinimumLength = 9, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string DL { get; set; }

        [Display(Name = "Issue State of DL")]
        //[RequiredIfNotEmpty("DL", ErrorMessage = "Issue State is required if DL is not empty.")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        public string DLState { get; set; }

        [Display(Name = "Supporting Document")]
        public string DLCertificatePath { get; set; }

        [Display(Name = "Driver's License Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
        //[PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase DLCertificateFile { get; set; }
    }
}
