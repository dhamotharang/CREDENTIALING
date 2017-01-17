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

        public ICollection<DEAScheduleTypeInfo> DEAScheduleTypeInfos { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
