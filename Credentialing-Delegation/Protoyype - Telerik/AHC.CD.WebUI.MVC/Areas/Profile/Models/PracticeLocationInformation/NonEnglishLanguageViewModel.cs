using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class NonEnglishLanguageViewModel
    {
        public int NonEnglishLanguageID { get; set; }

        public string Language { get; set; }

        public string InterpretersAvailable { get; set; }

        [Required]
        public YesNoOption InterpretersAvailableYesNoOption { get; set; }

    }
}