using AHC.CD.Entities.MasterData.Enums;
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
        public LanguageInfoViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int LanguageInfoID { get; set; }


        #region CanSpeakAmericanSignLanguage

        [Display(Name = "Do You Communicate in American Sign Language ?")]
        public string CanSpeakAmericanSignLanguage 
        {
            get
            {
                return this.CanSpeakAmericanSignYesNoOption.ToString();
            }
            private set
            {
                this.CanSpeakAmericanSignYesNoOption = (YesNoOption)Enum.Parse(typeof(YesNoOption), value);
            }
        }

        [Required(ErrorMessage = "Please specify whether you know American Sign Language or not.")]
        [Display(Name = "Do You Communicate in American Sign Language ?")]
        public YesNoOption CanSpeakAmericanSignYesNoOption { get; set; }

        #endregion

        public ICollection<KnownLanguageViewModel> KnownLanguages { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
