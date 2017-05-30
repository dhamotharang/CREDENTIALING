using AHC.CD.Entities.Credentialing.CCMPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.Credentialing.CCMPortal
{
   public interface IAppointmentRepository
    {
        Task<List<CCMAppiontment>> GetCCMAppointmentsInfo(string ApprovalStatus);

        Task<CCMActionDTO> GetCCMActionData(int CredInfoID);

        Task<dynamic> SaveCCMQuickActionResultsAsync(CCMQuickActionDTO CCMActionResult);
    }
}
