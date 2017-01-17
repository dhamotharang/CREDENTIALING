using AHC.CD.Business.Credentialing;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
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
        private IUnitOfWork uow = null;

        public SearchManager(IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.uow = uow;

        }

        /// <summary>
        /// Search Provider for Profile View or Profile Edit
        /// </summary>
        /// <param name="providerSearchRequestDTO"></param>
        /// <returns></returns>
        public List<ProviderSearchResultDTO> SearchProviderProfileForViewEdit(string NPINumber = null, string firstName = null, string lastName = null, string providerRelationship = null, string IPAGroupName = null, string providerLevel = null, string providerType = null)
        {
            string includeProperties = "OtherIdentificationNumber,PersonalDetail.ProviderTitles.ProviderType,PersonalDetail.ProviderLevel,ContractInfoes.ContractGroupInfoes.PracticingGroup.Group";

            try
            {
                ////Search on NPI Number
                if (!string.IsNullOrEmpty(NPINumber))
                {
                    List<ProviderSearchResultDTO> result = new List<ProviderSearchResultDTO>();

                    var profiles = profileRepository.GetAll(includeProperties).Where(o => o.OtherIdentificationNumber != null);

                    //var profiles = profileRepository.Get(_ => 1 == 1, "OtherIdentificationNumber").Where(p => p.OtherIdentificationNumber.NPINumber.Equals(NPINumber));
                    //var npi = profiles.First().OtherIdentificationNumber.NPINumber;
                    var npi = profiles.Any(a => a.OtherIdentificationNumber.NPINumber.Equals(NPINumber) && a.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString());
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
                else if (!string.IsNullOrEmpty(firstName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.FirstName.ToLower().Contains(firstName.ToLower())
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResult(provider);


                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                // Search on Last Name
                else if (!string.IsNullOrEmpty(lastName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.LastName.ToLower().Contains(lastName.ToLower())
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResult(provider);

                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                // Search on Provider Relationship
                else if (!string.IsNullOrEmpty(providerRelationship))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.ContractInfoes.Any(ci => ci.ProviderRelationship == providerRelationship && ci.ContractStatusOption == AHC.CD.Entities.MasterData.Enums.ContractStatus.Accepted && provider.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResult(provider);

                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                // Search on IPA Group Name
                else if (!string.IsNullOrEmpty(IPAGroupName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower()) && ci.ContractStatusOption == AHC.CD.Entities.MasterData.Enums.ContractStatus.Accepted && provider.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResult(provider);

                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                // Search on Provider Level
                else if (!string.IsNullOrEmpty(providerLevel))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.ProviderLevel.Name == providerLevel
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResult(provider);

                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                // Search on Provider Type
                else if (!string.IsNullOrEmpty(providerType))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.ProviderTitles.Any(ci => ci.ProviderType.Title.ToLower() == providerType.ToLower())
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
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


        public List<SearchResultForCred> SearchProviderProfileForCred(string NPINumber = null, string firstName = null, string lastName = null, string specialty = null, string IPAGroupName = null, string CAQH = null, string providerType = null)
        {
            string includeProperties = "OtherIdentificationNumber,PersonalDetail.ProviderTitles.ProviderType,SpecialtyDetails.Specialty,ContractInfoes.ContractGroupInfoes.PracticingGroup.Group";

            try
            {
                ////Search on NPI Number
                if (!string.IsNullOrEmpty(NPINumber))
                {
                    List<SearchResultForCred> result = new List<SearchResultForCred>();

                    var profiles = profileRepository.GetAll(includeProperties).Where(o => o.OtherIdentificationNumber != null);
                    var npi = profiles.Any(a => a.OtherIdentificationNumber.NPINumber.Equals(NPINumber) && a.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString());
                    if (!npi)
                        return result;
                    var providerSearchResult = (from provider in profiles
                                                where provider.OtherIdentificationNumber.NPINumber.Equals(NPINumber)
                                                select ConstructProviderSearchResultForCred(provider)).First();

                    result.Add(providerSearchResult);
                    return result;//providerSearchResult.ToList<ProviderSearchResultDTO>();


                }
                // Search on First Name
                else if (!string.IsNullOrEmpty(firstName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.FirstName.ToLower().Contains(firstName.ToLower())
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResultForCred(provider);


                    return providerSearchResult.ToList<SearchResultForCred>();
                }
                // Search on Last Name
                else if (!string.IsNullOrEmpty(lastName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.LastName.ToLower().Contains(lastName.ToLower())
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResultForCred(provider);

                    return providerSearchResult.ToList<SearchResultForCred>();
                }
                // Search on CAQH
                else if (!string.IsNullOrEmpty(CAQH))
                {
                    List<SearchResultForCred> result = new List<SearchResultForCred>();

                    var profiles = profileRepository.GetAll(includeProperties).Where(o => o.OtherIdentificationNumber.CAQHNumber != null);


                    var caqh = profiles.Any(a => a.OtherIdentificationNumber.CAQHNumber.Equals(CAQH) && a.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString());
                    if (!caqh)
                        return result;
                    var providerSearchResult = (from provider in profiles
                                                where provider.OtherIdentificationNumber.CAQHNumber.Equals(CAQH)
                                                select ConstructProviderSearchResultForCred(provider)).First();

                    result.Add(providerSearchResult);
                    return result;//providerSearchResult.ToList<ProviderSearchResultDTO>();


                }
                // Search on IPA Group Name
                else if (!string.IsNullOrEmpty(IPAGroupName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower()))
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResultForCred(provider);

                    return providerSearchResult.ToList<SearchResultForCred>();
                }
                // Search on Provider Level
                else if (!string.IsNullOrEmpty(specialty))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.SpecialtyDetails.Any(s => s.Specialty.Name.ToLower() == specialty.ToLower())
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResultForCred(provider);

                    return providerSearchResult.ToList<SearchResultForCred>();
                }
                // Search on Provider Type
                else if (!string.IsNullOrEmpty(providerType))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.ProviderTitles.Any(ci => ci.ProviderType.Title.ToLower() == providerType.ToLower())
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResultForCred(provider);

                    return providerSearchResult.ToList<SearchResultForCred>();
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

        public List<CredentialingInfo> SearchProviderProfileForReCred(string NPINumber = null, string firstName = null, string lastName = null, string specialty = null, string IPAGroupName = null, string CAQH = null, string providerType = null)
        {
            string includeProperties = "Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,Profile.SpecialtyDetails.Specialty,Profile.PersonalDetail.ProviderTitles.ProviderType,Profile.ContractInfoes.ContractGroupInfoes.PracticingGroup.Group,CredentialingLogs, CredentialingContractRequests.ContractGrid.BusinessEntity, CredentialingContractRequests.ContractGrid.LOB, CredentialingContractRequests.ContractGrid.ProfilePracticeLocation.Facility, CredentialingContractRequests.ContractGrid.ProfileSpecialty.Specialty, CredentialingContractRequests.ContractGrid.Report";
            var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();

            try
            {
                ////Search on NPI Number
                if (!string.IsNullOrEmpty(NPINumber))
                {
                    List<CredentialingInfo> result = new List<CredentialingInfo>();
                    var providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                               where provider.Profile.OtherIdentificationNumber.NPINumber.Equals(NPINumber)
                                               select provider;
                    List<CredentialingInfo> result1 = new List<CredentialingInfo>();
                    int count = 0;
                    List<string> arr = new List<string>();
                    foreach (var p in providerSearchResult)
                    {
                        if (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {
                            if (count == 0)
                            {
                                arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                result1.Add(p);
                                count++;
                            }
                            else
                            {
                                var count1 = 0;
                                for (var i = 0; i < arr.Count; i++)
                                {
                                    if (p.Profile.OtherIdentificationNumber.NPINumber == arr[i])
                                    {
                                        count1++;
                                    }
                                }
                                if (count1 == 0)
                                {
                                    arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                    result1.Add(p);
                                    count++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    foreach (var p in result1)
                    {
                        if ((p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {
                            foreach (CredentialingContractRequest ContractObj in p.CredentialingContractRequests)
                            {

                                foreach (ContractGrid GridObj in ContractObj.ContractGrid)
                                {

                                    GridObj.CredentialingInfo = null;

                                }

                            }
                            result.Add(p);
                        }
                    }
                    return result;

                }
                // Search on First Name
                else if (!string.IsNullOrEmpty(firstName))
                {
                    List<CredentialingInfo> result = new List<CredentialingInfo>();
                    IEnumerable<CredentialingInfo> providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                                                          where provider.Profile.PersonalDetail.FirstName.ToLower().Contains(firstName.ToLower())
                                                                          select provider;
                    List<CredentialingInfo> result1 = new List<CredentialingInfo>();
                    int count = 0;
                    List<string> arr = new List<string>();
                    foreach (var p in providerSearchResult)
                    {
                        if (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString())
                        {
                            if (count == 0)
                            {
                                arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                result1.Add(p);
                                count++;
                            }
                            else
                            {
                                var count1 = 0;
                                for (var i = 0; i < arr.Count; i++)
                                {
                                    if (p.Profile.OtherIdentificationNumber.NPINumber == arr[i])
                                    {
                                        count1++;
                                    }
                                }
                                if (count1 == 0)
                                {
                                    arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                    result1.Add(p);
                                    count++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    foreach (CredentialingInfo p in result1)
                    {
                        if ((p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {

                            foreach (CredentialingContractRequest ContractObj in p.CredentialingContractRequests)
                            {

                                foreach (ContractGrid GridObj in ContractObj.ContractGrid)
                                {

                                    GridObj.CredentialingInfo = null;

                                }

                            }

                            result.Add(p);
                        }
                    }

                    return result;
                }
                // Search on Last Name
                else if (!string.IsNullOrEmpty(lastName))
                {
                    List<CredentialingInfo> result = new List<CredentialingInfo>();
                    var providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                               where provider.Profile.PersonalDetail.LastName.ToLower().Contains(lastName.ToLower())
                                               select provider;

                    List<CredentialingInfo> result1 = new List<CredentialingInfo>();
                    int count = 0;
                    List<string> arr = new List<string>();
                    foreach (var p in providerSearchResult)
                    {
                        if (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {
                            if (count == 0)
                            {
                                arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                result1.Add(p);
                                count++;
                            }
                            else
                            {
                                var count1 = 0;
                                for (var i = 0; i < arr.Count; i++)
                                {
                                    if (p.Profile.OtherIdentificationNumber.NPINumber == arr[i])
                                    {
                                        count1++;
                                    }
                                }
                                if (count1 == 0)
                                {
                                    arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                    result1.Add(p);
                                    count++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }



                    foreach (CredentialingInfo p in result1)
                    {
                        if ((p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {
                            foreach (CredentialingContractRequest ContractObj in p.CredentialingContractRequests)
                            {

                                foreach (ContractGrid GridObj in ContractObj.ContractGrid)
                                {

                                    GridObj.CredentialingInfo = null;

                                }

                            }
                            result.Add(p);
                        }
                    }
                    return result;
                }
                // Search on CAQH
                else if (!string.IsNullOrEmpty(CAQH))
                {
                    List<CredentialingInfo> result = new List<CredentialingInfo>();
                    var reCredInfo = credentialingInitiationInfoRepo.GetAll(includeProperties).Where(o => o.Profile.OtherIdentificationNumber.CAQHNumber != null);
                    var npi = reCredInfo.Any(a => a.Profile.OtherIdentificationNumber.CAQHNumber.Equals(CAQH) && a.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString());
                    if (!npi)
                        return result;
                    var providerSearchResult = from provider in reCredInfo
                                               where provider.Profile.OtherIdentificationNumber.CAQHNumber.Equals(CAQH)
                                               select provider;
                    List<CredentialingInfo> result1 = new List<CredentialingInfo>();
                    int count = 0;
                    List<string> arr = new List<string>();
                    foreach (var p in providerSearchResult)
                    {
                        if (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {
                            if (count == 0)
                            {
                                arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                result1.Add(p);
                                count++;
                            }
                            else
                            {
                                var count1 = 0;
                                for (var i = 0; i < arr.Count; i++)
                                {
                                    if (p.Profile.OtherIdentificationNumber.NPINumber == arr[i])
                                    {
                                        count1++;
                                    }
                                }
                                if (count1 == 0)
                                {
                                    arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                    result1.Add(p);
                                    count++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    foreach (CredentialingInfo p in result1)
                    {
                        if ((p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {
                            foreach (CredentialingContractRequest ContractObj in p.CredentialingContractRequests)
                            {

                                foreach (ContractGrid GridObj in ContractObj.ContractGrid)
                                {

                                    GridObj.CredentialingInfo = null;

                                }

                            }
                            result.Add(p);
                        }
                    }

                    return result;


                }
                //Search on IPA Group Name
                else if (!string.IsNullOrEmpty(IPAGroupName))
                {
                    List<CredentialingInfo> result = new List<CredentialingInfo>();
                    var providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                               where provider.Profile.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower()))
                                               select provider;
                    List<CredentialingInfo> result1 = new List<CredentialingInfo>();
                    int count = 0;
                    List<string> arr = new List<string>();
                    foreach (var p in providerSearchResult)
                    {
                        if (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {
                            if (count == 0)
                            {
                                arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                result1.Add(p);
                                count++;
                            }
                            else
                            {
                                var count1 = 0;
                                for (var i = 0; i < arr.Count; i++)
                                {
                                    if (p.Profile.OtherIdentificationNumber.NPINumber == arr[i])
                                    {
                                        count1++;
                                    }
                                }
                                if (count1 == 0)
                                {
                                    arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                    result1.Add(p);
                                    count++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    foreach (CredentialingInfo p in result1)
                    {
                        if ((p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {
                            foreach (CredentialingContractRequest ContractObj in p.CredentialingContractRequests)
                            {

                                foreach (ContractGrid GridObj in ContractObj.ContractGrid)
                                {

                                    GridObj.CredentialingInfo = null;

                                }

                            }
                            result.Add(p);
                        }
                    }
                    return result;
                }
                // Search on Provider Level
                else if (!string.IsNullOrEmpty(specialty))
                {
                    List<CredentialingInfo> result = new List<CredentialingInfo>();
                    var providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                               where provider.Profile.SpecialtyDetails.Any(s => s.Specialty.Name.ToLower() == specialty.ToLower())
                                               select provider;
                    List<CredentialingInfo> result1 = new List<CredentialingInfo>();
                    int count = 0;
                    List<string> arr = new List<string>();
                    foreach (var p in providerSearchResult)
                    {
                        if (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {
                            if (count == 0)
                            {
                                arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                result1.Add(p);
                                count++;
                            }
                            else
                            {
                                var count1 = 0;
                                for (var i = 0; i < arr.Count; i++)
                                {
                                    if (p.Profile.OtherIdentificationNumber.NPINumber == arr[i])
                                    {
                                        count1++;
                                    }
                                }
                                if (count1 == 0)
                                {
                                    arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                    result1.Add(p);
                                    count++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    foreach (CredentialingInfo p in result1)
                    {
                        if ((p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {
                            foreach (CredentialingContractRequest ContractObj in p.CredentialingContractRequests)
                            {

                                foreach (ContractGrid GridObj in ContractObj.ContractGrid)
                                {

                                    GridObj.CredentialingInfo = null;

                                }

                            }
                            result.Add(p);
                        }
                    }
                    return result;
                }
                // Search on Provider Type
                else if (!string.IsNullOrEmpty(providerType))
                {
                    List<CredentialingInfo> result = new List<CredentialingInfo>();
                    var providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                               where provider.Profile.PersonalDetail.ProviderTitles.Any(ci => ci.ProviderType.Title.ToLower() == providerType.ToLower())
                                               select provider;
                    List<CredentialingInfo> result1 = new List<CredentialingInfo>();
                    int count = 0;
                    List<string> arr = new List<string>();
                    foreach (var p in providerSearchResult)
                    {
                        if (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {
                            if (count == 0)
                            {
                                arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                result1.Add(p);
                                count++;
                            }
                            else
                            {
                                var count1 = 0;
                                for (var i = 0; i < arr.Count; i++)
                                {
                                    if (p.Profile.OtherIdentificationNumber.NPINumber == arr[i])
                                    {
                                        count1++;
                                    }
                                }
                                if (count1 == 0)
                                {
                                    arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                                    result1.Add(p);
                                    count++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    foreach (CredentialingInfo p in result1)
                    {
                        if ((p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                        {
                            foreach (CredentialingContractRequest ContractObj in p.CredentialingContractRequests)
                            {

                                foreach (ContractGrid GridObj in ContractObj.ContractGrid)
                                {

                                    GridObj.CredentialingInfo = null;

                                }

                            }
                            result.Add(p);
                        }
                    }
                    return result;
                }
                else
                {
                    throw new Exception("New Manager :");
                }

            }
            catch (ApplicationException ex)
            {
                throw ex;
            }

            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.ALL_PROFILE_GET_EXCEPTION, ex);
            }
        }
        public List<CredentialingInfo> SearchProviderProfileForDeCred(string NPINumber = null, string firstName = null, string lastName = null, string specialty = null, string IPAGroupName = null, string CAQH = null, string providerType = null)
        {
            string includeProperties = "Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,Profile.SpecialtyDetails.Specialty,Profile.PersonalDetail.ProviderTitles.ProviderType,Profile.ContractInfoes.ContractGroupInfoes.PracticingGroup.Group,CredentialingLogs, CredentialingContractRequests.ContractGrid.BusinessEntity, CredentialingContractRequests.ContractGrid.LOB, CredentialingContractRequests.ContractGrid.ProfilePracticeLocation.Facility, CredentialingContractRequests.ContractGrid.ProfileSpecialty.Specialty, CredentialingContractRequests.ContractGrid.Report";
            var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();

            try
            {
                ////Search on NPI Number
                if (!string.IsNullOrEmpty(NPINumber))
                {
                    var providerSearchResultAll = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                                  where provider.Profile.OtherIdentificationNumber.NPINumber.Equals(NPINumber)
                                                   && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                                  select provider;
                    return DeCredSearchResult(providerSearchResultAll);

                }
                // Search on First Name
                else if (!string.IsNullOrEmpty(firstName))
                {
                    IEnumerable<CredentialingInfo> providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                                                          where provider.Profile.PersonalDetail.FirstName.ToLower().Contains(firstName.ToLower())
                                                                           && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                                                          select provider;

                    return DeCredSearchResult(providerSearchResult);
                }
                // Search on Last Name
                else if (!string.IsNullOrEmpty(lastName))
                {
                    var providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                               where provider.Profile.PersonalDetail.LastName.ToLower().Contains(lastName.ToLower())
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select provider;
                    return DeCredSearchResult(providerSearchResult);
                    
                }
                // Search on CAQH
                else if (!string.IsNullOrEmpty(CAQH))
                {
                    List<CredentialingInfo> result = new List<CredentialingInfo>();
                    var reCredInfo = credentialingInitiationInfoRepo.GetAll(includeProperties).Where(o => o.Profile.OtherIdentificationNumber.CAQHNumber != null);
                    var npi = reCredInfo.Any(a => a.Profile.OtherIdentificationNumber.CAQHNumber.Equals(CAQH) && a.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString());
                    if (!npi)
                        return result;
                    var providerSearchResult = from provider in reCredInfo
                                                where provider.Profile.OtherIdentificationNumber.CAQHNumber.Equals(CAQH)
                                                select provider;

                    return DeCredSearchResult(providerSearchResult);



                }
                //Search on IPA Group Name
                else if (!string.IsNullOrEmpty(IPAGroupName))
                {
                    var providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                               where provider.Profile.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower()))
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select provider;
                    return DeCredSearchResult(providerSearchResult);
                }
                // Search on Provider Level
                else if (!string.IsNullOrEmpty(specialty))
                {
                    var providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                               where provider.Profile.SpecialtyDetails.Any(s => s.Specialty.Name.ToLower() == specialty.ToLower())
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select provider;

                    return DeCredSearchResult(providerSearchResult);
                }
                // Search on Provider Type
                else if (!string.IsNullOrEmpty(providerType))
                {
                    var providersearchresult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                               where provider.Profile.PersonalDetail.ProviderTitles.Any(ci => ci.ProviderType.Title.ToLower() == providerType.ToLower())
                                                && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                               select provider;
                    return DeCredSearchResult(providersearchresult);

                    
                }
                else
                {
                    throw new Exception("New Manager :");
                }

            }
            catch (ApplicationException ex)
            {
                throw ex;
            }

            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.ALL_PROFILE_GET_EXCEPTION, ex);
            }
        }

        private static List<CredentialingInfo> DeCredSearchResult(IEnumerable<CredentialingInfo> providerSearchResultAll)
        {
            List<CredentialingInfo> result = new List<CredentialingInfo>();
            List<CredentialingInfo> result1 = new List<CredentialingInfo>();
            int count = 0;
            List<string> arr = new List<string>();
            foreach (var p in providerSearchResultAll)
            {
                if (!(p.CredentialingLogs.Any(x=>x.Credentialing==AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialing.ToString())))
                {
                    if (count == 0)
                    {
                        arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                        result1.Add(p);
                        count++;
                    }
                    else
                    {
                        var count1 = 0;
                        for (var i = 0; i < arr.Count; i++)
                        {
                            if (p.Profile.OtherIdentificationNumber.NPINumber == arr[i])
                            {
                                count1++;
                            }
                        }
                        if (count1 == 0)
                        {
                            arr.Add(p.Profile.OtherIdentificationNumber.NPINumber);
                            result1.Add(p);
                            count++;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }



            foreach (CredentialingInfo p in result1)
            {
                if ((p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                {
                    foreach (CredentialingContractRequest ContractObj in p.CredentialingContractRequests)
                    {

                        foreach (ContractGrid GridObj in ContractObj.ContractGrid)
                        {

                            GridObj.CredentialingInfo = null;

                        }

                    }
                    result.Add(p);
                }
            }
            return result;
        }


        private SearchResultForCred ConstructProviderSearchResultForCred(Profile provider)
        {
            if (provider == null) return null;

            return new SearchResultForCred
            {
                ProfileID = provider.ProfileID,
                ProfilePhotoPath = provider.ProfilePhotoPath,
                NPINumber = (provider.OtherIdentificationNumber == null) ? "Not Available" : provider.OtherIdentificationNumber.NPINumber,
                CAQH = (provider.OtherIdentificationNumber == null) ? "Not Available" : provider.OtherIdentificationNumber.CAQHNumber,
                Titles = GetTitles(provider),
                FirstName = (provider.PersonalDetail == null) ? "Not Available" : provider.PersonalDetail.FirstName,
                LastName = (provider.PersonalDetail == null) ? "Not Available" : provider.PersonalDetail.LastName,
                Specialties = GetSpecialties(provider),
                IPAGroupNames = GetGroupNames(provider)
            };
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

        private List<string> GetSpecialties(Profile provider)
        {
            List<string> spl = new List<string>();
            foreach (var specialty in provider.SpecialtyDetails)
            {
                if (specialty.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                {
                    spl.Add(specialty.Specialty.Name);
                }


            }
            return spl;
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

        public List<Profile> SearchProfileForViewEdit(string NPINumber = null, string firstName = null, string lastName = null, string providerRelationship = null, string IPAGroupName = null, string providerLevel = null, string providerType = null)
        {
            string includeProperties = "OtherIdentificationNumber,PersonalDetail.ProviderTitles.ProviderType,PersonalDetail.ProviderLevel,ContractInfoes.ContractGroupInfoes.PracticingGroup.Group";

            try
            {
                //Search on NPI Number
                if (!string.IsNullOrEmpty(NPINumber))
                {
                    List<Profile> result = new List<Profile>();

                    var profiles = profileRepository.GetAll(includeProperties).Where(o => o.OtherIdentificationNumber != null);

                    var npi = profiles.Any(a => a.OtherIdentificationNumber.NPINumber.Equals(NPINumber));

                    if (!npi)
                        return result;

                    var providerSearchResult = (from provider in profiles
                                                where provider.OtherIdentificationNumber.NPINumber.Equals(NPINumber)
                                                select ConstructProfileSearchResult(provider)).First();

                    result.Add(providerSearchResult);
                    return result;
                }

                // Search on First Name
                else if (!string.IsNullOrEmpty(firstName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.FirstName.ToLower().Contains(firstName.ToLower())
                                               select ConstructProfileSearchResult(provider);

                    return providerSearchResult.ToList<Profile>();
                }

                // Search on Last Name
                else if (!string.IsNullOrEmpty(lastName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.LastName.ToLower().Contains(lastName.ToLower())
                                               select ConstructProfileSearchResult(provider);

                    return providerSearchResult.ToList<Profile>();
                }

                // Search on Provider Relationship
                else if (!string.IsNullOrEmpty(providerRelationship))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.ContractInfoes.Any(ci => ci.ProviderRelationship == providerRelationship
                                                && ci.ContractStatusOption == AHC.CD.Entities.MasterData.Enums.ContractStatus.Accepted)
                                               select ConstructProfileSearchResult(provider);

                    return providerSearchResult.ToList<Profile>();
                }

                // Search on IPA Group Name
                else if (!string.IsNullOrEmpty(IPAGroupName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower()))
                                               select ConstructProfileSearchResult(provider);

                    return providerSearchResult.ToList<Profile>();
                }

                // Search on Provider Level
                else if (!string.IsNullOrEmpty(providerLevel))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.ProviderLevel.Name == providerLevel
                                               select ConstructProfileSearchResult(provider);

                    return providerSearchResult.ToList<Profile>();
                }

                // Search on Provider Type
                else if (!string.IsNullOrEmpty(providerType))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.ProviderTitles.Any(ci => ci.ProviderType.Title.ToLower() == providerType.ToLower())
                                               select ConstructProfileSearchResult(provider);

                    return providerSearchResult.ToList<Profile>();
                }
                else
                {
                    throw new Exception("Search Manager: Unable to find profiles with the given search data");
                }

            }
            catch (ApplicationException)
            {
                throw;
            }
        }
    }
}
