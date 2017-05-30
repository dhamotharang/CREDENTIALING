using AHC.CD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.AuditLogManager
{
    public interface IAuditManagerADO
    {
        Task<AduitDTO> GetAllAuditLog(AuditSearchDTO auditSearchDTO);
    }
}
