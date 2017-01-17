using AHC.CD.Data.Repository;
using AHC.CD.Entities;
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
        private IUnitOfWork uof = null;

        public ProfileDelegationManager(IUnitOfWork uof, IRepositoryManager repositoryManager)
        {
            this.uof = uof;
            this.repositoryManager = repositoryManager;
            
        }

        public async Task<IEnumerable<ProfileUser>> GetAllTeamLeadsAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<ProfileUser>(t => t.RoleCode.Equals("TL") && t.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString());
                return data;
            }
            catch (Exception)
            {

                throw;
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
