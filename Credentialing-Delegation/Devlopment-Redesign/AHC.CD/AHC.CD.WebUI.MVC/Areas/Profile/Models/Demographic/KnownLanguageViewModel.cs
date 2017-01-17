using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class KnownLanguageViewModel
    {
        public int KnownLanguageID { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public int ProficiencyIndex { get; set; }
    }
}