using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class BirthInformationViewModel
    {
        public int BirthInformationID { get; set; }

        [Display(Name = "Date of Birth *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [DateStart(MaxPastYear = "-18", MinPastYear = "-100", ErrorMessage = "Provider should be above 18 years of age.")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Country of Birth *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        //[RegularExpression(@"^[a-zA-Z ,-.]*$", ErrorMessage = "Please enter valid Country. Only alphabets, special characters and numbers accepted.")]
        public string CountryOfBirth { get; set; }

        [Display(Name = "State of Birth *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        //[RegularExpression(@"^[a-zA-Z ,-.]*$", ErrorMessage = "Please enter valid State. Only alphabets, special characters and numbers accepted.")]
        public string StateOfBirth { get; set; }

        [Display(Name = "County of Birth")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        //[RegularExpression(@"^[a-zA-Z ,-.]*$", ErrorMessage = "Please enter valid County. Only alphabets, special characters and numbers accepted.")]
        public string CountyOfBirth { get; set; }

        [Display(Name = "City of Birth *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        //[RegularExpression(@"^[a-zA-Z ,-.]*$", ErrorMessage = "Please enter valid City. Only alphabets, special characters and numbers accepted.")]
        public string CityOfBirth { get; set; }

        [Display(Name = "Supporting Document")]
        public string BirthCertificatePath { get; set; }

        [Display(Name = "Supporting Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase BirthCertificateFile { get; set; }
    }
}
