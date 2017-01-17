using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.ProviderInfo
{
    public class WorkInfo
    {
        public int WorkInfoID { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EndDate { get; set; }

        public bool IsIndependent { get; set; }
        public string ReasonForDeparture { get; set; }
        public string WorkplaceName { get; set; }
        public virtual EmploymentInfo EmploymentInfo{ get; set; }
        public virtual WorkInfoContact WorkHistoryContact { get; set; }

    }
}
