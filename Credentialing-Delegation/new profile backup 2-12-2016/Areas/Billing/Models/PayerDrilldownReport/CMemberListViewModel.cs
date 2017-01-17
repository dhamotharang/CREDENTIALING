using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.PayerDrilldownReport
{
    public class CMemberListViewModel
    {
        public int ID { get; set; }

        public string MemberName { get; set; }

        public string ClaimSubmitted { get; set; }

        public string ClaimAccepted { get; set; }

        public string ClaimRejected { get; set; }

        public string ClaimPending { get; set; }
    }
}