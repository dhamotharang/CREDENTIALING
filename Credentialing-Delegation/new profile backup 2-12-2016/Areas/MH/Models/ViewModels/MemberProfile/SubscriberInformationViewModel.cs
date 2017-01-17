using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class SubscriberInformationViewModel
    {
        public int SubscriberId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string Relationship { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string Phone { get; set; }

        public string SubscriberID { get; set; }

        public string CoverageStartDate { get; set; }

        public string CoverageEndDate { get; set; }
    }
}