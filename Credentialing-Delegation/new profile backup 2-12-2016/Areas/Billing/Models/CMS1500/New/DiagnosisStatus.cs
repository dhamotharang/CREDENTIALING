using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class DiagnosisStatus
    {
        [Key]
        public int DiagnosisInfoStatus_PK_Id { get; set; }
        public int StatusID_FK_ID { get; set; }
        public long DiagnosisInfoID_FK_ID { get; set; }
        public Nullable<long> DiagnosisInfoReasonID_FK_ID { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public int AuditType_FK_ID { get; set; }
        [ForeignKey("AuditType_FK_ID")]
        public virtual AuditType AuditType { get; set; }
        [ForeignKey("DiagnosisInfoID_FK_ID")]
        public virtual Diagnosis Diagnosis { get; set; }
        //public virtual MyProperty { get; set; }.
        [ForeignKey("DiagnosisInfoReasonID_FK_ID")]
        public virtual DiagnosisReason DiagnosisReason { get; set; }
        public virtual Status Status { get; set; }
    }
}