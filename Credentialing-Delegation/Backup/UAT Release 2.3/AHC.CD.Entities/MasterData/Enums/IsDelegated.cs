using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Enums
{
    public enum IsDelegated
    {
        [Display(Name = "Yes")]
        YES = 1,
        [Display(Name = "No")]
        NO
    }
}
