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
    public class ProviderDisclosureQuestionAnswer
    {
        public ProviderDisclosureQuestionAnswer()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProviderDisclosureQuestionAnswerID { get; set; }

        #region ProviderDisclousreAnswer

        [Required]
        public string ProviderDisclousreAnswer { get; private set; }

        [NotMapped]
        public YesNoOption AnswerYesNoOption
        {
            get
            {
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
        public int ProfileDisclosureQuestionID { get; set; }
        [ForeignKey("ProfileDisclosureQuestionID")]
        public ProfileDisclosureQuestion ProfileDisclosureQuestion { get; set; }

        #endregion        
        
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
