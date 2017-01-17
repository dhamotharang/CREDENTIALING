using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using AHC.CD.Resources.Rules;
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

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(12, MinimumLength = 8, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_NUMBER, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_NUMBER)]      
        [Display(Name = "ECFMG Number*")]
        public string ECFMGNumber { get; set; }
        
        //[Required(ErrorMessage=ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "ECFMG Issue Date")]
      //  [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = false, ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        //////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? ECFMGIssueDate { get; set; }

        [Display(Name = "Supporting Document")]
        public string ECFMGCertPath { get; set; }

        
        [Display(Name = "Supporting Document")]
        //[RequiredIfEmpty("ECFMGCertPath", ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
//        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase ECFMGCertificateDocumentFile { get; set; }
    }
}
