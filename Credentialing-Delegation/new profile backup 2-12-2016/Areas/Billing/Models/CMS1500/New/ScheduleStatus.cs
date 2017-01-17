using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class ScheduleStatus
    {
        public ScheduleStatus()
        {
            //this.Schedule = new HashSet<Schedule>();
            this.ScheduleStatusReason = new HashSet<ScheduleStatusReason>();
        }
        [Key]
        public int ScheduleStatus_PK_Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }

        //public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<ScheduleStatusReason> ScheduleStatusReason { get; set; }
    }
}
