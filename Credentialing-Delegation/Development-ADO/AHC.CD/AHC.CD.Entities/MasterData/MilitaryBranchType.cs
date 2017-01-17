using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterData
{
    public enum  MilitaryBranchType
    {
        Army = 1,
        Marine,
        [DisplayName("Air Force")]
        AirForce,
        Navy,
        [DisplayName("Coast Guard")]
        CoastGuard,
    }
}
