﻿using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Profiles;
using AHC.CD.Data.ADO.AspnetUser;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.Notification;
using AHC.CD.Entities.UserInfo;
using AHC.CD.Resources.Document;
using AHC.MailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Users
{
    internal class UserManager : IUserManager
    {
        IUserRepository userRepository = null;
        IUnitOfWork unitOfWork = null;
        IDocumentsManager documentManager = null;
        ProfileDocumentManager profileDocumentManager = null;
        IProfileRepository profileRepository = null;
        IEmailSender mailService = null;
        
        public UserManager(IUnitOfWork uow, IDocumentsManager documentManager, IEmailSender mailService, IUserDetails iUserDetails)
        {
            this.mailService = mailService;
            this.userRepository = uow.GetUserRepository();
            this.unitOfWork = uow;
            this.documentManager = documentManager;
            this.profileRepository = uow.GetProfileRepository();
            this.profileDocumentManager = new ProfileDocumentManager(profileRepository, documentManager);
            this.profileUserRepo = uow.GetGenericRepository<ProfileUser>();
            this.cdRoleRepository = uow.GetGenericRepository<CDRole>();
        }
        IGenericRepository<ProfileUser> profileUserRepo = null;
        IGenericRepository<CDRole> cdRoleRepository = null;
        public int? GetProfileId(string userId)
        {
            try
            {
                var user = userRepository.Find(u => u.AuthenicateUserId.Equals(userId));
                return user.ProfileId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int? GetCDuserIdforLogin(string userId)
        {
            try
            {
                var user = userRepository.Find(u => u.AuthenicateUserId.Equals(userId));
                if (user != null)
                    return user.CDUserID;
                else return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Profile getProfileById(int profileId)
        {
            var profileRepo = unitOfWork.GetGenericRepository<Profile>();
            Profile profile = profileRepo.Find(c => c.ProfileID == profileId, "PersonalDetail.ProviderLevel", "PersonalDetail.ProviderTitles.ProviderType", "ContractInfoes.ContractGroupInfoes.PracticingGroup", "ContractInfoes.ContractGroupInfoes.PracticingGroup.Group");
            return profile;
        }

        public async Task CreateProfile(string userId, Entities.MasterProfile.Profile profile)
        {
            try
            {
                var user = await userRepository.FindAsync(u => u.AuthenicateUserId.Equals(userId));
                user.Profile = profile;
                userRepository.Update(user);
                await userRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<object>> GetProfileForTeamLeadOperation(string userId, string roleName)
        {
            try
            {
                var user = await userRepository.FindAsync(u => u.AuthenicateUserId.Equals(userId));
                List<Object> profiles = new List<Object>();

                foreach (var item in user.UserRelation.UserRoleRelations.Where(r => r.RoleName.Equals(roleName)))
                {
                    if (item.User.Profile != null && item.User.Profile.Status.Equals(StatusType.Active.ToString()))
                        profiles.Add(new { ProfileID = item.User.Profile.ProfileID, PersonalDetail = item.User.Profile.PersonalDetail });
                }

                return profiles;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IEnumerable<Entities.CDUser>> GetMigratedUsers()
        {
            try
            {
                return await userRepository.GetAsync(u => u.AuthenicateUserId == null && u.ProfileId != null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateMigratedUsers()
        {
            try
            {
                await userRepository.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddSampleUsers(List<Entities.CDUser> dbUsers)
        {
            userRepository.CreateRange(dbUsers);

            await userRepository.SaveAsync();
            var roleManager = new CDRoleManager(unitOfWork);

            //asssign CCO role to the user
            await roleManager.AssignCCORoleAsync(dbUsers[1]);

            for (int i = 8; i <= 12; i++)
            {
                await roleManager.AssignProviderRoleAsync(dbUsers[i]);
            }

            for (int i = 3; i <= 7; i++)
            {
                await roleManager.AssignTeamLeadRoleAsync(dbUsers[1]);
                dbUsers[i].UserRelation = new CDUserRelation()
                {
                    UserRoleRelations = new List<CDUserRoleRelation>() { new CDUserRoleRelation() { UserId = dbUsers[i + 5].CDUserID, RoleName = "PRO" } }
                };
            }

            await userRepository.SaveAsync();
        }

        public async Task<int> InitiateUserAsync(string authenticateUserId, ProfileUser profileUser)
        {
            try
            {
                var id = await CreateUserWithRoleAsync(authenticateUserId, profileUser.RoleCode, profileUser.Email);

                profileUser.ActivationDate = DateTime.Now;
                profileUser.CDUserID = id;
                var profileUserId = await AddProfileUserAsync(profileUser);

                return profileUserId;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private async Task<int> CreateUserWithRoleAsync(string authenticateUserId, string role,string EmailId)
        {
            //Create a CD user and associate profile 
            CDUser cdUser = new CDUser() { AuthenicateUserId = authenticateUserId, StatusType = StatusType.Active, EmailId = EmailId };

            userRepository.Create(cdUser);
            await userRepository.SaveAsync();

            var userRole = cdRoleRepository.Find(r => r.Code.Equals(role));

            await new CDRoleManager(unitOfWork).AssignRoleForUser(cdUser, userRole.CDRoleID);
            return cdUser.CDUserID;
        }

        private async Task<int> AddProfileUserAsync(ProfileUser profileUser)
        {

            var user = profileUserRepo.Create(profileUser);

            //save the information in the repository
            await profileUserRepo.SaveAsync();

            return user.ProfileUserID;
        }

        public async Task<int> InitiateProviderAsync(string authenticateUserId, Profile profile, DocumentDTO cvDocument, DocumentDTO contractDocument)
        {
            var profileId = await CreateUserWithProviderRoleAsync(authenticateUserId, profile);

            await UpdateProviderIdAndContractAsync(profileId, contractDocument, cvDocument);

            return profileId;
        }

        private async Task<int> CreateUserWithProviderRoleAsync(string authenticateUserId, Profile profile)
        {
            //Create a CD user and associate profile 
            CDUser cdUser = new CDUser() { AuthenicateUserId = authenticateUserId, StatusType = StatusType.Active, Profile = profile, EmailId = profile.ContactDetail.EmailIDs.FirstOrDefault().EmailAddress };

            userRepository.Create(cdUser);
            await userRepository.SaveAsync();

            await new CDRoleManager(unitOfWork).AssignProviderRoleAsync(cdUser);
            return cdUser.ProfileId.Value;
        }

        private async Task UpdateProviderIdAndContractAsync(int profileId, DocumentDTO contractDocument, DocumentDTO cvDocument)
        {
            Profile updateProfile = profileRepository.Find(p => p.ProfileID == profileId, "ContractInfoes");

            // Set Provider ID
            updateProfile.ProviderID = SetProviderID(profileId);

            // Save Documents

            if (cvDocument != null)
            {
                updateProfile.CVInformation.CVDocumentPath = AddDocument(DocumentRootPath.CV_DOCUMENT_PATH, DocumentTitle.CV, null, cvDocument, profileId);
            }

            if (contractDocument != null)
            {
                updateProfile.ContractInfoes.ElementAt(0).ContractFilePath = AddDocument(DocumentRootPath.CONTRACT_DOCUMENT_PATH, DocumentTitle.CONTRACT, null, contractDocument, profileId);
            }

            //save the information in the repository
            await profileRepository.SaveAsync();
        }

        private string SetProviderID(int profileID)
        {
            string providerID;
            if (profileID <= 9)
                providerID = "PR000" + profileID;
            else if (profileID <= 99)
                providerID = "PR00" + profileID;
            else if (profileID <= 999)
                providerID = "PR0" + profileID;
            else
                providerID = "PR" + profileID;

            return providerID;
        }

        private string AddDocument(string docRootPath, string docTitle, DateTime? expiryDate, DocumentDTO document, int profileId)
        {
            //Create a profile document object
            ProfileDocument profileDocument = CreateProfileDocumentObject(docTitle, null, expiryDate);

            //Assign the Doc root path
            document.DocRootPath = docRootPath;

            //Add the document if uploaded
            return profileDocumentManager.AddDocument(profileId, document, profileDocument);
        }

        private ProfileDocument CreateProfileDocumentObject(string title, string docPath, DateTime? expiryDate)
        {
            return new ProfileDocument()
            {
                DocPath = docPath,
                Title = title,
                ExpiryDate = expiryDate
            };
        }

        /// <summary>
        /// Method to mark notification as read
        /// </summary>
        /// <param name="dashboardNotificationID"></param>
        /// <returns></returns>
        public async Task<UserDashboardNotification> MarkNotificationAsRead(int dashboardNotificationID)
        {
            var dashboardNotificationRepo = unitOfWork.GetGenericRepository<UserDashboardNotification>();
            UserDashboardNotification data = await dashboardNotificationRepo.FindAsync(x => x.UserDashboardNotificationID == dashboardNotificationID);
            data.AcknowledgementStatusType = AHC.CD.Entities.MasterData.Enums.AcknowledgementStatusType.Read;
            dashboardNotificationRepo.Update(data);
            dashboardNotificationRepo.Save();
            return data;
        }


        public async Task<IEnumerable<CDUser>> GetAllCDUsers()
        {
            try
            {
                var users = await userRepository.GetAllAsync("CDRoles, CDRoles.CDRole");

                return users.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<CDUser> GetAllCDUsers1()
        {
            try
            {
                var users = userRepository.GetAll();

                return users.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<string> ActivateUserAsync(string AuthenticateUserId)
        {
            string stat = "";
            try
            {
                var cduserId = await GetCDuserId(AuthenticateUserId);
                if (profileUserRepo.Any(y => y.CDUserID == cduserId))
                {
                    var res = await profileUserRepo.FindAsync(x => x.CDUserID == cduserId);
                    if (res != null)
                    {
                        res.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                        profileUserRepo.Update(res);
                        profileUserRepo.Save();
                    }
                }

                var res1 = await userRepository.FindAsync(y => y.CDUserID == cduserId);
                res1.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                userRepository.Update(res1);
                userRepository.Save();
                stat = "true";
                if (res1.Profile != null)
                {
                    var status = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    profileRepository.ReactivateProfile(res1.Profile.ProfileID, status);
                    await profileRepository.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return stat;
        }

        public async Task ActivateUserAsyncForProviderUser(string AuthenticateUserId)
        {
            try
            {
                var cduserId = await GetCDuserId(AuthenticateUserId);
                var res = await profileUserRepo.FindAsync(x => x.CDUserID == cduserId);
                var provideruserrepo = unitOfWork.GetGenericRepository<ProviderUser>();
                if (res != null)
                {
                    var res1 = provideruserrepo.Find(x => x.ProfileId == res.ProfileUserID);
                    if (res1 != null)
                    {
                        res1.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                        provideruserrepo.Update(res1);
                        provideruserrepo.Save();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<int> GetCDuserId(string authId)
        {
            var cduser = await userRepository.FindAsync(x => x.AuthenicateUserId == authId);
            return cduser.CDUserID;
        }


        public async Task<string> DeactivateUserAsync(string AuthenticateUserId)
        {
            string stat = "";
            try
            {
                var cduserId = await GetCDuserId(AuthenticateUserId);
                if (profileUserRepo.Any(y => y.CDUserID == cduserId))
                {
                    var res = await profileUserRepo.FindAsync(x => x.CDUserID == cduserId);
                    if (res != null)
                    {
                        res.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                        profileUserRepo.Update(res);
                        profileUserRepo.Save();
                    }
                }

                var res1 = await userRepository.FindAsync(y => y.CDUserID == cduserId);
                res1.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                userRepository.Update(res1);
                userRepository.Save();
                stat = "true";
                if (res1.Profile != null)
                {
                    var status = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                    profileRepository.DeactivateProfile(res1.Profile.ProfileID, status);
                    await profileRepository.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stat;
        }

        public async Task DeactivateUserAsyncForProviderUser(string AuthenticateUserId)
        {
            try
            {
                var cduserId = await GetCDuserId(AuthenticateUserId);
                var res = await profileUserRepo.FindAsync(x => x.CDUserID == cduserId);
                var provideruserrepo = unitOfWork.GetGenericRepository<ProviderUser>();
                if (res != null)
                {
                    var res1 = provideruserrepo.Find(x => x.ProfileId == res.ProfileUserID);
                    if (res1 != null)
                    {
                        res1.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                        provideruserrepo.Update(res1);
                        provideruserrepo.Save();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CDUser> GetAllUsers()
        {
            try
            {
                var cduserrolerepo = unitOfWork.GetGenericRepository<CDUserRole>();
                string IncludeProperties = "CDRoles.CDRole";
                var res = userRepository.Get(x => x.CDRoles.Any(y => y.CDRole.Code != "PRO"), IncludeProperties);
                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
    }
}