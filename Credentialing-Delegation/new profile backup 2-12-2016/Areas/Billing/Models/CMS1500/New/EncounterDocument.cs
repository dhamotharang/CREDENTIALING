using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class EncounterDocument
    {
        public EncounterDocument()
        {
            this.EncounterDocumentNote = new HashSet<EncounterDocumentNote>();
        }

        [Key]
        public long EncounterDocumentID { get; set; }
        public Nullable<long> Encounter_FK_ID { get; set; }
        public Nullable<long> Document_FK_ID { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }

        [ForeignKey("Document_FK_ID")]
        public virtual Document Document { get; set; }
        [ForeignKey("Encounter_FK_ID")]
        public virtual Encounter Encounter { get; set; }
        public virtual ICollection<EncounterDocumentNote> EncounterDocumentNote { get; set; }
    }
}