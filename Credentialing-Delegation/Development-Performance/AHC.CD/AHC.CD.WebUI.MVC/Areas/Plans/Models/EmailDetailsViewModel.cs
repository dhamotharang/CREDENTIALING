using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Plans.Models
{
    public class EmailDetailsViewModel
    {
        public int? EmailDetailID { get; set; }

        [Display(Name = "Email Address *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$", ErrorMessage = ValidationErrorMessage.INVALID_EMAIL_FORMAT)]
        [StringLength(100, MinimumLength = 7, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string EmailAddress { get; set; }

        #region Preference

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Email Preference")]
        public PreferenceType PreferenceType { get; set; }

        #endregion


        #region Status

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Status")]
        public StatusType StatusType { get; set; }

        #endregion 
    }
}