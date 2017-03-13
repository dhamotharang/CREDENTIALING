using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public interface IRequestForApprovalManager
    {
        Task<dynamic> GetAllUpdatesAndRenewalsAsync();
        Task<dynamic> GetAllCredentialRequestsAsync();
        Task<dynamic> GetAllHistoryAsync();
        Task<dynamic> GetCredRequestDataByIDAsync(int ID);
        Task<dynamic> GetCredRequestHistoryDataByIDAsync(int ID);
        Task<bool> SetDecesionForCredRequestByIDAsync(int ID, string ApprovalType, string Reason);
    }
}
