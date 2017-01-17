using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.WorkHistory
{
    public class PublicHealthServiceViewModel
    {
        public int PublicHealthServiceID { get; set; }

        [Required]
        [Display(Name = "Have You Performed public health Service? *")]
        public bool PerformedPublicHealthService { get; set; }

        [Display(Name = "Last Location")]
        public string LastLocation { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Begining Date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Ending Date")]
        public DateTime EndDate { get; set; }
    }
}