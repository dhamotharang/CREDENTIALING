using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.DisclosureQuestions
{
    public class ProfileDisclosureQuestionAnswer
    {
        public ProfileDisclosureQuestionAnswer()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProfileDisclosureQuestionAnswerID { get; set; }

        #region ProviderDisclousreAnswer

        [Required]
        public string ProviderDisclousreAnswer { get; set; }

        [NotMapped]
        public YesNoOption? AnswerYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.ProviderDisclousreAnswer))
                    return null;

                if (this.ProviderDisclousreAnswer.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ProviderDisclousreAnswer);
            }
            set
            {
                this.ProviderDisclousreAnswer = value.ToString();
            }
        }

        #endregion

        public string Reason { get; set; }

        #region Question

        [Required]
        public int QuestionID { get; set; }
        [ForeignKey("QuestionID")]
        public Question Question { get; set; }

        #endregion        
        
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
