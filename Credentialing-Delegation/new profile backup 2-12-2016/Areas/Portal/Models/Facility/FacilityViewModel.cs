using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Facility
{
    public class FacilityViewModel
    {
        [Display(Name = "Facility ID")]
        public DateTime FacilityID { get; set; }

        [Display(Name = "Tax ID")]
        public DateTime TaxID { get; set; }

        [Display(Name = "Org NPI")]
        public DateTime OrganizationNPI { get; set; }

        [Display(Name = "Legal Entity")]
        public DateTime LegalEntity { get; set; }

        public FacilitityGeneralInfoViewModel FacilitityGeneralInfo { get; set; }
        public FacilityAccessibilityQuestionViewModel FacilityAccessibilityQuestion { get; set; }
        public FacilityPracticeTypeViewModel FacilityPracticeType { get; set; }
        public FacilityServiceQuestionViewModel FacilityServiceQuestion { get; set; }
        public FacilityTypeViewModel FacilityType { get; set; }
        public FacilityServiceViewModel FacilityService { get; set; }
     
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }


    }
}