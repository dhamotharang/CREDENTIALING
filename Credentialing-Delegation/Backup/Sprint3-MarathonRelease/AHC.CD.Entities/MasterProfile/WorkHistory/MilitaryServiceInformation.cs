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

        [Required]
        public string MilitaryBranch { get; set; }

        public string MilitaryRank { get; set; }

        public string MilitaryDischarge { get; set; }

        [Required]
        public string MilitaryPresentDuty { get; set; }        

        public string MilitaryDischargeExplanation { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DischargeDate { get; set; }

        public string HonorableExplanation { get; set; }        

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
