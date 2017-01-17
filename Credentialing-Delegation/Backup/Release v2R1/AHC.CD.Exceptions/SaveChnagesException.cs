using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions
{
    public class SaveChnagesException : ApplicationException
    {
        public SaveChnagesException(Exception innerException = null,string message = "")
            : base(message, innerException)
        {
            
        }
    }
}
