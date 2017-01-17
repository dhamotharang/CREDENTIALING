using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile.Demographics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing
{
    public class Plan
    {   
        public Plan()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PlanID { get; set; }
         
        public string PlanCode { get; set; }

        public string PlanName { get; set; }

        public string  PlanDescription { get; set; }

        public bool IsDelegated { get; set; }

        #region Plan Line Of Business

        public ICollection<PlanLOB> PlanLOBs { get; set; }

        #endregion

        //#region Application Template

        //public int? AttachedFormID { get; set; }

        //#endregion 
         
        #region Contact Details

        public ICollection<PlanContactDetail> ContactDetails { get; set; }  

        #endregion

        #region Location Details

        public ICollection<PlanAddress> Locations { get; set; }

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

        public DateTime LastModifiedDate { get; set; }

    }
}
