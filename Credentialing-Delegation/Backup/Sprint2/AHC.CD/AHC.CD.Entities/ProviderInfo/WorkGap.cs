using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.ProviderInfo
{
    public class WorkGap
    {
        public int WorkGapID { get; set; }
        public int DurationOfGap { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? GapFromDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? GapToDate { get; set; }
        public string ReasonForGap { get; set; }

    }
}
