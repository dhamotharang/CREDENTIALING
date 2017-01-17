using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.WorkHistory
{
    public class MilitaryServiceInformation
    {
        public MilitaryServiceInformation()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int MilitaryServiceInformationID { get; set; }

        #region MilitaryBranch

        [Required]
        public int MilitaryBranchID { get; set; }
        [ForeignKey("MilitaryBranchID")]
        public virtual MilitaryBranch MilitaryBranch { get; set; }

        #endregion

        #region MilitaryRank

        [Required]
        public int MilitaryRankID { get; set; }
        [ForeignKey("MilitaryRankID")]
        public MilitaryRank MilitaryRank { get; set; }

        #endregion

        #region MilitaryDischarge

        [Required]
        public int MilitaryDischargeID { get; set; }
        [ForeignKey("MilitaryDischargeID")]
        public MilitaryDischarge MilitaryDischarge { get; set; }

        #endregion

        #region MilitaryPresentDuty

        [Required]
        public int MilitaryPresentDutyID { get; set; }
        [ForeignKey("MilitaryPresentDutyID")]
        public MilitaryPresentDuty MilitaryPresentDuty { get; set; }
        
        #endregion

        public string MilitaryDischargeExplanation { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime DischargeDate { get; set; }

        //[Required]
        //public bool NoMilitaryObligation { get; set; }

        //public string ObligationExplanation { get; set; }        

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
