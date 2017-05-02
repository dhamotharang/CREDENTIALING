using AHC.CD.Data.ADO.AspnetUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Users
{
    public class PasswordHistory : IPasswordHistory
    {
        private readonly IUserDetails iUserDetails = null;
        public PasswordHistory(IUserDetails iUserDetails)
        {
            this.iUserDetails = iUserDetails;
        }
        public int? PasswordHistoryLastUpdated(string GUID)
        {
            var temp = iUserDetails.UsersPasswordLastUpdated(GUID);
            if (temp.CreatedDate!=null)
            {
                return (DateTime.Now - temp.CreatedDate).Days;
            }
            return null;
        }
    }
}
