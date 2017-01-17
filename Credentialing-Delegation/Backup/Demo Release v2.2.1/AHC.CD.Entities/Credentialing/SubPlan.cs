using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing
{
    public class SubPlan
    {
        public SubPlan()
        {
            LastModifiedDate = DateTime.Now;
        }   

        public int SubPlanId { get; set; }

        public string SubPlanCode { get; set; }

        public string SubPlanDescription { get; set; }

        public string SubPlanName { get; set; }

        public StatusType Status { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
