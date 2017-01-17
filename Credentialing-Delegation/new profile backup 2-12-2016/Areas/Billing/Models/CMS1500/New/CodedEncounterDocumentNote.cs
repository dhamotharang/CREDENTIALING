using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class CodedEncounterDocumentNote
    {
        [Key]
        public long CodedEncounterDocumentNote_ID { get; set; }
        public Nullable<long> CodedEncounter_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> Note_FK_Id { get; set; }
        [ForeignKey("CodedEncounter_FK_Id")]
        public virtual CodedEncounter CodedEncounter { get; set; }
        [ForeignKey("Note_FK_Id")]
        public virtual Note Note { get; set; }
    }
}