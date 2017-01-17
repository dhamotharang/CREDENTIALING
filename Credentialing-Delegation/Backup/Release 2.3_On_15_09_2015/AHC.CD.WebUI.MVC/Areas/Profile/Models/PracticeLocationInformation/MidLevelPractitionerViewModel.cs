using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class MidLevelPractitionerViewModel
    {
        public int PracticeLocationDetailID { get; set; }

        public int MidLevelPractitionerID { get; set; }

        [Required(ErrorMessage="Please select a Mid-Level Practitioner.")]
        public int ProfileID { get; set; }

        public StatusType StatusType { get; set; }

        public DateTime? ActivationDate { get; set; }

        public DateTime? DeactivationDate { get; set; }
    }
}