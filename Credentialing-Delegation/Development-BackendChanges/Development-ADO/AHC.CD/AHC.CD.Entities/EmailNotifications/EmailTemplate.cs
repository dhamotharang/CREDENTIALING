using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.EmailNotifications
{
    public class EmailTemplate
    {
        public EmailTemplate()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int EmailTemplateID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Action { get; set; }

        #region Email Notification Type

        public string EmailNotificationType { get; private set; }

        [NotMapped]
        public EmailNotificationType? EmailNotificationTypeCategory
        {
            get
            {
                if (String.IsNullOrEmpty(this.EmailNotificationType))
                    return null;

                return (EmailNotificationType)Enum.Parse(typeof(EmailNotificationType), this.EmailNotificationType);
            }
            set
            {
                this.EmailNotificationType = value.ToString();
            }
        }

        #endregion

        #region Email Detail

        public string To { get; set; }

        public string CC { get; set; }

        public string BCC { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        #endregion

        #region Email Recurrence

        public string IsRecurrenceEnabled { get; set; }
        
        [NotMapped]
        public YesNoOption? IsRecurrenceEnabledYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsRecurrenceEnabled))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsRecurrenceEnabled);
            }
            set
            {
                this.IsRecurrenceEnabled = value.ToString();
            }
        }

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
