using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class ContactScheduleViewModel
    {
        public int ContactScheduleId { get; set; }

        public string PreferredDayOfContact { get; set; }

        public string PreferredTimeOfContactFrom { get; set; }

        public string PreferredTimeOfContactTo { get; set; }

        public string PreferredContactMedium { get; set; }

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