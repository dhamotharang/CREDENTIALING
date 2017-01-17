using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class EncounteredSchedule
    {
        [Key]
        public long EncounteredSchedule_PK_Id { get; set; }
        public Nullable<long> Encounter_FK_Id { get; set; }
        public Nullable<long> Schedule_FK_Id { get; set; }

        [ForeignKey("Encounter_FK_Id")]
        public virtual Encounter Encounter { get; set; }
        [ForeignKey("Schedule_FK_Id")]
        public virtual Schedule Schedule { get; set; }
    }
}