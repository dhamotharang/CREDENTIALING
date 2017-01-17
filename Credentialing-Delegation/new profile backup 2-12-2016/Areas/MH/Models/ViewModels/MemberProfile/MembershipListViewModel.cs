using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class MembershipListViewModel
    {
        public string SubscriberID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Relationship { get; set; }

        public string CoverageStartDate { get; set; }

        public string CoverageEndDate { get; set; }

        public string Preference { get; set; }
    }
}