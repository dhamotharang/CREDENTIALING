using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class ProviderPracticeOfficeHourViewModel : PracticeOfficeHourViewModel
    {

        public string AnyTimePhoneCoverage { get; set; }

        [Required]
        [Display(Name = "24/7 Phone Coverage ?*")]
        public YesNoOption AnyTimePhoneCoverageYesNoOption { get; set; }

        [RequiredIf("AnyTimePhoneCoverageYesNoOption", (int)YesNoOption.YES, ErrorMessage = "Answering Service field is Required.")]
        [Display(Name = "Answering Service*")]
        public YesNoOption? AnsweringServiceYesNoOption { get; set; }

        [RequiredIf("AnyTimePhoneCoverageYesNoOption", (int)YesNoOption.YES, ErrorMessage = "Voice Mail With Instructions To Call Answering Service field is Required.")]
        [Display(Name = "Voice Mail With Instructions To Call Answering Service*")]
        public YesNoOption? VoiceMailToAnsweringServiceYesNoOption { get; set; }

        [RequiredIf("AnyTimePhoneCoverageYesNoOption", (int)YesNoOption.YES, ErrorMessage = "Voice Mail With Other Instructions field is Required.")]
        [Display(Name = "Voice Mail With Other Instructions*")]
        public YesNoOption? VoiceMailOtherYesNoOption { get; set; }

        [Display(Name = "After Hours Telephone Number")]
        public string AfterHoursTelephoneNumber { get; set; }

        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = ValidationErrorMessage.PHONE_FAX_NUMBER)]
        public string Number { get; set; }

        public string CountryCode { get; set; }

        public string UpdateHistory { get; set; }
    }
}