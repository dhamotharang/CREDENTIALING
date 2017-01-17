using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class EncounterStatusReason
    {
        [Key]
        public Nullable<long> EncounterStatusReason_PK_Id { get; set; }
        //public long Encounter_FK_Id { get; set; }
        public int EncounterStaus_FK_Id { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> Reason_FK_Id { get; set; }

        //[ForeignKey("Encounter_FK_Id")]
        //public virtual Encounter Encounter { get; set; }
        [ForeignKey("EncounterStaus_FK_Id")]
        public virtual EncounterStatus EncounterStatus { get; set; }
        [ForeignKey("Reason_FK_Id")]
        public virtual Reason Reason { get; set; }
    }
}