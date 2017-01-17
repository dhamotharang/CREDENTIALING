using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
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

        #region CanSpeakAmericanSignLanguage

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SPECIFY)]
        [Display(Name = "Do You Communicate in American Sign Language ? *")]
        public YesNoOption CanSpeakAmericanSignYesNoOption { get; set; }

        #endregion

        public ICollection<KnownLanguageViewModel> KnownLanguages { get; set; }
    }
}
