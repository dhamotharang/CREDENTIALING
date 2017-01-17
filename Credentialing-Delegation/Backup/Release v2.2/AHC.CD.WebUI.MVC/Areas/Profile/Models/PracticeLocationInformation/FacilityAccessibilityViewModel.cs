using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class FacilityAccessibilityViewModel
    {
        public int FacilityAccessibilityID { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only Alphabets Numbers, And Spaces Accepted.")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
        public string OtherHandicappedAccess { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only Alphabets Numbers, And Spaces Accepted.")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
        public string OtherDisabilityServices { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only Alphabets Numbers, And Spaces Accepted.")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
        public string OtherTransportationAccess { get; set; }

        public ICollection<FacilityAccessibilityQuestionAnswerViewModel> FacilityAccessibilityQuestionAnswers { get; set; }
    }
}
