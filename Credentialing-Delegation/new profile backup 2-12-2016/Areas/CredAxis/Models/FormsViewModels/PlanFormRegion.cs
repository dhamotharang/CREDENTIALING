using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.FormsViewModels
{
    public class PlanFormRegion
    {
        [Key]
        public int PlanFormRegionID { get; set; }

        [Display(Name = "Region")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Region { get; set; }
    }
}