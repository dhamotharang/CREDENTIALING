using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Diagnosis
    {
        public Diagnosis()
        {
            // this.Billed_ICDs = new HashSet<Billed_ICDs>();
            //this.Diagnosis1 = new HashSet<Diagnosis>();
            this.DiagnosisStatus = new HashSet<DiagnosisStatus>();
            //this.DiagnosisPointers = new HashSet<DiagnosisPointer>();
        }
        [Key]
        public long Diagnosis_PK_Id { get; set; }
        public string ICDCode { get; set; }
        public string ICDVersion { get; set; }
        public Nullable<long> DiagnosisReference_FK_Id { get; set; }
        //public string HCCCodeMappingID { get; set; }
        public List<HCCCode> HCCCodes { get; set; }
        //public Nullable<long> CodingInfo_FK_Id { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<int> AuditTypeID_FK_ID { get; set; }
        public string MemberCode { get; set; }
        [ForeignKey("AuditTypeID_FK_ID")]
        public virtual AuditType AuditType { get; set; }
        //  public virtual ICollection<Billed_ICDs> Billed_ICDs { get; set; }
        // public virtual CodedEncounter CodedEncounter { get; set; }
        //public virtual ICollection<Diagnosis> Diagnosis1 { get; set; }
        public virtual Diagnosis DiagnosisReference { get; set; }
        public virtual ICollection<DiagnosisStatus> DiagnosisStatus { get; set; }
        //public virtual ICollection<DiagnosisPointer> DiagnosisPointers { get; set; }
        
    }
}