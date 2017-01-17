using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Facility
{
    public class FacilityServiceQuestionViewModel
    {


        public FacilityServiceQuestionViewModel()
        {
            ModifiedDate = DateTime.Now;
        }
        [Display(Name = "Facility Service Question ID")]
        public int FacilityServiceQuestionID { get; set; }


        [Display(Name = "Title")]
        public string Title { get; set; }


        [Display(Name = "Short Title")]
        public string ShortTitle { get; set; }


        [Display(Name = "Status")]
        public string Status { get; set; }


        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }
    }
}