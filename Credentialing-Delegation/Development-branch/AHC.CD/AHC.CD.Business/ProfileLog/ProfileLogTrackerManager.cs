using AHC.CD.Data.Repository;
using AHC.CD.Entities.ProfileLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.ProfileLog
{
    internal class ProfileLogTrackerManager : IProfileLogTrackerManager
    {
        private IUnitOfWork uow = null;

        public ProfileLogTrackerManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public bool SetLockToProfile(Entities.ProfileLog.ProfileLogTracker log)
        {
            try
            {
                bool showProfile = false;

                var logRepo = uow.GetGenericRepository<ProfileLogTracker>();

                if (logRepo.Any(p => p.ProfileId == log.ProfileId))
                {
                    var tracker = logRepo.Find(p => p.ProfileId == log.ProfileId);

                    if (tracker.IsLocked == AHC.CD.Entities.MasterData.Enums.YesNoOption.NO.ToString())
                    {
                        tracker.IsLockedYesNoOption = AHC.CD.Entities.MasterData.Enums.YesNoOption.YES;

                        return showProfile;
                    }
                    else
                    {
                        return showProfile = false;
                    }
                }
                else
                {
                    log.IsLockedYesNoOption = AHC.CD.Entities.MasterData.Enums.YesNoOption.YES;
                    logRepo.Create(log);
                    logRepo.Save();

                    return showProfile = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public void SetUnlockToProfile(int profileId)
        {
            try
            {
                var logRepo = uow.GetGenericRepository<ProfileLogTracker>();

                var tracker = logRepo.Find(p => p.ProfileId == profileId);

                tracker.IsLockedYesNoOption = AHC.CD.Entities.MasterData.Enums.YesNoOption.NO;

                logRepo.Update(tracker);

                logRepo.Save();

            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
