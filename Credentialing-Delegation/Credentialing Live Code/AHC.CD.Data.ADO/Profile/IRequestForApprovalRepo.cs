using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.Profile
{
    public interface IRequestForApprovalRepo
    {
        Task<dynamic> GetAllUpdatesAndRenewalsRepo();
        Task<dynamic> GetAllCredentialRequestsRepo();
        Task<dynamic> GetAllHistoryRepo();
        Task<dynamic> GetAllUpdatesAndRenewalsForProviderRepo(int ID);
        Task<dynamic> GetAllCredentialRequestsForProviderRepo(int ID);
        Task<dynamic> GetAllHistoryForProviderRepo(int ID);
        Task<dynamic> GetCredRequestDataByIDRepo(int ID);
        Task<dynamic> GetCredRequestHistoryDataByIDRepo(int ID);
    }
}
