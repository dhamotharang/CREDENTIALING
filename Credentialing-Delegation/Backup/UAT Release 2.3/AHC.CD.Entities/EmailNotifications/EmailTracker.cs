using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.EmailNotifications
{
    public class EmailTracker
    {
        public EmailTracker()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int EmailTrackerID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EmailTrackerDate { get; set; }

        #region Email Status

        public string EmailStatusType { get; set; }

        [NotMapped]
        public EmailStatusType? EmailStatusTypeCategory
        {
            get
            {
                if (String.IsNullOrEmpty(this.EmailStatusType))
                    return null;

                return (EmailStatusType)Enum.Parse(typeof(EmailStatusType), this.EmailStatusType);
            }
            set
            {
                this.EmailStatusType = value.ToString();
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
