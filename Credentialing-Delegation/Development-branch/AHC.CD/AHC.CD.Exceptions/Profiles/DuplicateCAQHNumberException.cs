using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions.Profiles
{
    public class DuplicateCAQHNumberException : ApplicationException
    {
        public DuplicateCAQHNumberException(string message = "", Exception innerException = null)
            : base(message, innerException)
        {

        }
    }
}
