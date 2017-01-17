using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Exceptions.Profiles;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Search
{
    internal class SearchManager : ISearchManager
    {
        IProfileRepository profileRepository = null;
        public SearchManager(IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();

        }

        /// <summary>
        /// Search Provider for Profile View or Profile Edit
        /// </summary>
        /// <param name="providerSearchRequestDTO"></param>
        /// <returns></returns>
        public List<ProviderSearchResultDTO> SearchProviderProfileForViewEdit(string NPINumber = null, string firstName = null, string lastName = null, string providerRelationship = null, string IPAGroupName = null)
        {
            string includeProperties = "OtherIdentificationNumber,PersonalDetail.ProviderTitles.ProviderType,ContractInfoes.ContractGroupInfoes.PracticingGroup.Group";
            
            try
            {
                ////Search on NPI Number
                if(!string.IsNullOrEmpty(NPINumber))
                {
                    List<ProviderSearchResultDTO> result = new List<ProviderSearchResultDTO>();

                    var profiles = profileRepository.GetAll(includeProperties).Where(o => o.OtherIdentificationNumber != null);

                    //var profiles = profileRepository.Get(_ => 1 == 1, "OtherIdentificationNumber").Where(p => p.OtherIdentificationNumber.NPINumber.Equals(NPINumber));
                    //var npi = profiles.First().OtherIdentificationNumber.NPINumber;
                    var npi = profiles.Any(a => a.OtherIdentificationNumber.NPINumber.Equals(NPINumber));
                    //if (string.IsNullOrWhiteSpace(npi) && npi != NPINumber)
                    if (!npi)
                        return result;
                    //var providerSearchResult = (from provider in profileRepository.Get(_ => 1 == 1, includeProperties)
                    //                            where provider.OtherIdentificationNumber.NPINumber.Equals(NPINumber)
                    //                            select ConstructProviderSearchResult(provider)).First();   

                    var providerSearchResult = (from provider in profiles
                                                where provider.OtherIdentificationNumber.NPINumber.Equals(NPINumber)
                                                select ConstructProviderSearchResult(provider)).First();   

                    result.Add(providerSearchResult);
                    return result;//providerSearchResult.ToList<ProviderSearchResultDTO>();
                                                
                        
                }
                // Search on First Name
                else if(!string.IsNullOrEmpty(firstName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.FirstName.ToLower().Contains(firstName.ToLower())
                                               select ConstructProviderSearchResult(provider);
                                              
                                               
                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                    // Search on Last Name
                else if(!string.IsNullOrEmpty(lastName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.LastName.ToLower().Contains(lastName.ToLower())
                                               select ConstructProviderSearchResult(provider);

                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                    // Search on Provider Relationship
                else if(!string.IsNullOrEmpty(providerRelationship))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.ContractInfoes.Any(ci => ci.ProviderRelationship == providerRelationship)
                                               select ConstructProviderSearchResult(provider);

                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                    // Search on IPA Group Name
                else if (!string.IsNullOrEmpty(IPAGroupName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower()))
                                               select ConstructProviderSearchResult(provider);

                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                else
                {
                    throw new Exception("Search Manager: Unable to find providers with the given search data");
                }

            }
            catch (ApplicationException)
            {
                throw;
            }
            //catch (Exception ex)
            //{
            //    throw new ProfileManagerException(ExceptionMessage.ALL_PROFILE_GET_EXCEPTION, ex);
            //}
        }

        private ProviderSearchResultDTO ConstructProviderSearchResult(Profile provider)
        {
            if (provider == null) return null;

            return new ProviderSearchResultDTO
            {
                ProfileID = provider.ProfileID,
                NPINumber = (provider.OtherIdentificationNumber == null) ? "Not Available" : provider.OtherIdentificationNumber.NPINumber,
                Titles = GetTitles(provider),
                FirstName = (provider.PersonalDetail == null) ? "Not Available" : provider.PersonalDetail.FirstName,
                LastName = (provider.PersonalDetail == null) ? "Not Available" : provider.PersonalDetail.LastName,
                ProviderRelationships = GetRelationships(provider),
                IPAGroupNames = GetGroupNames(provider)
            };
        }

        private List<string> GetGroupNames(Profile provider)
        {
            List<string> groupNames = new List<string>();
            foreach (var contractInfo in provider.ContractInfoes)
            {
                foreach (var contractGroupInfo in contractInfo.ContractGroupInfoes)
                {
                    groupNames.Add(contractGroupInfo.PracticingGroup.Group.Name);
                }
            }
            return groupNames;
        }

        private List<string> GetRelationships(Profile provider)
        {
            List<string> relationships = new List<string>();
            foreach (var contractInfo in provider.ContractInfoes)
            {
                relationships.Add(contractInfo.ProviderRelationship);
            }
            return relationships;
        }

        private List<string> GetTitles(Profile provider)
        {
            List<string> titles = new List<string>();
            foreach (var title in provider.PersonalDetail.ProviderTitles)
            {
                titles.Add(title.ProviderType.Title);
            }
            return titles;
        }
    }
}
