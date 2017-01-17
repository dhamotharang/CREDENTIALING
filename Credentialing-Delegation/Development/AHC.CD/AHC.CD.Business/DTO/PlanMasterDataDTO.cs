using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.DTO
{
    public class PlanMasterDataDTO
    {
        public IEnumerable<LOB> LOBs { get; set; }
        public IEnumerable<OrganizationGroup> BusinessEnities { get; set; }
    }
}
