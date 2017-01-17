using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Plans.Models
{
    public class PlanListContactDetailViewModel
    {
        public int PlanContactDetailID { get; set; }

        [Display(Name = "Contact Person Name ")]
        public string ContactPersonName { get; set; }

        public ContactDetailsViewModel ContactDetail { get; set; }

        #region Status

        public StatusType? StatusType { get; set; }

        #endregion  
    }
}