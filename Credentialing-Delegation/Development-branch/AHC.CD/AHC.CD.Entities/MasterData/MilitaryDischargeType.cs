using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterData
{
    public enum MilitaryDischargeType
    {
        Retired = 1,
        Genaral,
        Honorable,
        [Display(Name = "Other than honorable")]
        Otherthanhonorable,
        [Display(Name = "Bad Conduct")]
        BadConduct,
        Officer,
        [Display(Name = "Entry Level Separation")]
        EntryLevelSeparation

    }
}
