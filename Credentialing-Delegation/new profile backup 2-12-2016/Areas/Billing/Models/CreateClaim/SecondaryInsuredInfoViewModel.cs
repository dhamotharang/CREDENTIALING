using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim
{
    public class SecondaryInsuredInfoViewModel
    {
        public string SubscriberFirstName { get; set; }

        public string SubscriberMiddleName { get; set; }

        public string SubscriberLastName { get; set; }

        public string PolicyNumber { get; set; }

        public string PlanName { get; set; }

        public string PatientAccountNumber{ get; set; }
    }
}