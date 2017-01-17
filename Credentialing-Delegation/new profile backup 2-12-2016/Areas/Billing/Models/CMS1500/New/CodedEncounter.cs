using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class CodedEncounter
    {

        public CodedEncounter()
        {
            //this.BilledEncounters = new HashSet<BilledEncounter>();
            //this.BillingInfoes = new HashSet<BillingInfo>();
            //this.CodedEncounterDocumentNotes = new HashSet<CodedEncounterDocumentNote>();
            //this.CodedEncounter1 = new HashSet<CodedEncounter>();
            this.CodedEncounterLogs = new List<CodedEncounterLog>();
            this.CodedEncounterNotes = new List<CodedEncounterNote>();
            this.CodingReasons = new List<CodingReason>();
            this.Diagnosis = new List<Diagnosis>();
            this.ProcedureLogs = new List<ProcedureLog>();
            this.Procedures = new List<Procedure>();
        }
        [Key]
        public long CodedEncounter_PK_Id { get; set; }
        public Nullable<long> Encounter_FK_Id { get; set; }
        public System.DateTime DateofCreation { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CodingStatus_FK_Id { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<long> CodedEncounterReference_FK_Id { get; set; }
        public string CodingControlNo { get; set; }

        public string ICDCodeVersion { get; set; }

        public bool isICD10 { get; set; }
        //public virtual ICollection<BilledEncounter> BilledEncounters { get; set; }
        //public virtual ICollection<BillingInfo> BillingInfoes { get; set; }
        [ForeignKey("Encounter_FK_Id")]
        public Encounter Encounter { get; set; }
        public List<CodedEncounterDocument> CodedEncounterDocument { get; set; }
        //public virtual ICollection<CodedEncounterDocumentNote> CodedEncounterDocumentNotes { get; set; }
        //public virtual ICollection<CodedEncounter> CodedEncounter1 { get; set; }
        [ForeignKey("CodedEncounterReference_FK_Id")]
        public CodedEncounter CodedEncounterReference { get; set; }
        public CodedEncounterStatus CodedEncounterStatus { get; set; }
        public IList<CodedEncounterLog> CodedEncounterLogs { get; set; }
        public IList<CodedEncounterNote> CodedEncounterNotes { get; set; }
        public IList<CodingReason> CodingReasons { get; set; }
        public IList<Diagnosis> Diagnosis { get; set; }
        public IList<ProcedureLog> ProcedureLogs { get; set; }
        public IList<Procedure> Procedures { get; set; }
        public Signature SignaturesInfo { get; set; }
    }
}