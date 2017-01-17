using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing
{
   public class PlanBusinessModel
    {
        public int PlanID { get; set; }

        public string PlanCode { get; set; }

        public string PlanName { get; set; }

        public ICollection<LOB> LOB { get; set;}

    }
}
