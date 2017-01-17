using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AHC.CD.Entities.MasterData.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AHC.CD.Entities.EmailNotifications
{
    public class CustomDaysInWeek
    {
        public CustomDaysInWeek()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CustomDaysInWeekID { get; set; }

        #region Day Of Week

        public string Day { get; private set; }

        [NotMapped]
        public DaysOfWeek? DayOfWeek
        {
            get
            {
                if (String.IsNullOrEmpty(this.Day))
                    return null;

                return (DaysOfWeek)Enum.Parse(typeof(DaysOfWeek), this.Day);
            }
            set
            {
                this.Day = value.ToString();
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

        public DateTime LastModifiedDate { get; set; }
    }
}
