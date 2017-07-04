using AHC.CD.Business.Credentialing;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.UserInfo;
using AHC.CD.Exceptions.Profiles;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

        public async Task<List<SearchUserforGroupMailDTO>> GetAllUsersForGroupMailAsync()
        {
            try
            {
                List<SearchUserforGroupMailDTO> Users = new List<SearchUserforGroupMailDTO>();
                string IncludeProperties = "CDRoles.CDRole,Profile,Profile.PersonalDetail.ProviderLevel,Profile.ContractInfoes.ContractGroupInfoes.PracticingGroup";
                var CDUsersResultData = await uow.GetGenericRepository<CDUser>().GetAllAsync(IncludeProperties);
                var OtherUserResultData = await uow.GetGenericRepository<OtherUser>().GetAllAsync();

                CDUsersResultData.ToList().ForEach(x =>
                {
                    var constructedData = ConstructUserSearchResultForGroupMail(x);
                    if (constructedData != null)
                        Users.Add(constructedData);
                });
                OtherUserResultData.ToList().ForEach(x =>
                {
                    Users.Add(new SearchUserforGroupMailDTO { CDuserId = x.CDUserID, EmailIds = x.EmailId, FirstName = x.FirstName, LastName = x.LastName, UserType = "OtherUser", FullName = x.FullName });
                });
                return Users;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<SearchUserforGroupMailDTO>> SearchUserforGroupMail(string firstName = null, string IPAGroupName = null, string providerLevel = null)
        {
            string IncludeProperties = "Profile,Profile.PersonalDetail.ProviderLevel,Profile.ContractInfoes.ContractGroupInfoes.PracticingGroup";
            try
            {
                var cduserrepo = uow.GetGenericRepository<CDUser>();
                var result = await cduserrepo.GetAllAsync(IncludeProperties);
                var providersresult = from user in result
                                      //where user.Profile != null
                                      select user;

                if (!string.IsNullOrEmpty(firstName))
                {
                    var userSearchResult = from user in providersresult
                                           where (user.Profile != null && user.Profile.PersonalDetail.FirstName.ToLower().Contains(firstName.ToLower()))
                                           && user.Status != StatusType.Inactive.ToString()
                                           select ConstructUserSearchResult(user);

                    //var usersearchresult1 = from user in providersresult
                    //                        where user.EmailId.ToLower().Contains(firstName.ToLower())
                    //                        && user.Status != StatusType.Inactive.ToString()
                    //                        select ConstructUserSearchResult(user);

                    //userSearchResult = userSearchResult.ToList<SearchUserforGroupMailDTO>().AddRange(usersearchresult1.ToList<SearchUserforGroupMailDTO>());
                    return userSearchResult.ToList();
                    //return userSearchResult;
                }
                else if (!string.IsNullOrEmpty(IPAGroupName))
                {
                    var userSearchResult = from user in providersresult
                                           where user.Profile != null && user.Profile.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower()) && ci.ContractStatusOption == ContractStatus.Accepted
                                           && user.Status != StatusType.Inactive.ToString())
                                           select ConstructUserSearchResult(user);

                    return userSearchResult.ToList<SearchUserforGroupMailDTO>();
                }
                // Search on Provider Level
                else if (!string.IsNullOrEmpty(providerLevel))
                {
                    var userSearchResult = from user in providersresult
                                           where user.Profile != null && user.Profile.PersonalDetail.ProviderLevel.Name == providerLevel
                                           && user.Status != StatusType.Inactive.ToString()
                                           select ConstructUserSearchResult(user);

                    return userSearchResult.ToList<SearchUserforGroupMailDTO>();
                }
                else
                {
                    throw new Exception("Search Manager: Unable to find users with the given search data");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private SearchUserforGroupMailDTO ConstructUserSearchResult(CDUser user)
        {
            if (user == null) return null;

            return new SearchUserforGroupMailDTO
            {
                CDuserId = user.CDUserID,
                EmailIds = user.EmailId,
                FirstName = user.Profile.PersonalDetail.FirstName,
                LastName = user.Profile.PersonalDetail.LastName
            };
        }
        private SearchUserforGroupMailDTO ConstructUserSearchResultForGroupMail(CDUser Cduser)
        {
            SearchUserforGroupMailDTO user = null;
            if (Cduser.Profile == null && Cduser.Status != StatusType.Inactive.ToString() && Cduser.AuthenicateUserId != null)
            {
                user = new SearchUserforGroupMailDTO();
                user.CDuserId = Cduser.CDUserID;
                user.EmailIds = Cduser.EmailId;
                user.FullName = Cduser.AuthenicateUserId;
                user.Roles = Cduser.CDRoles.ToList().Select(x => x.CDRole.Name).ToList();
                user.UserType = "User";
            }
            else if (Cduser.Profile != null && Cduser.Profile.Status != StatusType.Inactive.ToString())
            {
                user = new SearchUserforGroupMailDTO();
                user.CDuserId = Cduser.CDUserID;
                user.EmailIds = Cduser.EmailId;
                user.FirstName = Cduser.Profile.PersonalDetail.FirstName;
                user.LastName = Cduser.Profile.PersonalDetail.LastName;
                user.FullName = user.FirstName + " " + (Cduser.Profile.PersonalDetail.MiddleName != null ? Cduser.Profile.PersonalDetail.MiddleName : "") + " " + user.LastName;
                user.Roles = Cduser.CDRoles.ToList().Select(x => x.CDRole.Name).ToList();
                user.ProviderLevel = Cduser.Profile.PersonalDetail.ProviderLevel == null ? null : Cduser.Profile.PersonalDetail.ProviderLevel.Name;
                user.ProviderRelationship = Cduser.Profile.ContractInfoes.Any(x => x.ContractStatus != ContractStatus.Inactive.ToString()) == false ? null : Cduser.Profile.ContractInfoes.Where(x => x.ContractStatus != ContractStatus.Inactive.ToString()).FirstOrDefault().ProviderRelationship;
                //user.ProviderRelationship = Cduser.Profile.ContractInfoes != null ? GetProfileRelationships(Cduser.Profile) : null;
                //user.IPA = Cduser.Profile.ContractInfoes.Any(x => x.ContractStatus != ContractStatus.Inactive.ToString()) == false ? null : Cduser.Profile.ContractInfoes.Where(x => x.ContractStatus != ContractStatus.Inactive.ToString()).FirstOrDefault().ContractGroupInfoes.Select(x=>x.PracticingGroup.Group.Name).ToList();
                user.IPA = Cduser.Profile.ContractInfoes != null ? GetGroupNames(Cduser.Profile) : null;
                user.ProfileImagePath = Cduser.Profile.ProfilePhotoPath;
                user.UserType = "Provider";
                user.NPINumber = Cduser.Profile.OtherIdentificationNumber.NPINumber.ToString();
            }
            //else if (Cduser.Profile == null && Cduser.Status != StatusType.Inactive.ToString() && Cduser.AuthenicateUserId == null)
            //{
            //    var OtherUser = uow.GetGenericRepository<OtherUser>().Find(x => x.CDUserID == Cduser.CDUserID);
            //    if (OtherUser != null)
            //    {
            //        user.FirstName = OtherUser.FirstName;
            //        user.LastName = OtherUser.LastName;
            //        user.EmailIds = OtherUser.EmailId;
            //        user.CDuserId = OtherUser.CDUserID;
            //        user.UserType = "OtherUser";
            //    }
            //}

            return user;
        }

        /// <summary>
        /// Search Provider for Profile View or Profile Edit
        /// </summary>
        /// <param name="providerSearchRequestDTO"></param>
        /// <returns></returns>
        public List<ProviderSearchResultDTO> SearchProviderProfileForViewEdit(string NPINumber = null, string firstName = null, string lastName = null, string providerRelationship = null, string IPAGroupName = null, string providerLevel = null, string providerType = null)
        {
            string includeProperties = "ContactDetail.EmailIDs,OtherIdentificationNumber,PersonalDetail.ProviderTitles.ProviderType,PersonalDetail.ProviderLevel,ContractInfoes.ContractGroupInfoes.PracticingGroup.Group";

            try
            {
                ////Search on NPI Number
                if (!string.IsNullOrEmpty(NPINumber))
                {
                    List<ProviderSearchResultDTO> result = new List<ProviderSearchResultDTO>();

                    var profiles = profileRepository.GetAll(includeProperties).Where(o => o.OtherIdentificationNumber != null);

                    //var profiles = profileRepository.Get(_ => 1 == 1, "OtherIdentificationNumber").Where(p => p.OtherIdentificationNumber.NPINumber.Equals(NPINumber));
                    //var npi = profiles.First().OtherIdentificationNumber.NPINumber;
                    var npi = profiles.Any(a => a.OtherIdentificationNumber.NPINumber.Equals(NPINumber) && a.Status != StatusType.Inactive.ToString());
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
                                                && provider.Status != StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResult(provider);


                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                // Search on Last Name
                else if (!string.IsNullOrEmpty(lastName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.LastName.ToLower().Contains(lastName.ToLower())
                                                && provider.Status != StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResult(provider);

                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                // Search on Provider Relationship
                else if (!string.IsNullOrEmpty(providerRelationship))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.ContractInfoes.Any(ci => ci.ProviderRelationship == providerRelationship && ci.ContractStatusOption == ContractStatus.Accepted && provider.Status == StatusType.Active.ToString())
                                                && provider.Status != StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResult(provider);

                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                // Search on IPA Group Name
                else if (!string.IsNullOrEmpty(IPAGroupName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower()) && ci.ContractStatusOption == ContractStatus.Accepted && provider.Status == StatusType.Active.ToString())
                                                && provider.Status != StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResult(provider);

                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                // Search on Provider Level
                else if (!string.IsNullOrEmpty(providerLevel))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.ProviderLevel.Name == providerLevel
                                                && provider.Status != StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResult(provider);

                    return providerSearchResult.ToList<ProviderSearchResultDTO>();
                }
                // Search on Provider Type
                else if (!string.IsNullOrEmpty(providerType))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.ProviderTitles.Any(ci => ci.ProviderType.Title.ToLower() == providerType.ToLower())
                                                && provider.Status != StatusType.Inactive.ToString()
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
                    var npi = profiles.Any(a => a.OtherIdentificationNumber.NPINumber.Equals(NPINumber) && a.Status != StatusType.Inactive.ToString());
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
                                                && provider.Status != StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResultForCred(provider);


                    return providerSearchResult.ToList<SearchResultForCred>();
                }
                // Search on Last Name
                else if (!string.IsNullOrEmpty(lastName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.LastName.ToLower().Contains(lastName.ToLower())
                                                && provider.Status != StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResultForCred(provider);

                    return providerSearchResult.ToList<SearchResultForCred>();
                }
                // Search on CAQH
                else if (!string.IsNullOrEmpty(CAQH))
                {
                    List<SearchResultForCred> result = new List<SearchResultForCred>();

                    var profiles = profileRepository.GetAll(includeProperties).Where(o => o.OtherIdentificationNumber.CAQHNumber != null);


                    var caqh = profiles.Any(a => a.OtherIdentificationNumber.CAQHNumber.Equals(CAQH) && a.Status != StatusType.Inactive.ToString());
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
                                               where provider.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower() && cgi.Status != StatusType.Inactive.ToString()))
                                                && provider.Status != StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResultForCred(provider);

                    return providerSearchResult.ToList<SearchResultForCred>();
                }
                // Search on Provider Level
                else if (!string.IsNullOrEmpty(specialty))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.SpecialtyDetails.Any(s => s.Specialty.Name.ToLower() == specialty.ToLower() && s.Status == StatusType.Active.ToString())
                                                && provider.Status != StatusType.Inactive.ToString()
                                               select ConstructProviderSearchResultForCred(provider);

                    return providerSearchResult.ToList<SearchResultForCred>();
                }
                // Search on Provider Type
                else if (!string.IsNullOrEmpty(providerType))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.ProviderTitles.Any(ci => ci.ProviderType.Title.ToLower() == providerType.ToLower() && ci.Status == "Active")
                                                && provider.Status != StatusType.Inactive.ToString()
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
                        if (p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                        if ((p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                        if (p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString())
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
                        if ((p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                        if (p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                        if ((p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                    var npi = reCredInfo.Any(a => a.Profile.OtherIdentificationNumber.CAQHNumber.Equals(CAQH) && a.Status != StatusType.Inactive.ToString());
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
                        if (p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                        if ((p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                                               where provider.Profile.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower() && cgi.Status == StatusType.Active.ToString()))
                                               select provider;
                    List<CredentialingInfo> result1 = new List<CredentialingInfo>();
                    int count = 0;
                    List<string> arr = new List<string>();
                    foreach (var p in providerSearchResult)
                    {
                        if (p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                        if ((p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                                               where provider.Profile.SpecialtyDetails.Any(s => s.Specialty.Name.ToLower() == specialty.ToLower() && s.Status == StatusType.Active.ToString())
                                               select provider;
                    List<CredentialingInfo> result1 = new List<CredentialingInfo>();
                    int count = 0;
                    List<string> arr = new List<string>();
                    foreach (var p in providerSearchResult)
                    {
                        if (p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                        if ((p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                                               where provider.Profile.PersonalDetail.ProviderTitles.Any(ci => ci.ProviderType.Title.ToLower() == providerType.ToLower() && ci.Status == "Active")
                                               select provider;
                    List<CredentialingInfo> result1 = new List<CredentialingInfo>();
                    int count = 0;
                    List<string> arr = new List<string>();
                    foreach (var p in providerSearchResult)
                    {
                        if (p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString() || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                        if ((p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                                                   && provider.Status != StatusType.Inactive.ToString()
                                                  select provider;
                    return DeCredSearchResult(providerSearchResultAll);

                }
                // Search on First Name
                else if (!string.IsNullOrEmpty(firstName))
                {
                    IEnumerable<CredentialingInfo> providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                                                          where provider.Profile.PersonalDetail.FirstName.ToLower().Contains(firstName.ToLower())
                                                                          && provider.Status != StatusType.Inactive.ToString()
                                                                          select provider;

                    return DeCredSearchResult(providerSearchResult);
                }
                // Search on Last Name
                else if (!string.IsNullOrEmpty(lastName))
                {
                    var providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                               where provider.Profile.PersonalDetail.LastName.ToLower().Contains(lastName.ToLower())
                                                && provider.Status != StatusType.Inactive.ToString()
                                               select provider;
                    return DeCredSearchResult(providerSearchResult);

                }
                // Search on CAQH
                else if (!string.IsNullOrEmpty(CAQH))
                {
                    List<CredentialingInfo> result = new List<CredentialingInfo>();
                    var reCredInfo = credentialingInitiationInfoRepo.GetAll(includeProperties).Where(o => o.Profile.OtherIdentificationNumber.CAQHNumber != null);
                    var npi = reCredInfo.Any(a => a.Profile.OtherIdentificationNumber.CAQHNumber.Equals(CAQH) && a.Status != StatusType.Inactive.ToString());
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
                                               where provider.Profile.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower() && cgi.Status == StatusType.Active.ToString()))
                                                && provider.Status != StatusType.Inactive.ToString()
                                               select provider;
                    return DeCredSearchResult(providerSearchResult);
                }
                // Search on Provider Level
                else if (!string.IsNullOrEmpty(specialty))
                {
                    var providerSearchResult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                               where provider.Profile.SpecialtyDetails.Any(s => s.Specialty.Name.ToLower() == specialty.ToLower() && s.Status == StatusType.Active.ToString())
                                                && provider.Status != StatusType.Inactive.ToString()
                                               select provider;

                    return DeCredSearchResult(providerSearchResult);
                }
                // Search on Provider Type
                else if (!string.IsNullOrEmpty(providerType))
                {
                    var providersearchresult = from provider in credentialingInitiationInfoRepo.GetAll(includeProperties)
                                               where provider.Profile.PersonalDetail.ProviderTitles.Any(ci => ci.ProviderType.Title.ToLower() == providerType.ToLower() && ci.Status == "Active")
                                                && provider.Status != StatusType.Inactive.ToString()
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
                if (!(p.CredentialingLogs.Any(x => x.Credentialing == CredentialingType.DeCredentialing.ToString())))
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
                if ((p.CredentialingLogs.Last().Credentialing == CredentialingType.Credentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.ReCredentialing.ToString()) || (p.CredentialingLogs.Last().Credentialing == CredentialingType.DeCredentialingInitiated.ToString()))
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
                IPAGroupNames = GetGroupNames(provider),
                EmailIds = GetEmailIds(provider)
            };
        }

        private string GetEmailIds(Profile provider)
        {
            try
            {
                string EmailId = "";
                if (provider.ContactDetail != null)
                {
                    foreach (var EmailDetail in provider.ContactDetail.EmailIDs)
                    {
                        if (EmailDetail.Status == StatusType.Active.ToString() && EmailDetail.Preference == PreferenceType.Primary.ToString())
                        {
                            EmailId = EmailDetail.EmailAddress;
                            break;
                        }
                        else if (EmailDetail.Status == StatusType.Active.ToString() && EmailDetail.Preference == PreferenceType.Secondary.ToString())
                        {
                            EmailId = EmailDetail.EmailAddress;
                            break;
                        }
                    }
                }
                return EmailId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<string> GetGroupNames(Profile provider)
        {
            List<string> groupNames = new List<string>();
            foreach (var contractInfo in provider.ContractInfoes)
            {
                if (contractInfo.ContractStatus == ContractStatus.Accepted.ToString())
                {
                    foreach (var contractGroupInfo in contractInfo.ContractGroupInfoes)
                    {
                        if (contractGroupInfo.ContractGroupStatus == ContractGroupStatus.Accepted.ToString() && contractGroupInfo.Status != StatusType.Inactive.ToString())
                        {
                            groupNames.Add(contractGroupInfo.PracticingGroup.Group.Name);
                        }

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
                if (contractInfo.ContractStatus == ContractStatus.Accepted.ToString())
                {
                    relationships.Add(contractInfo.ProviderRelationship);
                }


            }
            return relationships;
        }

        private List<string> GetSpecialties(Profile provider)
        {
            List<string> spl = new List<string>();
            if (provider.SpecialtyDetails.Count != 0)
            {
                foreach (var specialty in provider.SpecialtyDetails)
                {
                    if (specialty.Status == StatusType.Active.ToString() && specialty.Specialty != null)
                    {
                        spl.Add(specialty.Specialty.Name);
                    }
                }
                return spl;
            }
            else return spl;
        }

        private List<string> GetTitles(Profile provider)
        {
            List<string> titles = new List<string>();
            foreach (var title in provider.PersonalDetail.ProviderTitles)
            {
                if (title.Status == StatusType.Active.ToString())
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
                    return result.Where(p => !p.PersonalDetail.FirstName.Contains("Test_")).ToList();
                }

                // Search on First Name
                else if (!string.IsNullOrEmpty(firstName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.FirstName.ToLower().Contains(firstName.ToLower())
                                               select ConstructProfileSearchResult(provider);

                    return providerSearchResult.ToList<Profile>().Where(p => !p.PersonalDetail.FirstName.Contains("Test_")).ToList();

                }

                // Search on Last Name
                else if (!string.IsNullOrEmpty(lastName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.LastName.ToLower().Contains(lastName.ToLower())
                                               select ConstructProfileSearchResult(provider);

                    return providerSearchResult.ToList<Profile>().Where(p => !p.PersonalDetail.FirstName.Contains("Test_")).ToList();
                }

                // Search on Provider Relationship
                else if (!string.IsNullOrEmpty(providerRelationship))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.ContractInfoes.Any(ci => ci.ProviderRelationship == providerRelationship
                                                && ci.ContractStatusOption == ContractStatus.Accepted)
                                               select ConstructProfileSearchResult(provider);

                    return providerSearchResult.ToList<Profile>().Where(p => !p.PersonalDetail.FirstName.Contains("Test_")).ToList();
                }

                // Search on IPA Group Name
                else if (!string.IsNullOrEmpty(IPAGroupName))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.ContractInfoes.Any(ci => ci.ContractGroupInfoes.Any(cgi => cgi.PracticingGroup.Group.Name.ToLower() == IPAGroupName.ToLower()))
                                               select ConstructProfileSearchResult(provider);

                    return providerSearchResult.ToList<Profile>().Where(p => !p.PersonalDetail.FirstName.Contains("Test_")).ToList();
                }

                // Search on Provider Level
                else if (!string.IsNullOrEmpty(providerLevel))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.ProviderLevel.Name == providerLevel
                                               select ConstructProfileSearchResult(provider);

                    return providerSearchResult.ToList<Profile>().Where(p => !p.PersonalDetail.FirstName.Contains("Test_")).ToList();
                }

                // Search on Provider Type
                else if (!string.IsNullOrEmpty(providerType))
                {
                    var providerSearchResult = from provider in profileRepository.GetAll(includeProperties)
                                               where provider.PersonalDetail.ProviderTitles.Any(ci => ci.ProviderType.Title.ToLower() == providerType.ToLower())
                                               select ConstructProfileSearchResult(provider);

                    return providerSearchResult.ToList<Profile>().Where(p => !p.PersonalDetail.FirstName.Contains("Test_")).ToList();
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
