using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Search
{
    internal class SearchProfileForTLManager : ISearchProfileForTLManager
    {
        IProfileRepository profileRepository = null;
        private IUnitOfWork uow = null;

        public SearchProfileForTLManager(IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.uow = uow;
        }

        private ProfileUser GetProfileByTL(string userId)
        {           
            var CDUserRepo = uow.GetGenericRepository<CDUser>();

            var cdUser = CDUserRepo.Find(u => u.AuthenicateUserId == userId);

            var profileUserRepo = uow.GetGenericRepository<ProfileUser>();
            var teamLead = profileUserRepo.Find(l => l.CDUserID == cdUser.CDUserID && l.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString(), "ProvidersUser");
            return teamLead;

        }

        public List<ProviderSearchResultDTO> SearchProviderProfileForViewEdit(string NPINumber = null, string firstName = null, string lastName = null, string providerRelationship = null, string IPAGroupName = null, string providerLevel = null, string providerType = null, string userId = null)
        {
            string includeProperties = "OtherIdentificationNumber,PersonalDetail.ProviderTitles.ProviderType,PersonalDetail.ProviderLevel,ContractInfoes.ContractGroupInfoes.PracticingGroup.Group";

            var teamLead = GetProfileByTL(userId);
            List<ProviderSearchResultDTO> result = new List<ProviderSearchResultDTO>();
            try
            {
                if (teamLead != null)
                {

                    ////Search on NPI Number
                    if (!string.IsNullOrEmpty(NPINumber))
                    {

                        var profiles = profileRepository.GetAll(includeProperties).Where(o => o.OtherIdentificationNumber != null);

                        var npi = profiles.Any(a => a.OtherIdentificationNumber.NPINumber.Equals(NPINumber) && a.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString());

                        if (!npi)
                            return result;

                        var providerSearchResult = (from provider in profiles
                                                    where provider.OtherIdentificationNumber.NPINumber.Equals(NPINumber)
                                                    select ConstructProviderSearchResult(provider)).First();

                        foreach (var item in teamLead.ProvidersUser)
                        {
                            if (item != null && item.ProfileId == providerSearchResult.ProfileID)
                            {
                                result.Add(providerSearchResult);
                            }

                        }

                        return result;//providerSearchResult.ToList<ProviderSearchResultDTO>();


                    }
                    // Search on First Name
                    else if (!string.IsNullOrEmpty(firstName))
                    {
                        var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                                   where provider.PersonalDetail.FirstName.ToLower().Contains(firstName.ToLower())
                                                    && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                                   select ConstructProviderSearchResult(provider);

                        if (providerSearchResult != null)
                        {
                            foreach (var item in providerSearchResult)
                            {
                                foreach (var item1 in teamLead.ProvidersUser)
                                {
                                    if (item1 != null && item.ProfileID == item1.ProfileId)
                                    {
                                        result.Add(item);
                                    }

                                }

                            }
                        }



                        return result;
                    }
                    // Search on Last Name
                    else if (!string.IsNullOrEmpty(lastName))
                    {
                        var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                                   where provider.PersonalDetail.LastName.ToLower().Contains(lastName.ToLower())
                                                    && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                                   select ConstructProviderSearchResult(provider);

                        if (providerSearchResult != null)
                        {
                            foreach (var item in providerSearchResult)
                            {
                                foreach (var item1 in teamLead.ProvidersUser)
                                {
                                    if (item1 != null && item.ProfileID == item1.ProfileId)
                                    {
                                        result.Add(item);
                                    }

                                }

                            }
                        }


                        return result;
                    }
                    // Search on Provider Relationship
                    else if (!string.IsNullOrEmpty(providerRelationship))
                    {
                        var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                                   where provider.ContractInfoes.Any(ci => ci.ProviderRelationship == providerRelationship && ci.ContractStatusOption == AHC.CD.Entities.MasterData.Enums.ContractStatus.Accepted && provider.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                                                    && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                                   select ConstructProviderSearchResult(provider);
                        if (providerSearchResult != null)
                        {
                            foreach (var item in providerSearchResult)
                            {
                                foreach (var item1 in teamLead.ProvidersUser)
                                {
                                    if (item1 != null && item.ProfileID == item1.ProfileId)
                                    {
                                        result.Add(item);
                                    }

                                }

                            }
                        }


                        return result;
                    }
                    // Search on IPA Group Name
                    else if (!string.IsNullOrEmpty(IPAGroupName))
                    {
                        var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                                   where provider.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower()))
                                                    && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                                   select ConstructProviderSearchResult(provider);
                        if (providerSearchResult != null)
                        {
                            foreach (var item in providerSearchResult)
                            {
                                foreach (var item1 in teamLead.ProvidersUser)
                                {
                                    if (item1 != null && item.ProfileID == item1.ProfileId)
                                    {
                                        result.Add(item);
                                    }

                                }

                            }
                        }


                        return result;
                    }
                    // Search on Provider Level
                    else if (!string.IsNullOrEmpty(providerLevel))
                    {
                        var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                                   where provider.PersonalDetail.ProviderLevel.Name == providerLevel
                                                    && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                                   select ConstructProviderSearchResult(provider);
                        if (providerSearchResult != null)
                        {
                            foreach (var item in providerSearchResult)
                            {
                                foreach (var item1 in teamLead.ProvidersUser)
                                {
                                    if (item1 != null && item.ProfileID == item1.ProfileId)
                                    {
                                        result.Add(item);
                                    }

                                }

                            }
                        }


                        return result;
                    }
                    // Search on Provider Type
                    else if (!string.IsNullOrEmpty(providerType))
                    {
                        var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                                   where provider.PersonalDetail.ProviderTitles.Any(ci => ci.ProviderType.Title.ToLower() == providerType.ToLower())
                                                    && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                                   select ConstructProviderSearchResult(provider);
                        if (providerSearchResult != null)
                        {
                            foreach (var item in providerSearchResult)
                            {
                                foreach (var item1 in teamLead.ProvidersUser)
                                {
                                    if (item1 != null && item.ProfileID == item1.ProfileId)
                                    {
                                        result.Add(item);
                                    }

                                }

                            }
                        }


                        return result;
                    }
                    else
                    {
                        throw new Exception("Unable to find providers with the given search data");
                    }
                }

                return result;
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
                    if (contractGroupInfo.ContractGroupStatus == AHC.CD.Entities.MasterData.Enums.ContractGroupStatus.Accepted.ToString() && contractGroupInfo.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString())
                    {
                        groupNames.Add(contractGroupInfo.PracticingGroup.Group.Name);
                    }

                }
            }
            return groupNames;
        }

        private List<string> GetRelationships(Profile provider)
        {
            List<string> relationships = new List<string>();
            foreach (var contractInfo in provider.ContractInfoes)
            {
                if (contractInfo.ContractStatus == AHC.CD.Entities.MasterData.Enums.ContractStatus.Accepted.ToString())
                {
                    relationships.Add(contractInfo.ProviderRelationship);
                }


            }
            return relationships;
        }

        private List<string> GetTitles(Profile provider)
        {
            List<string> titles = new List<string>();
            foreach (var title in provider.PersonalDetail.ProviderTitles)
            {
                if (title.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                {
                    titles.Add(title.ProviderType.Title);
                }

            }
            return titles;
        }

        private List<string> GetProfileGroupNames(Profile provider)
        {
            return provider.ContractInfoes.SelectMany(x => x.ContractGroupInfoes).Select(y => y.PracticingGroup).Select(z => z.Group.Name).ToList();
        }

        private List<ProviderTitle> GetProfileTitles(Profile provider)
        {
            return provider.PersonalDetail.ProviderTitles.ToList();
        }

        private List<ContractInfo> GetProfileRelationships(Profile provider)
        {
            return provider.ContractInfoes.ToList();
        }

        private Profile ConstructProfileSearchResult(Profile provider)
        {
            if (provider == null) return null;

            Profile p = new Profile()
            {
                ProfileID = provider.ProfileID,

                OtherIdentificationNumber = new OtherIdentificationNumber { NPINumber = (provider.OtherIdentificationNumber == null) ? "Not Available" : provider.OtherIdentificationNumber.NPINumber },

                PersonalDetail = new PersonalDetail { FirstName = (provider.PersonalDetail == null) ? "Not Available" : provider.PersonalDetail.FirstName, LastName = (provider.PersonalDetail == null) ? "Not Available" : provider.PersonalDetail.LastName, ProviderTitles = GetProfileTitles(provider) },

                ContractInfoes = GetProfileRelationships(provider),

                StatusType = provider.StatusType,

            };

            return p;
        }

        
    }
}
