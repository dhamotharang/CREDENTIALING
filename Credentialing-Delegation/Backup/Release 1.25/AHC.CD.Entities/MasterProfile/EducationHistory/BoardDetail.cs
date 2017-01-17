using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.EducationHistory
{
    public class BoardDetail
    {
        public int BoardDetailID { get; set; }
        public bool BoardCertified { get; set; }
        public string BoardCode { get; set; }
        public string BoardName { get; set; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime InitialCertificationDate { get; set; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime LastReCerificationDate  { get; set; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public string ReasonForNotBoardCertified { get; set; }
        public string PercentOfTime { get; set; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExamDate { get; set; }

        public string ExamStatus { get; set; }

        public ExamStatusEnum ExamStatusEnum { get; set; }

        public string ReasonForNotTakingExam { get; set; }
        public string SpecialityCertPath { get; set; }
        public string PracticeInterest { get; set; }


    }

    public enum ExamStatusEnum
    {
        ResultPending=1,
        IntentToSit,
        NotIntentToSit
    }
}
