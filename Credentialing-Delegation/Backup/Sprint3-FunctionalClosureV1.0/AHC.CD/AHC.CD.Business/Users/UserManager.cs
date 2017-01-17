using AHC.CD.Data.Repository;
using AHC.CD.Entities.MasterData.Enums;
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

        public UserManager(IUnitOfWork uow)
        {
            this.userRepository = uow.GetUserRepository();
        }       
        
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
    }
}
