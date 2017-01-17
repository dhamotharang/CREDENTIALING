using System;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Portal.Models.Facility
{
    public class FacilityServiceViewModel
    {

        public FacilityServiceViewModel()
        {
            ModifiedDate = DateTime.Now;
        }
        [Display(Name = "Facility Service ID")]
        public int FacilityServiceID { get; set; }


        [Display(Name = "Facility Service")]
        public string Title { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }


        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }

    }

}