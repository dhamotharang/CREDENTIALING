using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.UserInfo;
using AHC.CD.Exceptions.Profiles;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    internal class ProfileDelegationManager : IProfileDelegationManager
    {
        private IRepositoryManager repositoryManager = null;
        private IProfileRepository profileRepository = null;
        private IUnitOfWork uof = null;

        public ProfileDelegationManager(IUnitOfWork uof, IRepositoryManager repositoryManager)
        {
            this.uof = uof;
            this.repositoryManager = repositoryManager;
            this.profileRepository = uof.GetProfileRepository(); ;            
        }

        public IEnumerable<ProfileUser> GetAllTeamLeadsAsync()
        {
            try
            {
                var profileUserRepo = uof.GetGenericRepository<ProfileUser>();
                var data1 = profileUserRepo.GetAll();
                var data = data1.Where(t => t.RoleCode.Equals("TL") && t.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString());
                //var data = await repositoryManager.GetAsync<ProfileUser>(t => t.RoleCode.Equals("TL") && t.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString());
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Methos to get all the roles in the system
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CDRole>> GetAllRoles()
        {
            try
            {
                var data = await repositoryManager.GetAsync<CDRole>(c => c.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString());
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get profile data
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public object GetProfileData(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //Personal Detail
                    "PersonalDetail.ProviderLevel",
                    "PersonalDetail.ProviderTitles.ProviderType",
                    "ContactDetail",
                    "OtherIdentificationNumber"
                };

                var profile = profileRepository.Find(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    PersonalDetail = profile.PersonalDetail,
                    ContactDetail = profile.ContactDetail,
                    OtherIdentificationNumber = profile.OtherIdentificationNumber
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get existing roles of a profile
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public List<CDUserRole> GetRolesForUser(int profileId)
        {
            try
            {
                var cdRoleRepo = uof.GetGenericRepository<CDUser>();
                CDUser user = cdRoleRepo.Find(c => c.ProfileId == profileId, "CDRoles.CDRole");
                foreach (var userRole in user.CDRoles)
                {
                    userRole.CDUser = null;
                    userRole.CDRole.CDUsers = null;
                }
                return user.CDRoles.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CDUserRole AddRole(int profileId, int roleID)
        {
            try
            {
                var userRepo = uof.GetGenericRepository<CDUser>();
                CDUser user = userRepo.Find(c => c.ProfileId == profileId);
                CDRole role = uof.GetGenericRepository<CDRole>().Find(c => c.CDRoleID == roleID);
                if (user.CDRoles.Any(c => c.CDRoleId == roleID))
                {
                    throw new ProfileDelegationManagerException(ExceptionMessage.DUPLICATE_ROLE_ADD_PROFILE);
                }
                user.CDRoles.Add(new CDUserRole { CDRoleId = role.CDRoleID, CDUserId = user.CDUserID });
                userRepo.Update(user);
                userRepo.Save();
                CDUserRole returnUserRole = userRepo.Find(c => c.ProfileId == profileId, "CDRoles.CDRole").CDRoles.First(c => c.CDRoleId == roleID);
                return returnUserRole;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task AssignProfile(int profileId, int profileUserId, string userId)
        {
            try
            {
                var CDUserRepo = uof.GetGenericRepository<CDUser>();
                var providerUserRepo = uof.GetGenericRepository<ProviderUser>();

                var isExist = providerUserRepo.Any(p => p.ProfileId == profileId);

                if (!isExist)
                {
                    var cdUser = CDUserRepo.Find(u => u.AuthenicateUserId == userId);

                    var profileUserRepo = uof.GetGenericRepository<ProfileUser>();
                    var teamLead = profileUserRepo.Find(l => l.ProfileUserID == profileUserId && l.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString());

                    ProviderUser provider = new ProviderUser() { ProfileId = profileId, AssignedByCDUserId = cdUser.CDUserID, AssignedDate = DateTime.Now, LastModifiedDate = DateTime.Now, StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active };

                    teamLead.ProvidersUser.Add(provider);

                    profileUserRepo.Update(teamLead);
                    await profileUserRepo.SaveAsync();
                }
                else
                {
                    throw new ProfileDelegationManagerException(ExceptionMessage.USER_ASSIGNED_EXCEPTION);
                }
                
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
