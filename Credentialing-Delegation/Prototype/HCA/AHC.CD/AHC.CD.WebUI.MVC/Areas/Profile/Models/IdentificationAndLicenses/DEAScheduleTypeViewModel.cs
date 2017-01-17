using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
   public class DEAScheduleTypeViewModel
    {
        public int DEAScheduleTypeID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public DEAScheduleTypeDescriptionViewModel Description { get; set; }

        public string Status { get; set; }
    }
}
