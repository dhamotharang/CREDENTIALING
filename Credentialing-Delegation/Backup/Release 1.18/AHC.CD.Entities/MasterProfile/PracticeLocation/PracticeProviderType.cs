using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PracticeProviderType
    {
        public int PracticeProviderTypeId { get; set; }

        public int ProviderTypeID { get; set; }
        [ForeignKey("ProviderTypeID")]
        public virtual ProviderType ProviderType { get; set; }
    }
}
