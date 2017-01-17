using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Enums
{
    public enum GenderLimitationType
    {
        [Display(Name = "Male Only")]
        MaleOnly = 1,
        [Display(Name = "Female Only")]
        FemaleOnly,
        [Display(Name = "None")]
        None
    }
}
