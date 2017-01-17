using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
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

        [RequiredIf("SpecialtyBoardExamStatus", (int)SpecialtyBoardExamStatus.IntendToSit, ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [DateStart(MaxPastYear = "5", MinPastYear = "0", ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        [Display(Name = "Date Of Exam *")]
        public DateTime? ExamDate { get; set; }

        [RequiredIf("SpecialtyBoardExamStatus", (int)SpecialtyBoardExamStatus.DoNotIntendToSit, ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Reason For Not Taking Exam *")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
        public string ReasonForNotTakingExam { get; set; }

        [RequiredIf("SpecialtyBoardExamStatus", (int)SpecialtyBoardExamStatus.FailedExam, ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Remark For Failing The Exam *")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
        public string RemarkForExamFail { get; set; }
    }
}
