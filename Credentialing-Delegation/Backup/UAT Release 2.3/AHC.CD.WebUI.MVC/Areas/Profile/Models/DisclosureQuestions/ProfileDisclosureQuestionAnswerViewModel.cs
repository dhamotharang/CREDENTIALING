using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.DisclosureQuestions
{
    public class ProfileDisclosureQuestionAnswerViewModel
    {
        public int ProfileDisclosureQuestionAnswerID { get; set; }

        [Display(Name="Answer")]
        public YesNoOption? AnswerYesNoOption { get; set; }

        [RequiredIf("AnswerYesNoOption", (int)YesNoOption.YES, ErrorMessage = "Reason is required")]
        [Display(Name="Reason *")]
        public string Reason { get; set; }

        [Required]
        public int QuestionID { get; set; }
    }
}
