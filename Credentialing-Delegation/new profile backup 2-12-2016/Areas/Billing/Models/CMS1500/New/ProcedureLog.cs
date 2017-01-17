using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class ProcedureLog
    {
        [Key]
        public long ProcedureInfoLog_PK_Id { get; set; }
        public Nullable<long> CodedEncounter_FK_Id { get; set; }
        public string Modifier1 { get; set; }
        public string Modifier2 { get; set; }
        public string Modifier3 { get; set; }
        public string Modifier4 { get; set; }
        public Nullable<int> FeeScheduleID { get; set; }
        public string LineNote { get; set; }
        public string NDCCode { get; set; }
        public string NDCUnitPrice { get; set; }
        public string NDCQlfr { get; set; }
        public string NDCQuantityQlfr { get; set; }
        public string ProcedureCodeType { get; set; }
        public string AuditType { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        [ForeignKey("CodedEncounter_FK_Id")]
        public virtual CodedEncounter CodedEncounter { get; set; }
    }
}