using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions.Credentialing
{
    public class CredentialingException : ApplicationException
    {
        public CredentialingException(string message = "", Exception innerException = null)
            : base(message, innerException)
        {

        }
    }
}
