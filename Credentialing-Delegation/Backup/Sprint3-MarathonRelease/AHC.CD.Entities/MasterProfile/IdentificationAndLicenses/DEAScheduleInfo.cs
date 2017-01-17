using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.IdentificationAndLicenses
{
    public class DEAScheduleInfo
    {
        public DEAScheduleInfo()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int DEAScheduleInfoID { get; set; }

        #region DEASchedule

        [Required]
        public int DEAScheduleID { get; set; }
        [ForeignKey("DEAScheduleID")]
        public DEASchedule DEASchedule { get; set; }

        #endregion

        #region IsEligible

        [Required]
        public string IsEligible { get; set; }

        [NotMapped]
        public YesNoOption? YesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsEligible))
                    return null;

                if (this.IsEligible.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsEligible);
            }
            set
            {
                this.IsEligible = value.ToString();
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

                if (this.Status.Equals("Not Available"))
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
