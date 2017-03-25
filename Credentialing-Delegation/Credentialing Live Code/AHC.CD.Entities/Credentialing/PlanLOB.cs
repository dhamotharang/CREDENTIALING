using AHC.CD.Entities.MasterData.Enums;
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

        public int? ReCredentialingDuration { get; set; }

        #region Contact Details

        public ICollection<LOBContactDetail> LOBContactDetails { get; set; }    

        #endregion

        #region Location Details

        public ICollection<LOBAddressDetail> LOBAddressDetails { get; set; }      

        #endregion

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion        

        public virtual ICollection<SubPlan> SubPlans { get; set; }    

        public DateTime LastModifiedDate { get; set; }
    }
}