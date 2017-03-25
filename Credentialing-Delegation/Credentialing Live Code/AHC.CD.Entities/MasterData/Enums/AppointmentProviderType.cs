using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Enums
{
    public enum AppointmentProviderType
    {
        [Display(Name = "Solo Practice")]
        SoloPractice = 1,
        [Display(Name = "Group Practice")]
        GroupPractice,
        [Display(Name = "Network")]
        Network
    }
}
