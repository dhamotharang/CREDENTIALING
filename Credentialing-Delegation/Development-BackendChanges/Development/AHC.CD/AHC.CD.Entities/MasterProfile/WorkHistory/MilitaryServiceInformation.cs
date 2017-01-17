using AHC.CD.Entities.MasterData.Enums;
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
        
        //[Required(AllowEmptyStrings=true)]
        public string MilitaryRank { get; set; }

        public string MilitaryDischarge { get; set; }

        //[Required]
        public string MilitaryPresentDuty { get; set; }        

        public string MilitaryDischargeExplanation { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? DischargeDate { get; set; }

        public string HonorableExplanation { get; set; }

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

        public ICollection<MilitaryServiceInformationHistory> MilitaryServiceInformationHistory { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
