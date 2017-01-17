using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.BoardSpeciality
{
    public class SpecialityBoardNotCertifiedDetail
    {
        public SpecialityBoardNotCertifiedDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int SpecialityBoardNotCertifiedDetailID { get; set; }

        #region Exam Status Enum Mapping

        [Required]
        public string ExamStatus { get; private set; }

        [NotMapped]
        public SpecialityBoardExamStatus SpecialityBoardExamStatus
        {
            get
            {
                return (SpecialityBoardExamStatus)Enum.Parse(typeof(SpecialityBoardExamStatus), this.ExamStatus);
            }
            set
            {
                this.ExamStatus = value.ToString();
            }
        }

        #endregion        

        [Column(TypeName = "datetime2")]
        public DateTime ExamDate { get; set; }

        public string ReasonForNotTakingExam { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
