using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class DEAScheduleTypeDescriptionViewModel
    {
        public int DEAScheduleTypeDescriptionID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string TypeName { get; set; }

        public bool isChecked { get; set; }

        public string Status { get; set; }
    }
}
