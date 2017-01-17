using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions.Credentialing.PSV
{
    public class PSVManagerException : ApplicationException
    {
        public PSVManagerException(string message = "", Exception innerException = null)
            : base(message, innerException)
        {

        }
    }
}
