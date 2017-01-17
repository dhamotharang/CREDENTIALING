using AHC.CD.Data.Repository;
using AHC.CD.Entities;
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


        public async Task<IEnumerable<Entities.User>> GetMigratedUsers()
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

        public async Task AddSampleUsers(List<Entities.User> dbUsers)
        {
            userRepository.CreateRange(dbUsers);

            await userRepository.SaveAsync();

            for (int i = 3; i <= 7; i++)
            {
                dbUsers[i].UserRelation = new UserRelation()
                {
                    UserRoleRelations = new List<UserRoleRelation>() { new UserRoleRelation() { UserId = dbUsers[i + 5].UserID, RoleName = "PRO" } }
                };
            }

            await userRepository.SaveAsync();
        }
    }
}
