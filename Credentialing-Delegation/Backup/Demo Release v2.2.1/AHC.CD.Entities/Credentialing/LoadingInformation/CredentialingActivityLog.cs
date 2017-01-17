using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.LoadingInformation
{
    public class CredentialingActivityLog
    {
        public CredentialingActivityLog()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CredentialingActivityLogID { get; set; }

        public int? ActivityByID { get; set; }
        [ForeignKey("ActivityByID")]
        public CDUser ActivityBy { get; set; }

        #region Activity Status

        public string ActivityStatus { get; private set; }

        [NotMapped]
        public ActivityStatusType? ActivityStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.ActivityStatus))
                    return null;

                if (this.ActivityStatus.Equals("Not Available"))
                    return null;

                return (ActivityStatusType)Enum.Parse(typeof(ActivityStatusType), this.ActivityStatus);
            }
            set
            {
                this.ActivityStatus = value.ToString();
            }
        }

        #endregion

        #region Activity Type

        public string Activity { get; private set; }

        [NotMapped]
        public ActivityType? ActivityType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Activity))
                    return null;

                if (this.Activity.Equals("Not Available"))
                    return null;

                return (ActivityType)Enum.Parse(typeof(ActivityType), this.Activity);
            }
            set
            {
                this.Activity = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
