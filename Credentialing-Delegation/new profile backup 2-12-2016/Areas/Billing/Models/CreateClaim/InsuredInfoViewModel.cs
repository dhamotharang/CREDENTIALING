using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim
{
    public class InsuredInfoViewModel
    {
        public string SubscriberFirstName { get; set; }

        public string SubscriberMiddleName { get; set; }

        public string SubscriberLastName { get; set; }

        public DateTime? DateOfBirth{ get; set; }

        public string Gender { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City{ get; set; }

        public string State { get; set; }

        public string Zipcode { get; set; }

        public string Phone { get; set; }

        public string InsuredId { get; set; }

        public string PolicyNumber { get; set; }

        public string PlanName { get; set; }
    }
}