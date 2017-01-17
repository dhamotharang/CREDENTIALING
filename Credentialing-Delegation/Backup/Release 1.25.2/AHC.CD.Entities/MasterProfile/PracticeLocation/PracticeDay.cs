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
    public class PracticeDay
    {
        public PracticeDay()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PracticeDayID { get; set; }

        [Required]
        public string DayName { get; set; }

        [NotMapped]
        public DaysOfWeek? DayOfWeek
        {
            get
            {
                if (String.IsNullOrEmpty(this.DayName))
                    return null;

                if (this.DayName.Equals("Not Available"))
                    return null;

                return (DaysOfWeek)Enum.Parse(typeof(DaysOfWeek), this.DayName);
            }
            set
            {
                this.DayName = value.ToString();
            }
        }

        public string DayOff{ get; set; }

        [NotMapped]
        public YesNoOption? DayOffYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.DayOff))
                    return null;

                if (this.DayOff.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.DayOff);
            }
            set
            {
                this.DayOff = value.ToString();
            }
        }

        public virtual ICollection<PracticeDailyHour> DailyHours { get; set; }
        

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
