using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    class PracticeLocationMidLevel
    {
        public PracticeLocationMidLevel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PracticeLocationMidLevelID { get; set; }

        public int MidLevelPractitionerID { get; set; }
        [ForeignKey("MidLevelPractitionerID")]
        public MidLevelPractitioner MidLevelPractitioner { get; set; }

        #region Status

        public string Status { get; set; }

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

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

    }
}
