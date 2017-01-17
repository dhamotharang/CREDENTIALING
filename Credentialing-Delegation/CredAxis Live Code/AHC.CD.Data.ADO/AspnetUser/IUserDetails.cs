using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.AspnetUser
{
    public interface IUserDetails
    {
       dynamic GetUserDetailsByUserID(string userID);
       List<dynamic> GetCCOAndCRADetails();
    }
}
