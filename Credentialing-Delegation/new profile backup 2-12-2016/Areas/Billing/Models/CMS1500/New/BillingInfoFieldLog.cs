using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class BillingInfoFieldLog
    {

        [Key]
        public long BillingInfoFieldLog_PK_ID { get; set; }
        public Nullable<long> BillingLog_FK_ID { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        [ForeignKey("BillingLog_FK_ID")]
        public virtual BillingLog BillingLog { get; set; }
    }
}