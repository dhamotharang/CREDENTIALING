using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing
{
   public class LobBusinessModel
    {
        public int LOBID { get; set; }

        public string LOBCode { get; set; }

        public string LOBName { get; set; }

       public ICollection<SubPlan> SubPlans { get; set;}
    }
}
