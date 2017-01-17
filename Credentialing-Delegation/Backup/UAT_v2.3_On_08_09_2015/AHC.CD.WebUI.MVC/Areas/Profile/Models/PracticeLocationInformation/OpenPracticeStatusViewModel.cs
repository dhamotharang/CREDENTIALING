using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class OpenPracticeStatusViewModel
    {
        public int OpenPracticeStatusID { get; set; }

        public int PracticeLocationDetailID { get; set; }

        public ICollection<PracticeOpenStatusQuestionAnswerViewModel> PracticeQuestionAnswers { get; set; }

        [Display(Name = "If any of the above information varies by plan, Explain")]
       // [StringLength(500, MinimumLength = 2, ErrorMessage = "Please provide information between 2 and 500 characters.")]
       // [RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.ALPHABETS_NUMBER_SPACE_COMMA_HYPHEN_DOT)]
        public string AnyInformationVariesByPlan { get; set; }

        [Range(0,150, ErrorMessage="Minimum Age must be between 0 and 150 years.")]
        [RegularExpression("^[0-9]*", ErrorMessage = "Minimum Age must be a number.")]
        [Display(Name = "Minimum")]
        public int? MinimumAge { get; set; }

        [Range(0, 150, ErrorMessage = "Maximum Age must be between 0 and 150 years.")]
        [RegularExpression("^[0-9]*", ErrorMessage = "Maximum Age must be a number.")]
        [NumberGreaterThan(DependentProperty = "MinimumAge", ErrorMessage = "Maximum Age should not be less than Minimum Age.")]
        //[GreaterThanOrEqualTo("MinimumAge", ErrorMessage = "Maximum Age should not be less than Minimum Age.")]
        [Display(Name = "Maximum")]
        public int? MaximumAge { get; set; }

        [Display(Name = "Are There Any Practice Limitations?")]
        public YesNoOption? AnyPracticeLimitationOption { get; set; }

        [Display(Name = "List Other Limitations")]
      //  [StringLength(500, MinimumLength = 2, ErrorMessage = "Please provide other limitations between 2 and 500 characters.")]
      //  [RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.ALPHABETS_NUMBER_SPACE_COMMA_HYPHEN_DOT)]
        public string OtherLimitations { get; set; }

        [Display(Name = "Gender Limitations")]
        public GenderLimitationType? GenderLimitationType { get; set; }
    }
}
