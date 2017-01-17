using AHC.CD.Business.BusinessModels.DelegationProfileReport;
using AHC.CD.Entities.Credentialing.DelegationProfileReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.DelegationProfileReport
{
    public interface IDelegationProfileReportManager
    {
        Task<ProviderGeneralInfoBussinessModel> GetProfileDataByIdAsync(int profileId, List<ProviderPracitceInfoBusinessModel> locations, List<ProviderProfessionalDetailBusinessModel> specialtis);
        int SaveDelegationProfileReport(int requestId, ProfileReport report);
        Task<List<ProfileReport>> GetDelegationProfileReport(int CredContractRequestId);
    }
}
