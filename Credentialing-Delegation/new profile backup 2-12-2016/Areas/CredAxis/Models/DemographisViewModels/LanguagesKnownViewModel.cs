using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DemographisViewModels
{
    public class LanguagesKnownViewModel
    {
        [Key]
        public int? LanguagesKnownId { get; set; }

        [Display(Name = "Do You Communicate in American Sign Language ?")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool CommunicateAmericanLanguage { get; set; }


        [Display(Name = "Non- English Languages you are fluent (in order of proficiency)")]
        [DisplayFormat(NullDisplayText = "-")]
        public List<NonEnglishLangViewModel> NonEnglishLanguage { get; set; }

    }
}