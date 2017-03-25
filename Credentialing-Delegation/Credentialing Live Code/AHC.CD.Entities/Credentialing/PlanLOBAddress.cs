using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing
{
    public class PlanLOBAddress
    {
        public PlanLOBAddress()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PlanLOBAddressID { get; set; }

        public int PlanLOBID { get; set; }
        [ForeignKey("PlanLOBID")]
        public virtual PlanLOB PlanLOB { get; set; }

        public int PlanAddressID { get; set; }
        [ForeignKey("PlanAddressID")]
        public virtual PlanAddress PlanAddress { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
