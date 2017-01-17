using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Member
{
    public class MemberViewModel
    {
        public string AccessMemberID { get; set; }

        public string MemberFullName { get; set; }

        public string PlanName { get; set; }

        public string SubscriberID { get; set; }
    }
}