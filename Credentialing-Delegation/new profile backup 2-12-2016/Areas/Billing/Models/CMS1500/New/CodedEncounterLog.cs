using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class CodedEncounterLog
    {
        public CodedEncounterLog()
        {
            this.CodedEncounterFieldLogs = new HashSet<CodedEncounterFieldLog>();
        }
        [Key]
        public long CodedEncounterLog_PK_Id { get; set; }
        public Nullable<long> CodedEncounter_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [ForeignKey("CodedEncounter_FK_Id")]
        public virtual CodedEncounter CodedEncounter { get; set; }
        public ICollection<CodedEncounterFieldLog> CodedEncounterFieldLogs { get; set; }
    }
}