using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.DocumentWriter
{
    public class DocumentCreationException : ApplicationException
    {
        public DocumentCreationException(string message="",Exception innerException = null):base(message,innerException)
        {

        }
    }
}
