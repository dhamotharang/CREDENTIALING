using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class OpenPracticeStatusViewModel
    {
        public int OpenPracticeStatusID { get; set; }

        //public ICollection<PracticeOpenStatusQuestionAnswerViewModel> PracticeQuestionAnswers { get; set; }

        [Display(Name = "If any of the above information varies by plan, Explain")]
        public string AnyInformationVariesByPlan { get; set; }

        [Display(Name="Minimum")]
        public int MinimumAge { get; set; }

        [Display(Name = "Maximum")]
        public int MaximumAge { get; set; }

        [Display(Name = "List other limitations")]
        public string OtherLimitations { get; set; }

        #region GenderLimitation

        [Display(Name = "Gender Limitations")]
        public string GenderLimitation { get; private set; }

        //[NotMapped]
        //public GenderLimitationType GenderLimitationType
        //{
        //    get
        //    {
        //        return (GenderLimitationType)Enum.Parse(typeof(GenderLimitationType), this.GenderLimitation);
        //    }
        //    set
        //    {
        //        this.GenderLimitation = value.ToString();
        //    }
        //}

        #endregion
    }
}