using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.EmailNotifications
{
    public class EmailInfo
    {
        public EmailInfo()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int EmailInfoID { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        #region Is Recurrence Enabled

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

        #endregion

        #region Email Notification Type

        public string EmailNotificationType { get; set; }

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

        public ICollection<EmailRecipientDetail> EmailRecipients { get; set; }

        public EmailRecurrenceDetail EmailRecurrenceDetail { get; set; }

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
