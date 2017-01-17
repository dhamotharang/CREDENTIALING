using AHC.CD.Entities.MasterData.Enums;
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
        public VisaDetailViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int VisaDetailID { get; set; }
        
        #region IsResidentOfUSA

        [Display(Name = "Are you a citizen of US ? *")]
        public string IsResidentOfUSA 
        {
            get
            {
                return this.IsResidentOfUSAYesNoOption.ToString();
            }
            private set
            {
                this.IsResidentOfUSAYesNoOption = (YesNoOption)Enum.Parse(typeof(YesNoOption), value);
            }
        }

        [Required(ErrorMessage = "Please specify whether you are a citizen of US.")]
        [Display(Name = "Are you a citizen of US ? *")]
        public YesNoOption IsResidentOfUSAYesNoOption { get; set; }

        #endregion  

        #region IsAuthorizedToWorkInUS

        [Display(Name = "Are you authorized to work in US ? *")]
        public string IsAuthorizedToWorkInUS
        {
            get
            {
                return this.IsAuthorizedToWorkInUSYesNoOption.ToString();
            }
            private set
            {
                this.IsAuthorizedToWorkInUSYesNoOption = (YesNoOption)Enum.Parse(typeof(YesNoOption), value);
            }
        }
        [RequiredIf("IsResidentOfUSAYesNoOption", (int)YesNoOption.NO, ErrorMessage = "Please specify whether you are authorized to work in US.")]
        [Display(Name = "Are you authorized to work in US ? *")]
        public YesNoOption IsAuthorizedToWorkInUSYesNoOption { get; set; }

        #endregion

        [RequiredIf("IsAuthorizedToWorkInUSYesNoOption", (int)YesNoOption.YES, ErrorMessage = "Please enter visa details!!")]
        public VisaInfoViewModel VisaInfo { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
