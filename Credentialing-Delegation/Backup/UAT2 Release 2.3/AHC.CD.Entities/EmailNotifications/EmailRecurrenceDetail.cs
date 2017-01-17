using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.EmailNotifications
{
    public class EmailRecurrenceDetail
    {
        public EmailRecurrenceDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int EmailRecurrenceDetailID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NextMailingDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? FromDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ToDate { get; set; }

        public int? IntervalFactor { get; set; }

        #region Recurrence Interval

        public string RecurrenceIntervalType { get; set; }

        [NotMapped]
        public RecurrenceIntervalType? RecurrenceIntervalTypeCategory
        {
            get
            {
                if (String.IsNullOrEmpty(this.RecurrenceIntervalType))
                    return null;

                return (RecurrenceIntervalType)Enum.Parse(typeof(RecurrenceIntervalType), this.RecurrenceIntervalType);
            }
            set
            {
                this.RecurrenceIntervalType = value.ToString();
            }
        }

        #endregion

        //public ICollection<CustomDaysInWeek> CustomDaysInWeeks { get; set; }

        #region Recurrence Scheduled

        public string IsRecurrenceScheduled { get; set; }

        [NotMapped]
        public YesNoOption? IsRecurrenceScheduledYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsRecurrenceScheduled))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsRecurrenceScheduled);
            }
            set
            {
                this.IsRecurrenceScheduled = value.ToString();
            }
        }

        #endregion

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

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
