using AHC.CD.Entities.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public interface IProfileDelegationManager
    {
        Task<IEnumerable<ProfileUser>> GetAllTeamLeadsAsync();
        Task AssignProfile(int profileId, int profileUserId, string userId);

    }
}
