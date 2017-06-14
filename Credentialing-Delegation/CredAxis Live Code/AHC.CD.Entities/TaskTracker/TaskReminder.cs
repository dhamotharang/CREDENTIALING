using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.TaskTracker
{

    public class TaskReminder
    {
        public TaskReminder()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int TaskReminderID { get; set; }

        public string ReminderInfo { get; set; }

        public int ScheduledByID { get; set; }

        [ForeignKey("ScheduledByID")]
        public CDUser ScheduledBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ScheduledDateTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
        //Snooze time in minutes
        public int SnoozeTime { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion
    }
}
