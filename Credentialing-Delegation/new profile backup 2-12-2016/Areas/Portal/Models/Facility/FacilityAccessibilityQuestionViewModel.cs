using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Facility
{
    public class FacilityAccessibilityQuestionViewModel
    {


        public FacilityAccessibilityQuestionViewModel()
        {
            ModifiedDate = DateTime.Now;
        }

         [Display(Name = "Facility Accessibility Question Id")]
         public int FacilityAccessibilityQuestionId { get; set; }

         [Display(Name = "Title")]
         public string Title { get; set; }


         [Display(Name = "Short Title")]
         public string ShortTitle { get; set; }

          [Display(Name = "Other Handicapped Access ")]
         public string OtherHandicappedAccess { get; set; }

          [Display(Name = "Other Disability Services")]
          public string OtherDisabilityServices { get; set; }

          [Display(Name = "Other Transportation Access ")]
          public string OtherTransportationAccess { get; set; }

          [Display(Name = "Answer ")]
          public string Answer { get; set; }
       
        public int? FacilityTypeID { get; set; }
        [Display(Name = "Facility Type")]
        public FacilityTypeViewModel FacilityType { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }


        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }

    }
}