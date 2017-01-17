using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class SupervisingProviderViewModel
    {
        public int PracticeLocationDetailID { get; set; }

        public int SupervisingProviderID { get; set; }

        [Required(ErrorMessage = "Please select a Supervising Provider.")]
        public int ProfileID { get; set; }

        public StatusType StatusType { get; set; }

        public DateTime? ActivationDate { get; set; }

        public DateTime? DeactivationDate { get; set; }
    }
}