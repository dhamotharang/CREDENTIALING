using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class BillingServiceAdjustment
    {
        [Key]
        public long BillingAdjustment_PK_Id { get; set; }
        public Nullable<long> Billing_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string ProcedureCode { get; set; }
        public Nullable<decimal> AdjustedAmount { get; set; }

        [ForeignKey("Billing_FK_Id")]
        public virtual BillingInfo BillingInfo { get; set; }
    }
}