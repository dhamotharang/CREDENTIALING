using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.LoadingInformation
{
    public class TimelineActivity
    {
        public TimelineActivity()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int TimelineActivityID { get; set; }

        public int? ActivityByID { get; set; }
        [ForeignKey("ActivityByID")]
        public CDUser ActivityBy { get; set; }

        public string Activity { get; set; }

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

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
