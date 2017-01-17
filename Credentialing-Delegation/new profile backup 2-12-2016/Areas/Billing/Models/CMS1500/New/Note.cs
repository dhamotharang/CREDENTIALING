using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Note
    {
        public Note()
        {
            this.BillingNote = new HashSet<BillingNote>();
            this.CodedEncounterDocumentNote = new HashSet<CodedEncounterDocumentNote>();
            this.CodedEncounterNote = new HashSet<CodedEncounterNote>();
            this.EncounterDocumentNote = new HashSet<EncounterDocumentNote>();
            this.EncounterNote = new HashSet<EncounterNote>();
            this.ScheduleNote = new HashSet<ScheduleNote>();
        }
        [Key]
        public long Notes_PK_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<long> NoteCategory_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }

        public virtual ICollection<BillingNote> BillingNote { get; set; }
        public virtual ICollection<CodedEncounterDocumentNote> CodedEncounterDocumentNote { get; set; }
        public virtual ICollection<CodedEncounterNote> CodedEncounterNote { get; set; }
        public virtual ICollection<EncounterDocumentNote> EncounterDocumentNote { get; set; }
        public virtual ICollection<EncounterNote> EncounterNote { get; set; }
        [ForeignKey("NoteCategory_FK_Id")]
        public virtual NoteCategory NoteCategory { get; set; }
        public virtual ICollection<ScheduleNote> ScheduleNote { get; set; }
    }
}