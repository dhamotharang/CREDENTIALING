using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class CodingReason
    {
        [Key]
        public long CodingReason_PK_Id { get; set; }
        public Nullable<int> Reason_FK_Id { get; set; }
        public Nullable<long> CodedEncounter_FK_Id { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        [ForeignKey("CodedEncounter_FK_Id")]
        public CodedEncounter CodedEncounter { get; set; }
        [ForeignKey("Reason_FK_Id")]
        public Reason Reason { get; set; }
    }
}