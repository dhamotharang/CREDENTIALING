using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing
{
    public class PlanLOB
    {   
        public PlanLOB()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PlanLOBID { get; set; }

        public int PlanID { get; set; }
        [ForeignKey("PlanID")]
        public  Plan Plan { get; set; }  

        public int LOBID { get; set; }
        [ForeignKey("LOBID")]
        public  LOB LOB { get; set; }

        #region Contact Details

        public ICollection<PlanContactDetail> ContactDetails { get; set; }

        #endregion

        #region Location Details

        public ICollection<PlanAddress> Locations { get; set; }

        #endregion

        public virtual ICollection<SubPlan> SubPlans { get; set; }    

        public DateTime LastModifiedDate { get; set; }
    }
}