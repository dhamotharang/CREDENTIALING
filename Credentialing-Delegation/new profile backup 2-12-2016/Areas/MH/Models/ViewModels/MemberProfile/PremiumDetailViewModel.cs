using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class PremiumDetailViewModel
    {
        public int PremiumDetailId { get; set; }

        public string Status { get; set; }

        public string TimeStamp { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }

    }
}