using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using AHC.CD.Entities.MasterData.Enums;

namespace AHC.CD.Entities.Notification
{
    public class UserDashboardNotification
    {
        public UserDashboardNotification()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int UserDashboardNotificationID { get; set; }

        public string Action { get; set; }

        public string ActionPerformedByUser { get; set; }

        public string ActionPerformed { get; set; }

        public string Description { get; set; }

        #region Acknowledgement Status

        public string AcknowledgementStatus { get; private set; }

        [NotMapped]
        public AcknowledgementStatusType? AcknowledgementStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.AcknowledgementStatus))
                    return null;

                return (AcknowledgementStatusType)Enum.Parse(typeof(AcknowledgementStatusType), this.AcknowledgementStatus);
            }
            set
            {
                this.AcknowledgementStatus = value.ToString();
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
