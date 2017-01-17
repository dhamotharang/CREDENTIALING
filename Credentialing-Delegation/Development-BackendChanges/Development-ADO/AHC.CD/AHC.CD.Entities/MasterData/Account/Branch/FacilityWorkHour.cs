using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Account.Branch
{
    public class FacilityWorkHour
    {
        public FacilityWorkHour()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int FacilityWorkHourID { get; set; }

        [Required]
        public string DayName { get; set; }

        [NotMapped]
        public DayOfWeek? DayOfWeek
        {
            get
            {
                if (String.IsNullOrEmpty(this.DayName))
                    return null;

                if (this.DayName.Equals("Not Available"))
                    return null;

                return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), this.DayName);
            }
            set
            {
                this.DayName = value.ToString();
            }
        }

        [Required]
        public string StartTime { get; set; }

        [Required]
        public string EndTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
