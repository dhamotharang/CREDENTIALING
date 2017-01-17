using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class PracticeNonEnglishLanguageViewModel
    {
        public int PracticeNonEnglishLanguageID { get; set; }

        #region AmericanSignLanguage

        [Required]
        [Display(Name = "Do you communicate American Sign Language? *")]
        public string AmericanSignLanguage { get; private set; }

        //[NotMapped]
        //public YesNoOption AmericanSignLanguageYesNoOption
        //{
        //    get
        //    {
        //        return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.AmericanSignLanguage);
        //    }
        //    set
        //    {
        //        this.AmericanSignLanguage = value.ToString();
        //    }
        //}

        #endregion

        #region InterpretersAvailable

        [Required]
        [Display(Name = "Interpreters Available?    *")]
        public string InterpretersAvailable { get; private set; }

        //[NotMapped]
        //public YesNoOption InterpretersAvailableYesNoOption
        //{
        //    get
        //    {
        //        return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.InterpretersAvailable);
        //    }
        //    set
        //    {
        //        this.InterpretersAvailable = value.ToString();
        //    }
        //}

        #endregion

        public ICollection<NonEnglishLanguage> NonEnglishLanguages { get; set; }

        public ICollection<InterpretedLanguage> InterpretedLanguages { get; set; }

    }

    public class NonEnglishLanguage
    {
        public int NonEnglishLanguageID { get; set; }

        public string Language { get; set; }
    }

    public class InterpretedLanguage
    {
        public int InterpretedLanguageID { get; set; }

        public string Language { get; set; }
    }

}