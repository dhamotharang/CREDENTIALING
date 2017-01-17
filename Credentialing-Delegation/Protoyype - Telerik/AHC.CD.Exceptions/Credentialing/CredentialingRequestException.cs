using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions.Credentialing
{
    public class CredentialingRequestException : ApplicationException
    {
        public CredentialingRequestException(string message = "", Exception innerException = null)
            : base(message, innerException)
        {

        }
    }
}
