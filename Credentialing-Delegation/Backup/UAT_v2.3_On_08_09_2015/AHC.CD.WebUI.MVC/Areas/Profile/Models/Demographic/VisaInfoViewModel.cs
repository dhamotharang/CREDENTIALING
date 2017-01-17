using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class VisaInfoViewModel
    {
        public int VisaInfoID { get; set; }

        [Display(Name = "Visa Number")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [RegularExpression(RegularExpression.ALPHA_NUMERIC, ErrorMessage = ValidationErrorMessage.ALPHA_NUMERIC)]
        [StringLength(8, MinimumLength = 8, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        public string VisaNumber { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Visa Status")]
        public int? VisaStatusID { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Visa Type")]
        public int? VisaTypeID { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Visa Sponsor")]
   //     [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_NUMBER_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_NUMBERS_SPACE_HYPHEN)]
        //[StringLength(100, MinimumLength = 2, ErrorMessage = "Length of Visa Sponsor must be between {2} and {1} characters.")]
        public string VisaSponsor { get; set; }

        [Display(Name = "Visa Expiration")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
       // [DateStart(MinPastYear="0", MaxPastYear="10", ErrorMessage= ValidationErrorMessage.START_DATE_RANGE)]
        public DateTime? VisaExpirationDate { get; set; }

        //[Required]
        [Display(Name = "Select VISA Document")]
        public string VisaCertificatePath { get; set; }

        [Display(Name = "Supporting VISA Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
//        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase VisaCertificateFile { get; set; }

        //[Required]
        [Display(Name = "Green Card Number")]
        [RegularExpression(RegularExpression.ALPHA_NUMERIC, ErrorMessage = ValidationErrorMessage.ALPHA_NUMERIC)]
        [StringLength(13, MinimumLength = 13, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        public string GreenCardNumber { get; set; }

        [Display(Name = "Select GreenCard Document")]
        public string GreenCardCertificatePath { get; set; }

        [Display(Name = "Supporting Green Card Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase GreenCardCertificateFile { get; set; }

        [Display(Name = "National Id. Number")]
        [RegularExpression(@"^[a-zA-Z0-9.-]*$", ErrorMessage = ValidationErrorMessage.FOR_ALPHABETS_NUMBER_HYPHEN_DOT)]
        [StringLength(18, MinimumLength = 11, ErrorMessage = ValidationErrorMessage.NUMBER_LENGTH_MAX_MIN)]
        public string NationalIDNumber { get; set; }

        [Display(Name = "Select National Identification Document")]
        public string NationalIDCertificatePath { get; set; }

        [Display(Name = "Supporting National Identification Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase NationalIDCertificateFile { get; set; }

        [RequiredIfNotEmpty("NationalIDNumber", ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Country of Issue")]
        public string CountryOfIssue { get; set; }
    }
}