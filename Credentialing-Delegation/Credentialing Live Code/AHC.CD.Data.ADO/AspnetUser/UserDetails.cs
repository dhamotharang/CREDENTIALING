using AHC.CD.Data.ADO.CoreRepository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.AspnetUser
{
    public class UserDetails : IUserDetails
    {
        DAPPERRepository dp = null;
        public UserDetails()
        {
            this.dp = new DAPPERRepository();
        }

        public dynamic GetUserDetailsByUserID(string userID)
        {
            List<dynamic> Data = new List<dynamic>();
            try
            {
                DynamicParameters values = new DynamicParameters();
                values.Add("@userid", userID);
                string query = "SELECT Email,FullName FROM [dbo].[AspNetUsers] where Id=@userid";
                Data = dp.ExecuteQueryForASPNETUsers<dynamic>(values, query);

            }
            catch (Exception e)
            {
                throw e;
            }
            return Data[0];
        }
        public List<dynamic> GetCCOAndCRADetails()
        {
            List<dynamic> Data = new List<dynamic>();

            try
            {
               
                string query = "select FullName,Email from [dbo].[AspNetUsers] au inner join [dbo].[AspNetUserRoles] aur on au.Id=aur.UserId where aur.RoleId in (select Id from [dbo].[AspNetRoles] where Name='CCO' or Name='CRA')";
                Data = dp.ExecuteQueryForASPNETUsers<dynamic>(query);

            }
            catch (Exception e)
            {
                throw e;
            }
            return Data;
        }


        public dynamic UsersPasswordLastUpdated(string GUID)
        {
            List<dynamic> Data = new List<dynamic>();
            try
            {
                DynamicParameters values = new DynamicParameters();
                values.Add("@userid", GUID);
                string query = "SELECT top 1 t2.[CreatedDate] FROM [dbo].[AspNetUsers] as t1 LEFT OUTER JOIN [dbo].[UsedPasswords] as t2 ON t1.Id=t2.UserID where t1.[UserName]= @userid ORDER BY t2.[CreatedDate] DESC";
                Data = dp.ExecuteQueryForASPNETUsers<dynamic>(values, query);

            }
            catch (Exception e)
            {
                throw e;
            }
            return Data[0];
        }
    }
}
