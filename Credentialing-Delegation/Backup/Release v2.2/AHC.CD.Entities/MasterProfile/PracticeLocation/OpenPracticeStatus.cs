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

        public int? MinimumAge { get; set; }
        public int? MaximumAge { get; set; }

        #region Practice Limitation

        public string AnyPracticeLimitation { get; private set; }

        [NotMapped]
        public YesNoOption? AnyPracticeLimitationOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.AnyPracticeLimitation))
                    return null;

                if (this.AnyPracticeLimitation.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.AnyPracticeLimitation);
            }
            set
            {
                this.AnyPracticeLimitation = value.ToString();
            }
        }

        #endregion

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

                if (this.GenderLimitation.Equals("Not Available"))
                    return null;

                return (GenderLimitationType)Enum.Parse(typeof(GenderLimitationType), this.GenderLimitation);
            }
            set
            {
                this.GenderLimitation = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

    }
}
