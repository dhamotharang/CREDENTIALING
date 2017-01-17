using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class BillingNote
    {
        [Key]
        public long BillingNote_PK_Id { get; set; }
        public Nullable<long> BillingInfo_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<long> Note_FK_ID { get; set; }

        [ForeignKey("BillingInfo_FK_Id")]
        public virtual BillingInfo BillingInfo { get; set; }
        [ForeignKey("Note_FK_ID")]
        public virtual Note Note { get; set; }
    }
}