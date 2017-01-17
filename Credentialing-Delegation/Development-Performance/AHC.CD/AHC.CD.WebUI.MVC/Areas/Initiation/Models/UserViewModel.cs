using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Initiation.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage="Please enter the Full Name*")]
        [Display(Name="Full Name*")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the Email Id*")]
        [Display(Name = "Email Id*")]
        [DataType(DataType.EmailAddress, ErrorMessage="Please enter the valid email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please select the Role")]
        [Display(Name = "Role*")]
        public string RoleCode { get; set; }

        //[Required(ErrorMessage = "Please select the Gender*")]
        [Display(Name = "Gender")]
        public AHC.CD.Entities.MasterData.Enums.GenderType GenderType { get; set; }

        //[Required(ErrorMessage = "Please select the Date Of Birth*")]
        [Display(Name = "Date Of Birth")]
        [DateStart(MaxPastYear = "-18", MinPastYear = "-100", ErrorMessage = "Provider should be above 18 years of age.")]
        public DateTime? DateOfBirth { get; set; }

        //[Required(ErrorMessage = "Please enter the Mobile Number*")]
        [Display(Name = "Mobile Number")]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = ValidationErrorMessage.PHONE_FAX_NUMBER)]
        public string Phone { get; set; }

        public string PhoneCountryCode { get; set; }

    }
}