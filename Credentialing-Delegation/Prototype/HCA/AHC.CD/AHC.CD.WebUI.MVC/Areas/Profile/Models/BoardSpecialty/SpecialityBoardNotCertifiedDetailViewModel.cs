using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.BoardSpeciality
{
    public class SpecialityBoardNotCertifiedDetailViewModel
    {
        public int SpecialityBoardNotCertifiedDetailID { get; set; }
        
        [Required]
        [Display(Name = "If not board certified, select one *")]
        public string ExamStatus { get; private set; }

        [Display(Name = "Date Of Exam *")]
        public DateTime ExamDate { get; set; }

        [Display(Name = "Reason For Not Taking Exam *")]
        public string ReasonForNotTakingExam { get; set; }
    }
}
