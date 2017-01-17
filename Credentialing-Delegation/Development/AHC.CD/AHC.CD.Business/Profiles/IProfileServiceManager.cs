using AHC.CD.Entities.Credentialing.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public interface IProfileServiceManager
    {
        List<ProviderDTO> GetAllProviders();
        ProviderServiceDTO GetAllProvidersDataByNPI(string NPINumber);
        int GetProfileIDByNPI(string NPI);
         Task<object> GetProviderBriefProfileByNPI(string NPI);
         Task<List<ProviderDetailsDTO>> GetAllProviderDetails();
         List<ProviderDTOForUM> NewUMService();
         List<ProviderDTO> GetAllUltimateProviders();
         
         List<ProviderDetailsDTO> GetAllProviders1();

         #region MobileAppService
        object GetAllExpiriesForAProvider(int ProfileID);
         object GetAccountInfo(int ProfileID);
         object GetContractsForAprovider(int profileID);
         object GetProfileIDByEmail(string EmailID);
         object GetAllExpiryDates(int ProfileID);
         int GetCountOfPlansForAProvider(int ProfileID);
         int GetCountOfActiveContractsForaProvider(int ProfiileID);
         int GetCDUserIDByProfileID(int ProfileID); 
         #endregion
    }
}
