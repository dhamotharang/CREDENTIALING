using AHC.CD.Data.Repository;
using AHC.CD.Entities.ProfileDemographicInfo;
using AHC.CD.Entities.ProviderInfo;
using AHC.CD.Exceptions;
using AHC.CD.Resources;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business
{
    public class ProvidersManager : IProvidersManager
    {
        private IProvidersRepository providersRepository = null;
        private IGenericRepository<ProviderCategory> categoryRepository = null;
        private IGenericRepository<AddressInfo> addressRepository = null;
        private IGenericRepository<Group> groupsRepository = null;
        private IGenericRepository<ProviderType> providerTypeRepository = null;
        private IGenericRepository<PersonalInfo> personalInfoRepository = null;
        private IGenericRepository<ContactInfo> contactInfoRepository = null;

        public ProvidersManager(IUnitOfWork uow)
        {
            this.providersRepository = uow.GetProvidersRepository();
            this.categoryRepository = uow.GetProviderCategory();
            this.addressRepository = uow.GetProviderAddressRepository();
            this.groupsRepository = uow.GetGroupsRepository();
            this.providerTypeRepository = uow.GetProviderTypeRepository();
            this.personalInfoRepository = uow.GetPersonalInfoRepository();
            this.contactInfoRepository = uow.GetContactInfoRepository();
            
        }

        /// <summary>
        /// Create New Individual Provider and returns the Provider ID
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public int SaveIndividualProvider(Entities.ProviderInfo.Provider provider)
        {
            try
            {
               
                providersRepository.Create(provider);
                providersRepository.Save();
                return provider.ProviderID;
            }
            catch (Exception ex)
            {
                throw new ProviderSaveException(message: ExceptionMessage.PROVIDER_SAVE_EXCEPTION, innerException: ex);
            }
        }

        /// <summary>
        /// Create Individual Provider Async and returns the Provider ID
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveIndividualProviderAsync(Entities.ProviderInfo.Provider provider)
        {
            try
            {
                providersRepository.Create(provider);
                await providersRepository.SaveAsync();
                return provider.ProviderID;
            }
            catch (Exception ex)
            {
                throw new ProviderSaveException(message: ExceptionMessage.PROVIDER_SAVE_EXCEPTION, innerException: ex);
            }
        }
        
        public IEnumerable<Entities.ProviderInfo.Individual> GetAllIndividualProviders()
        {
            return providersRepository.GetAllIndividualProviders();
            //return providersRepository.GetAll("ProviderType,ContractInfo,Profile");
        }

        public async Task<IEnumerable<Entities.ProviderInfo.ProviderCategory>> GetAllCategoriesAsync()
        {
            //return await providersRepository.GetAllCategoriesAsync();
            return await categoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Entities.ProviderInfo.Individual>> GetAllIndividualProvidersByAsync(int providerCategoryId, Entities.ProviderInfo.ProviderStatus providerStatus)
        {
            return await providersRepository.GetAllIndividualProvidersByAsync(providerCategoryId, providerStatus);
        }

        public async Task<IEnumerable<Entities.ProviderInfo.Individual>> GetAllIndividualProvidersAsync()
        {
            return await providersRepository.GetAllIndividualProvidersAsync();
        }

        public async Task<int> UpdateIndividualProviderAddressAsync(Entities.ProfileDemographicInfo.AddressInfo address)
        {
            addressRepository.Update(address);
            return await addressRepository.SaveAsync();
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await groupsRepository.GetAllAsync();
        }

        /// <summary>
        /// Returns all Provider Type information and count based on the from year, to year and Provider Relation
        /// </summary>
        /// <param name="fromYear"></param>
        /// <param name="toYear"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetAllProviderTypeGraphData(ProviderRelation? providerRelation = null, int fromYear = 0, int toYear = 0)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            //if (providersRepository.GetAll().Count() == 0)
            //    throw new Exception("Providers data NOT found!");
            
            
            if (fromYear == 0 && toYear == 0 && providerRelation == null)  // for all Years and all relations - Default
            {
                var query = from provider in providersRepository.GetAll("ProviderType")
                            group provider by provider.ProviderType.Title into grouping
                            select new { Key = grouping.Key, Count = grouping.Count() };
                
                result = query.ToDictionary(x => x.Key,x=>x.Count); 
            }
            else if (providerRelation.HasValue && fromYear == 0 && toYear == 0)// for specific provider relation including all years
            {
                var query = from provider in providersRepository.GetAll("ProviderType")
                            where provider.Relation == providerRelation.Value
                            group provider by provider.ProviderType.Title into grouping
                            select new { Key = grouping.Key, Count = grouping.Count() };
                result = query.ToDictionary(x => x.Key, x => x.Count); 
            }

            else if (providerRelation == null & fromYear != 0 && toYear != 0)// for specific years with all relations
            {
                var query = from provider in providersRepository.GetAll("ProviderType")
                            where IsDataAvailable(provider, fromYear, toYear)
                            group provider by provider.ProviderType.Title into grouping
                            select new { Key = grouping.Key, Count = grouping.Count() };
                result = query.ToDictionary(x => x.Key, x => x.Count); 
            }

            else if(fromYear != 0 && toYear != 0 && providerRelation.HasValue) // for specific relation with from year and to year
            {
                 var query = from provider in providersRepository.GetAll("ProviderType")
                            where IsDataAvailable(provider, fromYear, toYear) & provider.Relation == providerRelation
                            group provider by provider.ProviderType.Title into grouping
                            select new { Key = grouping.Key, Count = grouping.Count() };
                result = query.ToDictionary(x => x.Key, x => x.Count); 
            }
            
            return result;
        }

        private bool IsDataAvailable(Provider provider, int fromYear, int ToYear)
        {
            if (provider.LastUpdatedDateTime.HasValue)
            {
                return provider.LastUpdatedDateTime.Value.Year >= fromYear && provider.LastUpdatedDateTime.Value.Year <= ToYear ;
            }
            return false;
        }
        
        public Dictionary<string, int> GetProviderTypeGraphDataAsync(string providerType, int year, ProviderRelation? providerRelation = null)
        {

            Dictionary<string, int> result = new Dictionary<string, int>();
            result.Add("Jan", 0);
            result.Add("Feb",0);
            result.Add("Mar", 0);
            result.Add("Apr", 0);
            result.Add("May", 0);
            result.Add("Jun", 0);
            result.Add("Jul", 0);
            result.Add("Aug", 0);
            result.Add("Sep", 0);
            result.Add("Oct", 0);
            result.Add("Nov", 0);
            result.Add("Dec", 0);

             List<Provider> allProvider = null;
            if (providerRelation == null)
            {

                allProvider = providersRepository.GetAllIndividualProviders()
                    .Where(p => p.LastUpdatedDateTime.Value.Year == year && p.ProviderType.Title == providerType)
                    .ToList<Provider>();
            }
            else
            {
                allProvider = providersRepository.GetAllIndividualProviders()
                    .Where(p => p.LastUpdatedDateTime.Value.Year == year && p.ProviderType.Title == providerType && p.Relation == providerRelation)
                    .ToList<Provider>();
            }
                  foreach (var provider in allProvider)
                  {
                      switch (provider.LastUpdatedDateTime.Value.Month)
                      {
                          case 1 :
                              result["Jan"]++;
                              break;

                          case 2:
                              result["Feb"]++;
                              break;
                          case 3:
                              result["Mar"]++;
                              break;
                          case 4:
                              result["Apr"]++;
                              break;
                          case 5:
                              result["May"]++;
                              break;
                          case 6:
                              result["Jun"]++;
                              break;
                          case 7:
                              result["Jul"]++;
                              break;
                          case 8:
                              result["Aug"]++;
                              break;
                          case 9:
                              result["Sep"]++;
                              break;
                          case 10:
                              result["Oct"]++;
                              break;
                          case 11:
                              result["Nov"]++;
                              break;
                          case 12:
                              result["Dec"]++;
                              break;
                      }

                     
                  }

                  return result;
        }

        /// <summary>
        /// /Method for checking whether a particular phone no exists or not in the database
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        public bool IsPhoneNoExists(int countryCode, string phoneNumber)
        {
            int count = 0;
            try
            {
                count = contactInfoRepository.GetAll().Where(ci => ci.CountryCode == countryCode && ci.PhoneNo == phoneNumber).Count();
                return count >= 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Method for checking whether a particular Email exists or not in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmailExists(string emailID)
        {
            int count = 0;
            try
            {
                count = personalInfoRepository.GetAll().Where(pi => pi.Email == emailID).Count();
                return count >= 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Provider> GetProviderByIdAsync(int providerId)
        {
            return await providersRepository.FindAsync(providerId);
        }

        /// <summary>
        /// Update the existing provider with additional information
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public int UpdateIndividualProvider(Provider provider)
        {
            providersRepository.Update(provider);
            return providersRepository.Save();
        }

        /// <summary>
        /// Update the existing provider with additional information asynchronously
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public async Task<int> UpdateIndividualProviderAsync(Provider provider)
        {
            providersRepository.Update(provider);
            return await providersRepository.SaveAsync();
        }
    }
}
