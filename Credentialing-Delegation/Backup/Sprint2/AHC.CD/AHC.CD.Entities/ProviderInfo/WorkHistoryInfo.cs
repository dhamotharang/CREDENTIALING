using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.ProviderInfo
{
    public class WorkHistoryInfo
    {
        public int WorkHistoryInfoID { get; set; }
        public virtual ICollection<WorkGap> WorkGaps { get; set; }
        public virtual ICollection<WorkInfo> WorkInfos { get; set; }
    }
}
