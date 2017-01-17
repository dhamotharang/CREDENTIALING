using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.MasterData
{
    public class MilitaryRankBusinessModel
    {
        public int MilitaryRankID { get; set; }

        public string MilitaryRankTitle { get; set; }

        #region Status

        public string Status { get; set; }
        
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        public int MilitaryBranchID { get; set; }

        public string MilitaryBranchTitle { get; set; }

        public DateTime LastModifiedDate { get; set; }
        
    }
}
