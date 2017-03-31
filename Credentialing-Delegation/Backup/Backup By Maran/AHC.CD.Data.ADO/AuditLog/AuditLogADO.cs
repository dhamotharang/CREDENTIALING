using AHC.CD.Data.ADO.CoreRepository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.AuditLog
{
    public class AuditLogADO : IAuditLogADO
    {
        DAPPERRepository dp = null;
        public AuditLogADO()
        {
            this.dp = new DAPPERRepository();
        }

        public List<dynamic> GetAllAuditLogADOAsync()
        {
            List<dynamic> Data = new List<dynamic>();
            try
            {
                string query = "SELECT * FROM [dbo].[AuditMessages]";
                Data = dp.ExecuteQueryForAuditLogger<dynamic>(query);
            }
            catch (Exception e)
            {
                throw e;
            }
            return Data;
        }

    }
}
