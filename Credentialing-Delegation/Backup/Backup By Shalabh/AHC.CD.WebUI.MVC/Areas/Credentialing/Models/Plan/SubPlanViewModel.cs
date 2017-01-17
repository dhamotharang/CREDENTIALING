using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models
{
    public class SubPlanViewModel
    {
        public int? SubPlanId { get; set; }

        [Display(Name = "Sub Plan Name")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        public string SubPlanName { get; set; }

        [Display(Name = "Sub Plan Code")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        public string SubPlanCode { get; set; }

        [Display(Name = "Sub Plan Description")]
        public string SubPlanDescription { get; set; }  

        public StatusType? Status { get; set; }
    }
}
