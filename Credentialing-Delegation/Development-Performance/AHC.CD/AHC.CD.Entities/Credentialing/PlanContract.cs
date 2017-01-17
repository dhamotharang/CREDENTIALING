using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;

namespace AHC.CD.Entities.Credentialing
{
    public class PlanContract
    {
        public PlanContract()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PlanContractID { get; set; }

        public int PlanLOBID { get; set; }
        [ForeignKey("PlanLOBID")]
        public PlanLOB PlanLOB { get; set; }

        public int OrganizationGroupID { get; set; }
        [ForeignKey("OrganizationGroupID")]
        public OrganizationGroup BusinessEntity { get; set; }   

        //public ICollection<PlanContractDetail> PlanContractDetails { get; set; }

        //public bool IsDelegated { get; set; }

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
