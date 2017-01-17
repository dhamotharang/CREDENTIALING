using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Users
{
    public interface IUserManager
    {
        int? GetProfileId(string userId);
        Task CreateProfile(string userId, Entities.MasterProfile.Profile profile);
        Task<IEnumerable<object>> GetProfileForTeamLeadOperation(string userId, string roleName);

        Task<IEnumerable<CDUser>> GetMigratedUsers();
        Task UpdateMigratedUsers();
        Task AddSampleUsers(List<CDUser> dbUsers);
        Task<int> InitiateProviderAsync(string authenticateUserId, Profile profile, DocumentDTO cvDocument, DocumentDTO contractDocument);

    }
}
