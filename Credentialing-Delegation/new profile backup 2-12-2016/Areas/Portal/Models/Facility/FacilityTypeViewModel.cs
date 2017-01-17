using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Facility
{
    public class FacilityTypeViewModel
    {
        public FacilityTypeViewModel()
        {
            ModifiedDate = DateTime.Now;
        }

        [Display(Name = "Facility Type ID")]
        public int FacilityTypeID { get; set; }

        [Display(Name = "Facility Type")]
        public string Name { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }


    }
}