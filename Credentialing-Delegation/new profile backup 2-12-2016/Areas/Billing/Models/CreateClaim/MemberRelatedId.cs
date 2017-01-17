using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim
{
    public class MemberRelatedId
    {
        public string MemberId { get; set; }

        public int BillingProviderId { get; set; }

        public int FacilityId { get; set; }

        public int RenderingProviderId { get; set; }

        public int RefferingProviderId { get; set; }

        public int SupervisingProviderId { get; set; }
    }
}