using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.DisclosureQuestions
{
    public class ProviderDisclosureQuestionAnswerViewModel
    {
        public int ProviderDisclosureQuestionAnswerID { get; set; }

        [Required]
        [Display(Name="Answer")]
        public string ProviderDisclosureAnswer { get; private set; }

        [Display(Name = "Reason")]
        public string Reason { get; set; }

        [Required]
        public int ProfileDisclosureQuestionID { get; set; }

        public string ProfileDisclosureQuestion { get; set; }
    }
}
