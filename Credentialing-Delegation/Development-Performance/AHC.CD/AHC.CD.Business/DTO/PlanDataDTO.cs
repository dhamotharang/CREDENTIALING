using AHC.CD.Entities.Credentialing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.DTO
{
    public class PlanDataDTO
    {
        public Plan PlanData { get; set; }
        public IEnumerable<PlanContract> PlanContracts { get; set; }
    }
}
