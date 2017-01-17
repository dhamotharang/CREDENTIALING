using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Schedule
    {
        public Schedule()
        {
            this.Complaint = new HashSet<Complaint>();
            this.EncounteredSchedule = new HashSet<EncounteredSchedule>();
            this.ScheduleNote = new HashSet<ScheduleNote>();
            this.PreviousSchedules = new HashSet<Schedule>();
            // this.ScheduleStatusReason = new HashSet<ScheduleStatusReason>();
        }
        [Key]
        public long Schedule_PK_Id { get; set; }
        public long RenderingProvider_FK_Id { get; set; }
        public Nullable<long> RefferingProvider_FK_Id { get; set; }
        public Nullable<long> OrderingPhysican_FK_Id { get; set; }
        public Nullable<long> BillingProvider_FK_Id { get; set; }
        public Nullable<long> SupervisingProvider_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public Nullable<long> Schedule_FK_Id { get; set; }
        public long Facility_FK_Id { get; set; }
        public int ScheduleStatus_FK_Id { get; set; }
        public string PrimarySubscriberCode { get; set; }
        public Nullable<System.TimeSpan> BookingTime { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<int> ScheduleCategory_FK_Id { get; set; }
        public string ScheduleCode { get; set; }
        public System.TimeSpan AppointmentTimeStart { get; set; }
        public System.TimeSpan? AppointmentTimeEnd { get; set; }
        public System.DateTime AppointmentDate { get; set; }
        public long Member_FK_Id { get; set; }
        //public Nullable<int> StatusType_FK_Id { get; set; }

        public virtual ICollection<Complaint> Complaint { get; set; }
        public virtual ICollection<EncounteredSchedule> EncounteredSchedule { get; set; }

        public Facility Facility { get; set; }
        public Member Member { get; set; }
        public Provider RenderingProvider { get; set; }
        public Provider RefferingProvider { get; set; }
        public Provider OrderingPhysican { get; set; }
        public Provider SupervisingProvider { get; set; }
        public ScheduleCategory ScheduleCategory { get; set; }
        public ICollection<ScheduleNote> ScheduleNote { get; set; }
        public ICollection<Schedule> PreviousSchedules { get; set; }
        public Schedule PreviousSchedule { get; set; }
        public ScheduleStatus ScheduleStatus { get; set; }
        public ICollection<ScheduleLog> ScheduleLog { get; set; }
        public Provider BillingProvider { get; set; }
    }
}