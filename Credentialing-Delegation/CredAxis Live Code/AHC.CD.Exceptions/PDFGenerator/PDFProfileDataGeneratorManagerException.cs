using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions.PDFGenerator
{
    public class PDFProfileDataGeneratorManagerException : ApplicationException
    {
        public PDFProfileDataGeneratorManagerException(string message = "", Exception innerException = null)
            : base(message, innerException)
        {

        }
    }
}
