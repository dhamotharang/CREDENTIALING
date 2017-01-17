using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions
{
    public class DatabaseValidationException : ApplicationException
    {
        public string ValidationError { get; set; }
        
        public DatabaseValidationException(string validationError, string message = "", Exception innerException = null)
            : base(message, innerException)
        {
            this.ValidationError = validationError;
        }
    }
}
