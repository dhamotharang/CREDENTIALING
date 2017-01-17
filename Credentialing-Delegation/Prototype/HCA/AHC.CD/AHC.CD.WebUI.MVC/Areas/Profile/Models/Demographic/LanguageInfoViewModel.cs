using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class LanguageInfoViewModel
    {
        public int LanguageInfoID { get; set; }

        [Required(ErrorMessage="This field is required.")]
        [Display(Name = "Do You Communicate in American Sign Language ?")]
        public string CanSpeakAmericanSignLanguage { get; set; }

        public virtual ICollection<KnownLanguageViewModel> KnownLanguages { get; set; }
    }

    public class KnownLanguageViewModel
    {
        
        public int KnownLanguageID { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public int ProficiencyIndex { get; set; }
    }
}
