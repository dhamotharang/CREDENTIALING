using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.BoardSpecialty
{
    public class SpecialtyBoardNotCertifiedDetailViewModel
    {
        public int SpecialtyBoardNotCertifiedDetailID { get; set; }
        
        [Required(ErrorMessage="Please specify the reason for not being Board Certified.")]
        [Display(Name = "If not board certified, select one *")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Board Exam Status!!")]
        public SpecialtyBoardExamStatus SpecialtyBoardExamStatus { get; set; }

        [RequiredIf("SpecialtyBoardExamStatus", (int)SpecialtyBoardExamStatus.IntendToSit, ErrorMessage = "Please specify the Date Of Exam.")]
        [DateStart(MaxPastYear = "5", MinPastYear = "0", ErrorMessage = "Date Of Exam should be within 5 years from now.")]
        [Display(Name = "Date Of Exam *")]
        public DateTime? ExamDate { get; set; }

        [RequiredIf("SpecialtyBoardExamStatus", (int)SpecialtyBoardExamStatus.DoNotIntendToSit, ErrorMessage = "Please enter the Reason For Not Taking the Exam.")]
        [Display(Name = "Reason For Not Taking Exam *")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Reason For Not Taking Exam should not exceed 500 characters.")]
        public string ReasonForNotTakingExam { get; set; }

        [RequiredIf("SpecialtyBoardExamStatus", (int)SpecialtyBoardExamStatus.FailedExam, ErrorMessage = "Please enter the Remarks For Failing The Exam.")]
        [Display(Name = "Remark For Failing The Exam *")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Remarks For Failing The Exam should not exceed 500 characters.")]
        public string RemarkForExamFail { get; set; }
    }
}
