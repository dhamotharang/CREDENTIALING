using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.AuditLog
{
    public interface IAuditLogADO
    {
        List<dynamic> GetAllAuditLogADOAsync();
    }
}
