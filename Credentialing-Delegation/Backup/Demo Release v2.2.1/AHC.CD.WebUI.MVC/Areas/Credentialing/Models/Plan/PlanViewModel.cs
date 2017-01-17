using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Plan
{
    public class PlanViewModel
    {
        public int? PlanID { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Plan Code *")]
        public string PlanCode { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Plan Name *")]
        public string PlanName { get; set; }

        [Display(Name = "Plan Description")]
        public string PlanDescription { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Is Delegated *")]
        public bool IsDelegated { get; set; }   

        #region Plan Line Of Business

        [Display(Name = "Line Of Business")]
        public ICollection<LOBViewModel> PlanLOBs { get; set; } 

        #endregion
        
        #region Contact Details
        
        [Display(Name = "Contact Details")]
        public ICollection<PlanContactDetailViewModel> ContactDetails { get; set; } 

        #endregion

        #region Location Details

        [Display(Name = "Location")]
        public ICollection<PlanAddressViewModel> Locations { get; set; }    

        #endregion 

        //#region Plan Logo

        //public string PlanLogoPath { get; set; }    

        //[Required(ErrorMessage = "Please upload plan logo.")]
        //[PostedFileExtension(AllowedFileExtensions = "jpeg,jpg,png,bmp,PNG,JPEG,JPG,BMP", ErrorMessage = "Please select the file of type jpeg, .png, .jpg, .bmp")]
        //[PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Plan logo should be less than 10 MB of size.")]
        //public HttpPostedFileBase PlanLogoFile { get; set; }    

        //#endregion

        #region Status

        public StatusType? StatusType { get; set; }

        #endregion  
    }
}