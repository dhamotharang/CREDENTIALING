using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Enums
{
    public enum SpecialityBoardExamStatus
    {
        [Display(Name = "I have taken exam, result pending")]
        ResultPending = 1,
        [Display(Name = "I intend to sit for exam")]
        IntentToSit,
        [Display(Name = "I do not intend to take exam")]
        NotIntentToSit
    }
}
