using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class BillingStatus
    {
        public BillingStatus()
        {
            this.BillingInfo = new HashSet<BillingInfo>();
        }

        [Key]
        public int BillingStatusID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public virtual ICollection<BillingInfo> BillingInfo { get; set; }
    }
}