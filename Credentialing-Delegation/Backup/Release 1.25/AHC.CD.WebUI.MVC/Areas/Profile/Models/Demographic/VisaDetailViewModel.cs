using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class VisaDetailViewModel
    {
        public int VisaDetailID { get; set; }
        
        #region IsResidentOfUSA

        [Display(Name = "Are you a citizen of US ? *")]        
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SPECIFY)]
        public YesNoOption IsResidentOfUSAYesNoOption { get; set; }

        #endregion  

        #region IsAuthorizedToWorkInUS

        [Display(Name = "Are you authorized to work in US ? *")]
        [RequiredIf("IsResidentOfUSAYesNoOption", (int)YesNoOption.NO , ErrorMessage = ValidationErrorMessage.REQUIRED_SPECIFY)]
        public YesNoOption IsAuthorizedToWorkInUSYesNoOption { get; set; }

        #endregion

        public VisaInfoViewModel VisaInfo { get; set; }
    }
}
