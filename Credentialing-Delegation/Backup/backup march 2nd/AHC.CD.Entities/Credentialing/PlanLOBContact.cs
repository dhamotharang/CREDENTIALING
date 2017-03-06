using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing
{
    public class PlanLOBContact
    {
        public PlanLOBContact()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PlanLOBContactID { get; set; }   

        public int PlanLOBID { get; set; }
        [ForeignKey("PlanLOBID")]
        public virtual PlanLOB PlanLOB { get; set; }

        public int PlanContactDetailID { get; set; }
        [ForeignKey("PlanContactDetailID")]
        public virtual PlanContactDetail PlanContactDetail { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
