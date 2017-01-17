using AHC.CD.Entities.MasterData;
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
    public class OfficeHour
    {
        public OfficeHour()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int OfficeHourID { get; set; }

        #region AnyTimePhoneCoverage

        [Required]
        public string AnyTimePhoneCoverage { get; private set; }

        [NotMapped]
        public YesNoOption AnyTimePhoneCoverageYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.AnyTimePhoneCoverage);
            }
            set
            {
                this.AnyTimePhoneCoverage = value.ToString();
            }
        }

        #endregion

        public ICollection<DailyWorkHour> DailyWorkHours { get; set; }

        //public ICollection<PracticeLocationDetail> PracticeLocationDetails { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
