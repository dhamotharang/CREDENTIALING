using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Facility
{
    public class FacilityLanguageViewModel
    {


        public int FacilityLanguageID { get; set; }

        public string AmericanSignLanguage { get; set; }

        
        [Display(Name = "Is American Sign Language used in the facility ?*")]
        public string AmericanSignLanguageYesNoOption { get; set; }

        [Display(Name = "Non-English Languages used in the facility")]
        public ICollection<NonEnglishLanguageViewModel> NonEnglishLanguages { get; set; }
    }
}