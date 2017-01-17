using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Plans.Models
{
    public class SubPlansViewModel
    {
        public int? SubPlanId { get; set; }

        [Display(Name = "Sub Plan Name")]
        public string SubPlanName { get; set; }

        [Display(Name = "Sub Plan Code")]
        public string SubPlanCode { get; set; }

        [Display(Name = "Sub Plan Description")]
        public string SubPlanDescription { get; set; }

        public StatusType? Status { get; set; }
    }
}