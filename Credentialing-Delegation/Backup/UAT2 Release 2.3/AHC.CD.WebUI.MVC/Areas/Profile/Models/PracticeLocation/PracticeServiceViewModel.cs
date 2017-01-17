using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class PracticeServiceViewModel
    {
        public int PracticeServiceID { get; set; }

        public ICollection<PracticeServiceQuestionAnswerViewModel> PracticeServiceQuestionAnswers { get; set; }

        #region PracticeType

        [Required]
        public int LocationPracticeTypeID { get; set; }

        #endregion

        public string AdditionalOfficeProcedures  { get; set; }
    }
}