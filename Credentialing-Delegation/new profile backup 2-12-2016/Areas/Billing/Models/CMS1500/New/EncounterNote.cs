using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class EncounterNote
    {
        [Key]
        public long EncountersNote_PK_Id { get; set; }
        public Nullable<long> Notes_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> Encounter_FK_Id { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }

        [ForeignKey("Encounter_FK_Id")]
        public virtual Encounter Encounter { get; set; }
        public virtual Note Note { get; set; }
    }
}