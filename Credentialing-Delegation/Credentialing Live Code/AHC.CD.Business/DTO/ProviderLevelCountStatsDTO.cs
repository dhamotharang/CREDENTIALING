using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.DTO
{
    public class ProviderLevelCountStatsDTO
    {
        public int ALL { get; set; }

        public int NURSE { get; set; }

        public int PCP { get; set; }

        public int MIDLEVEL { get; set; }
    }
}
