using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class ScheduleLog
    {
        public ScheduleLog()
        {
            this.ScheduleFieldLog = new HashSet<ScheduleFieldLog>();
        }
        [Key]
        public long ScheduleLogID { get; set; }
        public Nullable<long> Schedule_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [ForeignKey("Schedule_FK_Id")]
        public virtual Schedule Schedule { get; set; }
        public virtual ICollection<ScheduleFieldLog> ScheduleFieldLog { get; set; }
    }
}