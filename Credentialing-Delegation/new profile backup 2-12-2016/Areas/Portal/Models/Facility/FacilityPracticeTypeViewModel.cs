using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Facility
{
    public class FacilityPracticeTypeViewModel
    {


        public FacilityPracticeTypeViewModel()
        {
            ModifiedDate = DateTime.Now;
        }

        [Display(Name = "FacilityPracticeTypeID")]
        public int FacilityPracticeTypeID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }


        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }
    
    
    }
}