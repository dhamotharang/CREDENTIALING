using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.IdentificationAndLicenses
{
    public class DEAScheduleInfoHistory
    {
        public DEAScheduleInfoHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int DEAScheduleInfoHistoryID { get; set; }

        #region DEASchedule

        //[Required]
        public int? DEAScheduleID { get; set; }
        [ForeignKey("DEAScheduleID")]
        public DEASchedule DEASchedule { get; set; }

        #endregion

        #region IsEligible

        //[Required]
        public string IsEligible { get; set; }

        [NotMapped]
        public YesNoOption? YesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsEligible))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsEligible);
            }
            set
            {
                this.IsEligible = value.ToString();
            }
        }

        #endregion
        
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
