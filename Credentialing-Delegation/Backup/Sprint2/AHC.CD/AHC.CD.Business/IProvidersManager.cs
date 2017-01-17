using AHC.CD.Entities.ProfileDemographicInfo;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business
{
    public interface IProvidersManager
    {
        int SaveIndividualProvider(Provider provider);
        Task<int> SaveIndividualProviderAsync(Provider provider);

        int UpdateIndividualProvider(Provider provider);
        Task<int> UpdateIndividualProviderAsync(Provider provider);

        IEnumerable<Individual> GetAllIndividualProviders();
        Task<IEnumerable<Individual>> GetAllIndividualProvidersAsync();

        Task<IEnumerable<ProviderCategory>> GetAllCategoriesAsync();
        
        Task<IEnumerable<Individual>> GetAllIndividualProvidersByAsync(int providerCategoryId, ProviderStatus providerStatus);
        
        Task<int> UpdateIndividualProviderAddressAsync(AddressInfo address);

        Task<IEnumerable<Group>> GetAllGroupsAsync();

        Dictionary<string,int> GetAllProviderTypeGraphData(ProviderRelation? providerRelation = null, int fromYear=0, int toYear = 0);

        bool IsPhoneNoExists(int countryCode, string phoneNumber);

        bool IsEmailExists(string Email);

        Dictionary<string, int> GetProviderTypeGraphDataAsync(string providerType, int year, ProviderRelation? providerRelation = null);

       Task<Provider> GetProviderByIdAsync(int providerId);

        //Task<bool> SaveOrUpdateProviderDocumentAsync(int providerID, Document providerDocument, Stream inputStream, string filePathToSave);

    }
}
