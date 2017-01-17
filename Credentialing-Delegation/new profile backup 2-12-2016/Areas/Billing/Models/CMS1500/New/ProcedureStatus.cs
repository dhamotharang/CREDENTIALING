using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class ProcedureStatus
    {
        [Key]
        public int ProcedureStatus_PK_Id { get; set; }
        public int Status_FK_ID { get; set; }
        public long ProcedureInfo_FK_ID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<long> ProcedureInfoReason_FK_ID { get; set; }
        public int AuditType_FK_ID { get; set; }
        [ForeignKey("AuditType_FK_ID")]
        public AuditType AuditType { get; set; }
        [ForeignKey("ProcedureInfo_FK_ID")]
        public Procedure Procedure { get; set; }
        [ForeignKey("ProcedureInfoReason_FK_ID")]
        public ProcedureReason ProcedureReason { get; set; }
        [ForeignKey("Status_FK_ID")]
        public Status Status { get; set; }
    }
}
