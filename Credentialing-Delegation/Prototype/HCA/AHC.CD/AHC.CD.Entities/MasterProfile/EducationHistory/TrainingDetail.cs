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

        public string InstitutionName { get; set; }

        public string HospitalName { get; set; }

        public EducationAddress EducationAddress { get; set; }

        public string Telephone { get; set; }
        public string Fax { get; set; }

        #region IsCompleted

        public string IsCompleted { get; private set; }

        [NotMapped]
        public YesNoOption CompletedYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsCompleted);
            }
            set
            {
                this.IsCompleted = value.ToString();
            }
        }

        #endregion

        public string InCompleteReason { get; set; }

        public ICollection<ResidencyInternshipDetail> ResidencyInternshipDetails { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
