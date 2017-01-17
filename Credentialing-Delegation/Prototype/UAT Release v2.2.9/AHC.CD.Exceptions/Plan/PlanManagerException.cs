using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions.Plan
{
    public class PlanManagerException : ApplicationException
    {
        public PlanManagerException(string message = "", Exception innerException = null)
            : base(message, innerException)
        {

        }
    }
}
