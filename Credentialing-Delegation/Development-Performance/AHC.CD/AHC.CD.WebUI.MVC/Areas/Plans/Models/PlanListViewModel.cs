using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Plan;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Plans.Models
{
    public class PlanListViewModel
    {
        public int? PlanID { get; set; }

        [Display(Name = "Plan Code")]
        public string PlanCode { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Plan Name *")]
        public string PlanName { get; set; }

        [Display(Name = "Plan Description")]
        public string PlanDescription { get; set; }

        #region Delegated

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Delegated? *")]
        public IsDelegated? IsDelegated { get; set; }

        #endregion

        #region Plan Line Of Business

        [Display(Name = "Line Of Business")]
        public ICollection<PlanListLOBViewModel> PlanLOBs { get; set; }

        #endregion

        #region Contact Details

        [Display(Name = "Contact Details")]
        public ICollection<PlanListContactDetailViewModel> ContactDetails { get; set; }

        #endregion

        #region Location Details

        [Display(Name = "Location")]
        public ICollection<PlanListAddressViewModel> Locations { get; set; }

        #endregion

        #region Plan Logo

        public string PlanLogoPath { get; set; }

        [PostedFileExtension(AllowedFileExtensions = "jpeg,jpg,png,bmp,PNG,JPEG,JPG,BMP", ErrorMessage = "Please select the file of type jpeg, .png, .jpg, .bmp")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Plan logo should be less than 10 MB of size.")]
        public HttpPostedFileBase PlanLogoFile { get; set; }

        #endregion

        #region Status

        public StatusType? StatusType { get; set; }

        #endregion  
    }
}