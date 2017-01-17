using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions
{
  public  class SSNAlreadyExistsException:ApplicationException
    {
        public SSNAlreadyExistsException(string message="",Exception innerException = null):base(message,innerException)
        {

        }
    }
}
