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
    public class PracticeOfficeHour
    {
        public PracticeOfficeHour()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PracticeOfficeHourID { get; set; }

        public virtual ICollection<PracticeDay> PracticeDays { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
