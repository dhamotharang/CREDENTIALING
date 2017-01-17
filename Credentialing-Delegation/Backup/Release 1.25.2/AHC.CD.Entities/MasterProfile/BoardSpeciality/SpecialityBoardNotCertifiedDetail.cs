using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.BoardSpecialty
{
    public class SpecialtyBoardNotCertifiedDetail
    {
        public SpecialtyBoardNotCertifiedDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int SpecialtyBoardNotCertifiedDetailID { get; set; }

        #region Exam Status Enum Mapping

        [Required]
        public string ExamStatus { get; private set; }

        [NotMapped]
        public SpecialtyBoardExamStatus? SpecialtyBoardExamStatus
        {
            get
            {
                if (String.IsNullOrEmpty(this.ExamStatus))
                    return null;

                if (this.ExamStatus.Equals("Not Available"))
                    return null;

                return (SpecialtyBoardExamStatus)Enum.Parse(typeof(SpecialtyBoardExamStatus), this.ExamStatus);
            }
            set
            {
                this.ExamStatus = value.ToString();
            }
        }

        #endregion        

        [Column(TypeName = "datetime2")]
        public DateTime? ExamDate { get; set; }

        public string ReasonForNotTakingExam { get; set; }

        public string RemarkForExamFail { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
