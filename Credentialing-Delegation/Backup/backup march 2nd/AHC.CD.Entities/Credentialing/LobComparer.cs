using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing
{
   public  class LobComparer :IEqualityComparer<PlanLOB>
    {
       public bool Equals(PlanLOB x, PlanLOB y)
        {
            return x.LOBID == y.LOBID;
        }

       public int GetHashCode(PlanLOB obj)
        {
            return obj.LOBID.GetHashCode();
        }
    }
}
