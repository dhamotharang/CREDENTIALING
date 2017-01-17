using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class ScheduleFieldLog
    {
        [Key]
        public long ScheduleFieldLog_PK_ID { get; set; }
        public Nullable<long> ScheduleLog_FK_ID { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        [ForeignKey("ScheduleLog_FK_ID")]
        public virtual ScheduleLog ScheduleLog { get; set; }
    }
}