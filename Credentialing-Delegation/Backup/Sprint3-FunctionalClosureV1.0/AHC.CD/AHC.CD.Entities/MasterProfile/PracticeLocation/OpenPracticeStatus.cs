using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class OpenPracticeStatus
    {
        public OpenPracticeStatus()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int OpenPracticeStatusID { get; set; }

        public ICollection<PracticeOpenStatusQuestionAnswer> PracticeQuestionAnswers { get; set; }

        public string AnyInformationVariesByPlan { get; set; }

        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }

        public string OtherLimitations { get; set; }

        #region GenderLimitation

        public string GenderLimitation { get; private set; }

        [NotMapped]
        public GenderLimitationType? GenderLimitationType
        {
            get
            {
                if (String.IsNullOrEmpty(this.GenderLimitation))
                    return null;

                return (GenderLimitationType)Enum.Parse(typeof(GenderLimitationType), this.GenderLimitation);
            }
            set
            {
                this.GenderLimitation = value.ToString();
            }
        }

        #endregion

        //public ICollection<PracticeLocationDetail> PracticeLocationDetails { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

    }
}
