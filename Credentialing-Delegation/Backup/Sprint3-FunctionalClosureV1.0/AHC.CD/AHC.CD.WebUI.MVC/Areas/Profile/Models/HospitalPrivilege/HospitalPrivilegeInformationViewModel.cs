using AHC.CD.Entities.MasterData.Enums;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege
{
    public class HospitalPrivilegeInformationViewModel
    {
        public int HospitalPrivilegeInformationID { get; set; }

        #region HasHospitalPrivilege        

        [Required(ErrorMessage = "Please specify do you have any hospital privileges ?")]
        [Display(Name = "Do You Have Hospital Privileges? *")]
        public YesNoOption HospitalPrivilegeYesNoOption { get; set; }
        
        #endregion        
        [RequiredIf("HospitalPrivilegeYesNoOption", (int)YesNoOption.NO, ErrorMessage = "Please specify the type of admitting arrangements you have.")]
        [Display(Name = "If You Do Not Admit Patients, What Type Of Admitting Arrangements Do You Have? *")]
        [StringLength(500, MinimumLength = 0, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string OtherAdmittingArrangements { get; set; }
    }
}