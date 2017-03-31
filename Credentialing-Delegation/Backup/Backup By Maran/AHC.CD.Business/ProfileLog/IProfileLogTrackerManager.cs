using AHC.CD.Entities.ProfileLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.ProfileLog
{
    public interface IProfileLogTrackerManager
    {
        bool SetLockToProfile(ProfileLogTracker log);
        void SetUnlockToProfile(int profileId);
    }
}
