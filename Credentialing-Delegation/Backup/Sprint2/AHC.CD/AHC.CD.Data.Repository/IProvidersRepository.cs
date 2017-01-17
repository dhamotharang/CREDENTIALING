using AHC.CD.Entities.ProfileDemographicInfo;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository
{
    public interface IProvidersRepository : IGenericRepository<Provider>
    {
        IEnumerable<Individual> GetAllIndividualProviders();
        Task<IEnumerable<Individual>> GetAllIndividualProvidersAsync();

        Task<IEnumerable<ProviderCategory>> GetAllCategoriesAsync();

        Task<IEnumerable<Individual>> GetAllIndividualProvidersByAsync(int providerCategoryId, ProviderStatus providerStatus);
        bool IsEmailExist(string email);

        
    }
}
