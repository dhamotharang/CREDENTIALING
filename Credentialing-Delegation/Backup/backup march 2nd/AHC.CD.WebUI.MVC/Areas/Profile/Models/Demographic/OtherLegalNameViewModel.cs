using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class OtherLegalNameViewModel
    {
        public int OtherLegalNameID { get; set; }

        [Display(Name = "First Name *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
      //  [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        //[RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string OtherFirstName { get; set; }

        [Display(Name = "Middle Name")]
       // [StringLength(50, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
        ///[RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string OtherMiddleName { get; set; }

        [Display(Name = "Last Name *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
       // [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
       // [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string OtherLastName { get; set; }

        [Display(Name = "Suffix")]
       // [StringLength(10, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
       // [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string Suffix { get; set; }

        [Display(Name = "When did you start using other name")]
      //  [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "When did you stop using other name")]
        [DateEnd(DateStartProperty = "StartDate", MaxYear = "0", ErrorMessage = ValidationErrorMessage.STOP_DATE_RANGE)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Supporting Document")]
        public string DocumentPath { get; set; }

        [Display(Name = "Supporting Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
//        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase File { get; set; }

        public string UpdateHistory { get; set; }

        #region Status

        public StatusType? StatusType { get; set; }

        #endregion
    }
}
