using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim
{
    public class ProviderMemberSelectViewModel
    {
        public List<MemberResultViewModel> MemberResult { get; set; }
        public List<ProviderResultViewModel> ProviderResult { get; set; }
    }
}