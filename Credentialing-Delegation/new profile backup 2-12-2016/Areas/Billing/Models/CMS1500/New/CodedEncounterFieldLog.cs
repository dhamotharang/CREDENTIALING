using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class CodedEncounterFieldLog
    {
        [Key]
        public long CodedEncounterFieldLog_PK_ID { get; set; }
        public long CodedEncounterLog_FK_ID { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        [ForeignKey("CodedEncounterLog_FK_ID")]
        public virtual CodedEncounterLog CodedEncounterLog { get; set; }
    }
}