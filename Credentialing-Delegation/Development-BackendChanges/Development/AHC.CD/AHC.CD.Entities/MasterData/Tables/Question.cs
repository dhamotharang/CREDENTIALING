using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Tables
{
    public class Question
    {
        public Question()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int QuestionID { get; set; }

        [Required]
        //[MaxLength(500)]
        //[Index(IsUnique = true)]
        public string Title { get; set; }

        #region Question Category

        //[Required]
        public int? QuestionCategoryId { get; set; }
        [ForeignKey("QuestionCategoryId")]
        public virtual QuestionCategory QuestionCategory { get; set; }

        #endregion

        #region Question Theme

        public int? QuestionThemeId { get; set; }
        [ForeignKey("QuestionThemeId")]
        public QuestionTheme QuestionTheme { get; set; }

        #endregion        

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        public ICollection<ProfileDisclosureQuestion> ProfileDisclosureQuestions { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
