using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions.MasterData
{
   public class SchoolAddException : ApplicationException
    {

       public SchoolAddException(string message = "", Exception innerException = null)
           : base(message, innerException)
       {

       }
    }
}
