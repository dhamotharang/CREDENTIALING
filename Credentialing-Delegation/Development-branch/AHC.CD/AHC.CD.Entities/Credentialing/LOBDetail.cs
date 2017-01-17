using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing
{
    public class LOBDetail
    { 
        public LOBDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int LOBDetailID { get; set; }

        #region LOB Business Entity

        public ICollection<LobBE> LobBE { get; set; }

        #endregion

        #region Sub Plans

        public ICollection<SubPlan> SubPlans { get; set; } 

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
