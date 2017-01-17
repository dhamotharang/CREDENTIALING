using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.ProfessionalAffiliationViewModel
{
    public class ProfessionalAffiliationViewModel
    {
        public string ProfessionalAffiliationID { get; set; }

        [Display(Name = "ORGANIZATION NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string OrganizationName { get; set; }


        [Display(Name = "START DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StartDate { get; set; }

        [Display(Name = "END DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EndDate { get; set; }

        [Display(Name = "POSITION OFFICE HELD")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PositionOfficeHeld { get; set; }

        [Display(Name = "MEMBER/APPLICANT")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MemberApplicant { get; set; }

        



    }
}