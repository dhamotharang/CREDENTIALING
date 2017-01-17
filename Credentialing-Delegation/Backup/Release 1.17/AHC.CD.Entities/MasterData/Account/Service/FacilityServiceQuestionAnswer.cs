using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Account.Service
{
    public class FacilityServiceQuestionAnswer
    {
        public FacilityServiceQuestionAnswer()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int FacilityServiceQuestionAnswerID { get; set; }

        #region Question

        [Required]
        public int FacilityServiceQuestionId { get; set; }
        [ForeignKey("FacilityServiceQuestionId")]
        public FacilityServiceQuestion Question { get; set; }

        #endregion

        #region Answer

        public string Answer { get; private set; }

        [NotMapped]
        public YesNoOption? AnswerYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.Answer))
                    return null;

                if (this.Answer.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.Answer);
            }
            set
            {
                this.Answer = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
