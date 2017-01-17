using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class PracticeServiceQuestionAnswerViewModel
    {
        public int PracticeServiceQuestionAnswerID { get; set; }

        #region Question

        [Required]
        public int PracticeQuestionId { get; set; }

        [ForeignKey("PracticeQuestionId")]
        public PracticeAccessibilityQuestion Question { get; set; }

        #endregion

        #region Answer

        [Required]
        public string Answer { get; private set; }

        //[NotMapped]
        //public YesNoOption AnswerYesNoOption
        //{
        //    get
        //    {
        //        return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.Answer);
        //    }
        //    set
        //    {
        //        this.Answer = value.ToString();
        //    }
        //}

        #endregion
    }
}