using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class EncounterDocumentNote
    {
        [Key]
        public long EncounterDocumentNote_ID { get; set; }
        public Nullable<long> EncounterDocument_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> Note_FK_Id { get; set; }

        [ForeignKey("EncounterDocument_FK_Id")]
        public virtual EncounterDocument EncounterDocument { get; set; }
        [ForeignKey("Note_FK_Id")]
        public virtual Note Note { get; set; }
    }
}