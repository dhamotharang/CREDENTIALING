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
        Task<dynamic> GetAllUpdatesAndRenewalsForProviderAsync(int ID);
        Task<dynamic> GetAllCredentialRequestsAsync();
        Task<dynamic> GetAllCredentialRequestsForProviderAsync(int ID);
        Task<dynamic> GetAllHistoryAsync();
        Task<dynamic> GetAllHistoryForProviderAsync(int ID);
        Task<dynamic> GetCredRequestDataByIDAsync(int ID);
        Task<dynamic> GetCredRequestHistoryDataByIDAsync(int ID);
        Task<bool> SetDecesionForCredRequestByIDAsync(int ID, string ApprovalType, string Reason, string UserID);
        Task<dynamic> SetApprovalForCredRequestByIDsAsync(string CredRequestsIDS, string UserID);
        Task<int?> GetProfileID(string AuthID);
        Task<bool> SetApprovalByIDs(List<int> trackerIDs, string userAuthID);
        Task AddCredRequestTrackerNotification(List<int> credIDs, string approvalType, string userName);
        Task AddUpdatesRequestTrackerNotification(List<int> trackerIDs, string approvalType, string userName);
        Task<dynamic> GetAllCredRequestHistoryAsync();
        Task<dynamic> GetAllCredRequestHistoryByIDAsync(int ID);
        Task<dynamic> GetAllUpdateRequestHistoryAsync();
        Task<dynamic> GetAllUpdateRequestHistoryByIDAsync(int ID);
        Task<dynamic> GetAllRenewalRequestHistoryAsync();
        Task<dynamic> GetAllRenewalRequestHistoryByIDAsync(int ID);

    }
}
