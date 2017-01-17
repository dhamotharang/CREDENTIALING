using AHC.CD.Data.Repository;
using AHC.CD.Entities.ProfileDemographicInfo;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository
{
    internal class ProvidersEFRepository : EFGenericRepository<Provider>, IProvidersRepository
    {
        
        public IEnumerable<Individual> GetAllIndividualProviders()
        {
            return DbSet.OfType<Individual>().AsEnumerable<Individual>();
        }


        public async Task<IEnumerable<Individual>> GetAllIndividualProvidersAsync()
        {
            return await DbSet.OfType<Individual>().ToListAsync<Individual>();
        }


        public async Task<IEnumerable<ProviderCategory>> GetAllCategoriesAsync()
        {
            var list = from pc in DbSet.OfType<Individual>()
                       select pc.ProviderType.ProviderCategory;

            return await list.ToListAsync<ProviderCategory>();
        }

        public bool IsEmailExist(string email) {

         return  DbSet.OfType<PersonalInfo>().Any(a => a != null && a.Email.Equals(email));

         }

        public async Task<IEnumerable<Individual>> GetAllIndividualProvidersByAsync(int providerCategoryId, ProviderStatus providerStatus)
        {

            var list = from i in DbSet.OfType<Individual>()
                       where i.ProviderType.ProviderCategory.ProviderCategoryID == providerCategoryId && i.ProviderStatus == providerStatus
                       select i;
            
            return await list.ToListAsync<Individual>();
            
        }


        
    }
}
