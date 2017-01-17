using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Encounter
    {
        public Encounter()
        {
            this.ClaimDate = new HashSet<ClaimDate>();
            this.CodedEncounter = new HashSet<CodedEncounter>();
            this.EncounterDocument = new HashSet<EncounterDocument>();
            this.EncounterLog = new HashSet<EncounterLog>();
            this.EncounterNote = new HashSet<EncounterNote>();
            this.PreviousEncounters = new HashSet<Encounter>();
            this.EncounterStatusReason = new HashSet<EncounterStatusReason>();
            this.EncounteredSchedule = new List<EncounteredSchedule>();
          //  this.PatientCondition = new HashSet<PatientCondition>();
        }
        [Key]
        public long Encounter_PK_Id { get; set; }
        public Nullable<long> Encounter_FK_Id { get; set; }
        public string EncounterType { get; set; }
        public int EncounterStatus_FK_Id { get; set; }
        public string PlaceOf { get; set; }
        public Nullable<DateTime> DateOf_From { get; set; }
        public Nullable<DateTime> DateOf_To { get; set; }
        public System.TimeSpan CheckInTime { get; set; }
        public System.TimeSpan CheckOutTime { get; set; }
        public long DurationOfVisit { get; set; }
        public Nullable<System.DateTime> NextAppointmentDate { get; set; }
        public long TypeID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<int> EncounterCategoryCode { get; set; }
        public Nullable<long> PatientConditionRelation_FK_Id { get; set; }
        public string EncounterUniqueCode { get; set; }

        public virtual ICollection<ClaimDate> ClaimDate { get; set; }
        public virtual ICollection<CodedEncounter> CodedEncounter { get; set; }

        [ForeignKey("EncounterCategoryCode")]
        public virtual EncounterCategory EncounterCategory { get; set; }
        public virtual ICollection<EncounterDocument> EncounterDocument { get; set; }
        public List<EncounteredSchedule> EncounteredSchedule { get; set; }
        public virtual ICollection<EncounterLog> EncounterLog { get; set; }
        public virtual ICollection<EncounterNote> EncounterNote { get; set; }
        public virtual ICollection<Encounter> PreviousEncounters { get; set; }
        public List<ClaimDate> ClaimDates { get; set; }
        [ForeignKey("Encounter_FK_Id")]
        public virtual Encounter PreviousEncounter { get; set; }
        [ForeignKey("EncounterStatus_FK_Id")]
        public virtual EncounterStatus EncounterStatus { get; set; }
        public virtual ICollection<EncounterStatusReason> EncounterStatusReason { get; set; }
        public virtual PatientCondition PatientCondition { get; set; }
    }
}