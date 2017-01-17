using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.SummaryViewModel
{
    public class ProfileStatusViewModel
    {
        public string LastPsvDATE { get; set; }
        public string ProfileStatus { get; set; }
        public int ActiveContracts { get; set; }
        public int InActiveContracts { get; set; }
        public int ProfileCompletionPercen { get; set; }
    }
}