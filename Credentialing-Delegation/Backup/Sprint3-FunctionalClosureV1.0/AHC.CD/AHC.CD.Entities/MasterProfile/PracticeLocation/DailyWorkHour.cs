using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class DailyWorkHour
    {
        public DailyWorkHour()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int DailyWorkHourID { get; set; }

        [Required]
        public string DayName { get; set; }

        [Required]
        public string StartTime { get; set; }

        #region StartTimePeriod

        [Required]
        public string StartTimePeriod { get; private set; }

        [NotMapped]
        public TimePeriod? StartTimeAMPM
        {
            get
            {
                if (String.IsNullOrEmpty(this.StartTimePeriod))
                    return null;

                return (TimePeriod)Enum.Parse(typeof(TimePeriod), this.StartTimePeriod);
            }
            set
            {
                this.StartTimePeriod = value.ToString();
            }
        }

        #endregion

        [Required]
        public string EndTime { get; set; }

        #region EndTimePeriod

        [Required]
        public string EndTimePeriod { get; private set; }

        [NotMapped]
        public TimePeriod? EndTimeAMPM
        {
            get
            {
                if (String.IsNullOrEmpty(this.EndTimePeriod))
                    return null;

                return (TimePeriod)Enum.Parse(typeof(TimePeriod), this.EndTimePeriod);
            }
            set
            {
                this.EndTimePeriod = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
