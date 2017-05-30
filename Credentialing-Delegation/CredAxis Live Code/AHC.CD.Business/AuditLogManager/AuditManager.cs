using AHC.CD.Data.ADO.AuditLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.AuditLogManager
{
    internal class AuditManager : IAuditManager
    {
        private readonly IAuditLogADO iAuditLogADO = null;
        public AuditManager(IAuditLogADO iAuditLogADO)
        {
            this.iAuditLogADO = iAuditLogADO;
        }
        public List<dynamic> GetAllAuditLogAsync()
        {
            return iAuditLogADO.GetAllAuditLogADOAsync();
        }

    }
}
