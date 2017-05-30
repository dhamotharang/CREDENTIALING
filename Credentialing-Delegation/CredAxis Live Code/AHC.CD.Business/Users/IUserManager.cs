using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.Notification;

namespace AHC.CD.Business.Users
{
    public interface IUserManager
    {
        int? GetProfileId(string userId);
        int? GetCDuserIdforLogin(string userId);
        Profile getProfileById(int profileId);
        Task CreateProfile(string userId, Entities.MasterProfile.Profile profile);
        Task<IEnumerable<object>> GetProfileForTeamLeadOperation(string userId, string roleName);

        Task<IEnumerable<CDUser>> GetMigratedUsers();
        Task UpdateMigratedUsers();
        Task AddSampleUsers(List<CDUser> dbUsers);
        Task<int> InitiateProviderAsync(string authenticateUserId, Profile profile, DocumentDTO cvDocument, DocumentDTO contractDocument);
        Task<int> InitiateUserAsync(string authenticateUserId, ProfileUser profileUser);
        Task<string> ActivateUserAsync(string AuthenticateUserId);
        Task<string> DeactivateUserAsync(string AuthenticateUserId);
        Task ActivateUserAsyncForProviderUser(string AuthenticateUserId);
        Task DeactivateUserAsyncForProviderUser(string AuthenticateUserId);
        Task<UserDashboardNotification> MarkNotificationAsRead(int dashboardNotificationID);
        List<CDUser> GetAllUsers();
        Task<IEnumerable<CDUser>> GetAllCDUsers();
        IEnumerable<CDUser> GetAllCDUsers1();
        Task<int> GetCDUserID(string userAuthID);
    }
}
