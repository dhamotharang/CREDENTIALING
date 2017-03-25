using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class FacilityServiceQuestionAnswerViewModel
    {
        public int FacilityServiceQuestionAnswerID { get; set; }

        public int FacilityServiceQuestionId { get; set; }

        public YesNoOption? AnswerYesNoOption { get; set; }
    }
}
