using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace PortalTemplate.Areas.Encounters.Models.Schedule
{
    public class ProviderSelectMemberViewModel
    {
        public List<ProviderViewModel> Provider { get; set; }
        public List<MemberViewModel> Member { get; set; }
    }
}