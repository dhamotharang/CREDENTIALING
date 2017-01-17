using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class DisabilitiesViewModel
    {
        public int DisabilityId { get; set; }

        public string Description { get; set; }

        public string DisabilityCategoryCode { get; set; }

        public string DisabilityCategoryName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Status { get; set; }

        public string SourceCode { get; set; }

        public string SourceName { get; set; }

        public string TimeStamp { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }

    }
}