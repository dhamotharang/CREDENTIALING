using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PracticeAccessibilityQuestionAnswer
    {
        public PracticeAccessibilityQuestionAnswer()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PracticeAccessibilityQuestionAnswerID { get; set; }

        #region Question

        [Required]
        public int PracticeQuestionId { get; set; }
        [ForeignKey("PracticeQuestionId")]
        public PracticeAccessibilityQuestion Question { get; set; }

        #endregion        

        #region Answer

        [Required]
        public string Answer { get; private set; }

        [NotMapped]
        public YesNoOption? AnswerYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.Answer))
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
