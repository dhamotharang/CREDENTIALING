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
    public class DEAScheduleTypeInfo
    {
        public DEAScheduleTypeInfo()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int DEAScheduleTypeInfoID { get; set; }

        #region DEAScheduleType

        [Required]
        public int DEAScheduleTypeID { get; set; }
        [ForeignKey("DEAScheduleTypeID")]
        public DEAScheduleType DEAScheduleType { get; set; }

        #endregion
        
        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public YesNoOption? YesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.Status);
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
