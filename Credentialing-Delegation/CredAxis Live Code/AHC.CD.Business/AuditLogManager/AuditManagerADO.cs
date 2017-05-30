using AHC.CD.Data.ADO.AuditLog;
using AHC.CD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.AuditLogManager
{
    internal class AuditManagerADO : IAuditManagerADO
    {
        private readonly IAuditLogADO iAuditLogADO;
        public AuditManagerADO(IAuditLogADO iAuditLogADO)
        {
            this.iAuditLogADO = iAuditLogADO;
        }
        public async Task<AduitDTO> GetAllAuditLog(AuditSearchDTO auditSearchDTO)
        {
            AduitDTO auditMessages = new AduitDTO();
            try
            {
                auditMessages = await iAuditLogADO.GetAllAuditLog(auditSearchDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return auditMessages;
        }
    }
}
