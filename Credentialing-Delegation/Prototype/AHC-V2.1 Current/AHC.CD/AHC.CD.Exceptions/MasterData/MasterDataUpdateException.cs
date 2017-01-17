using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions.MasterData
{
   public class MasterDataUpdateException:ApplicationException
    {
       public MasterDataUpdateException(string message = "", Exception innerException = null)
           : base(message, innerException)
       {
           
       }
    }
}
