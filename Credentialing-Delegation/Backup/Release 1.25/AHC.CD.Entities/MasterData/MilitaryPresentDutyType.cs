using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterData
{
    public enum MilitaryPresentDutyType
    {
        Active = 1,
        Reserve,
        [Display(Name = "National Guard")]
        NationalGuard,

    }
}
