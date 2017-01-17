using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class PracticeAccessibilityViewModel
    {
        public int PracticeAccessibilityID { get; set; }

        public ICollection<PracticeAccessibilityQuestionAnswerViewModel> PracticeAccessibilityQuestionAnswers { get; set; }

        [Display(Name="")]
        public string OtherHandicapedAccess { get; set; }

        [Display(Name = "")]
        public string OtherDisabilityServices { get; set; }

        [Display(Name = "")]
        public string OtherTransportationServices { get; set; }

        public string UpdateHistory { get; set; }

    }
}