using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class SpecialEligibilitieViewModel
    {
        public int SpecialEligibilityId { get; set; }

        public int MemberProfileId { get; set; }

        public string Indicator { get; set; }

        public string EligibilityStartDate { get; set; }

        public string EligibilityEndDate { get; set; }

        public string Status { get; set; }

        public string SourceCode { get; set; }

        public string SourceName { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedByEmail { get; set; }

        public string LastModifiedDate { get; set; }

        public string LastModifiedByEmail { get; set; }


    }
}