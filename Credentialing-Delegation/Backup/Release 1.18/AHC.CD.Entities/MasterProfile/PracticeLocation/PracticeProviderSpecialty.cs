using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterData.Tables;
using System.ComponentModel.DataAnnotations.Schema;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PracticeProviderSpecialty
    {
        public int PracticeProviderSpecialtyId { get; set; }

        public int SpecialtyID { get; set; }
        [ForeignKey("SpecialtyID")]
        public virtual Specialty Specialty { get; set; }

    }
}
