using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Enums
{
    public enum SpecialtyBoardExamStatus
    {
        [Display(Name = "I have taken exam, result pending")]
        ResultPending = 1,
        [Display(Name = "I intend to sit for exam")]
        IntendToSit,
        [Display(Name = "I do not intend to take exam")]
        DoNotIntendToSit,
        [Display(Name = "I failed in the exam")]
        FailedExam
    }
}
