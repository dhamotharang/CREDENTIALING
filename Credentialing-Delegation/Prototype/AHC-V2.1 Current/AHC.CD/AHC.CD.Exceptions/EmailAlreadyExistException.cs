using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions
{
   public class EmailAlreadyExistException:ApplicationException
    {
        public EmailAlreadyExistException(string message="",Exception innerException = null):base(message,innerException)
        {

        }
    }
}
