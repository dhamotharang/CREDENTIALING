using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class EncounterLog
    {
        public EncounterLog()
        {
            this.EncounterFieldLog = new HashSet<EncounterFieldLog>();
        }
        [Key]
        public long EncounterLogID { get; set; }
        public Nullable<long> Encounter_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [ForeignKey("Encounter_FK_Id")]
        public virtual Encounter Encounter { get; set; }
        public virtual ICollection<EncounterFieldLog> EncounterFieldLog { get; set; }
    }
}