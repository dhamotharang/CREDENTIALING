using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;

namespace AHC.CD.Entities.Credentialing
{
    public class PlanContractDetail
    {
        public PlanContractDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PlanContractDetailID { get; set; }

        public int LOBID { get; set; }
        [ForeignKey("LOBID")]
        public LOB LOB { get; set; }

        public PlanLOBContact ContactDetail { get; set; }

        public ICollection<PlanContractBEMapping> PlanContractBEMapping { get; set;}

        public PlanLOBAddress AddressDetail { get; set; }

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
