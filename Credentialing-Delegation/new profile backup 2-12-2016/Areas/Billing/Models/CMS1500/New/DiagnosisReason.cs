using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class DiagnosisReason
    {
        [Key]
        public long DiagnosisInfoReason_PK_Id { get; set; }
        public Nullable<int> Reason_FK_Id { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }

        public virtual Reason Reason { get; set; }
    }
}