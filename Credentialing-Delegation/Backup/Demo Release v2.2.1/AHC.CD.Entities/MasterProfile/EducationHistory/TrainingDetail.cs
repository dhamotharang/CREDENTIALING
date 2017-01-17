using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.EducationHistory
{
    public class TrainingDetail
    {
        public TrainingDetail()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int TrainingDetailID { get; set; }

        //[Required]
        public string HospitalName { get; set; }

        public virtual SchoolInformation SchoolInformation { get; set; }

        #region IsCompleted

        //[Required]
        public string IsCompleted { get; set; }

        [NotMapped]
        public YesNoOption? CompletedYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsCompleted))
                    return null;

                if (this.IsCompleted.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsCompleted);
            }
            set
            {
                this.IsCompleted = value.ToString();
            }
        }

        #endregion

        public string InCompleteReason { get; set; }

        public virtual ICollection<ResidencyInternshipDetail> ResidencyInternshipDetails { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
