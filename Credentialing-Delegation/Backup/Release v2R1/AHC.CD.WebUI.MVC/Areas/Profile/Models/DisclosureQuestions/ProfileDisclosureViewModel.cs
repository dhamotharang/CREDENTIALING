using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.DisclosureQuestions
{
    public class ProfileDisclosureViewModel
    {
        public int ProfileDisclosureID { get; set; }

        public ICollection<ProfileDisclosureQuestionAnswerViewModel> ProfileDisclosureQuestionAnswers { get; set; }
    }
}
