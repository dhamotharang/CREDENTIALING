using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Procedure
    {
        public Procedure()
        {
            // this.Billed_CPTs = new HashSet<Billed_CPTs>();
            this.ClaimAdjustmentServiceLines = new HashSet<ClaimAdjustmentServiceLine>();
           // this.DiagnosisPointers = new HashSet<DiagnosisPointer>();
            //this.Procedure1 = new HashSet<Procedure>();
            this.ProcedureStatus = new List<ProcedureStatus>();
        }
        [Key]
        public long Procedure_PK_Id { get; set; }
        public string ProcedureCodeType { get; set; }
        public string Modifier1 { get; set; }
        public string Modifier2 { get; set; }
        public string Modifier3 { get; set; }
        public string Modifier4 { get; set; }
        public Nullable<long> CodingInfo_FK_Id { get; set; }
        public Nullable<int> Code { get; set; }
        public string Amount { get; set; }
        public Nullable<long> ProcedureReference_FK_Id { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }
        public Nullable<System.DateTime> LastUpdatedBy { get; set; }
        public Nullable<int> ProcedureStatus_FK_Id { get; set; }
        public Nullable<long> NDCData_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> AuditType_FK_ID { get; set; }
        public string MemberCode { get; set; }
        [ForeignKey("AuditType_FK_ID")]
        public AuditType AuditType { get; set; }
        //  public virtual ICollection<Billed_CPTs> Billed_CPTs { get; set; }
        public ICollection<ClaimAdjustmentServiceLine> ClaimAdjustmentServiceLines { get; set; }
        [ForeignKey("CodingInfo_FK_Id")]
        public CodedEncounter CodedEncounter { get; set; }
        public IList<int> DiagnosisPointers { get; set; }
        [ForeignKey("NDCData_FK_Id")]
        public NDCData NDCData { get; set; }
        //   public  ProcedureCodeType ProcedureCodeType { get; set; }
        //public virtual ICollection<Procedure> Procedure1 { get; set; }
        [ForeignKey("ProcedureReference_FK_Id")]
        public Procedure ProcedureReference { get; set; }
        public IList<ProcedureStatus> ProcedureStatus { get; set; }

        public float Units { get; set; }
        
    }
}