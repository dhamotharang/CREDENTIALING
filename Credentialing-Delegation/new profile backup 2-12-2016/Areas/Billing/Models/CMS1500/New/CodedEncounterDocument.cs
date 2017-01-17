using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class CodedEncounterDocument
    {
        public CodedEncounterDocument()
        {
            this.DocumentNotes = new List<EncounterDocumentNote>();
        }
        [Key]
        public long CodingDocument_PK_ID { get; set; }
        public Nullable<long> CodedEncounter_FK_ID { get; set; }
        public Nullable<long> Document_FK_ID { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public List<EncounterDocumentNote> DocumentNotes { get; set; }
    }
}
