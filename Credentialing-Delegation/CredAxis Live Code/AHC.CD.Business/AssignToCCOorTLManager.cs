using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business
{
    internal class AssignToCCOorTLManager : IAssignToCCOorTLManager
    {
        private IUnitOfWork uow = null;
        IGenericRepository<ProfileUser> profileUserRepo = null;

        public AssignToCCOorTLManager(IUnitOfWork uow)
        {
            this.uow = uow;
            this.profileUserRepo = uow.GetGenericRepository<ProfileUser>();
        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get the Assigned CCO of a Provider
        /// </summary>
        /// <param name="ProfileID"></param>
        /// <returns></returns>
        public string GetAssignedCCOForaProvider(int ProfileID)
        {
            try
            {
                var ProviderRepository = uow.GetGenericRepository<ProviderUser>();
                var ProfileRepository = uow.GetGenericRepository<ProfileUser>();
                List<ProviderUser> ProviderUsers = new List<ProviderUser>();
                ProviderUsers = (ProviderRepository.Get(p => p.ProfileId == ProfileID, "ProfileUser")).ToList();
                var CCO = ProviderUsers.Find(x => x.ProfileUser.RoleCode == "CCO");
                return (CCO != null) ? (CCO.ProfileUser.Name) : "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get the Assigned TL of a Provider
        /// </summary>
        /// <param name="ProfileID"></param>
        /// <returns></returns>
        public string GetAssignedTLForaProvider(int ProfileID)
        {
            try
            {
                var ProviderRepository = uow.GetGenericRepository<ProviderUser>();
                var ProfileRepository = uow.GetGenericRepository<ProfileUser>();
                List<ProviderUser> ProviderUsers = new List<ProviderUser>();
                ProviderUsers = (ProviderRepository.Get(p => p.ProfileId == ProfileID, "ProfileUser")).ToList();
                var TL = ProviderUsers.Find(x => x.ProfileUser.RoleCode == "TL");
                return (TL != null) ? (TL.ProfileUser.Name) : "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get All the CCO's
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProfileUser>> GetAllCCOs()
        {
            try
            {
                var users = await profileUserRepo.GetAllAsync();
                return users.ToList().Where(p => p.RoleCode == "CCO" && p.Status == "Active");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get All the TL's
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProfileUser>> GetAllTLs()
        {
            try
            {
                var users = await profileUserRepo.GetAllAsync();
                return users.ToList().Where(p => p.RoleCode == "TL" && p.Status == "Active");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to get the Count of Number of Providers Assigned
        /// </summary>
        /// <returns></returns>
        public int NumberOfProvidersAssigned(int ProfileUserID)
        {
            try
            {
                var ProviderUserRepo = uow.GetGenericRepository<ProviderUser>();
                return (ProviderUserRepo.GetAll().Where(p => p.ProfileUser_ProfileUserID == ProfileUserID)).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to get the Count of No of Tasks Assigned to a Provider
        /// </summary>
        /// <param name="ProfileUserID"></param>
        /// <returns></returns>
        public int GetNoofTasksAssigned(int? CDUserID)
        {
            try
            {
                var TaskTrackerRepository = uow.GetGenericRepository<AHC.CD.Entities.TaskTracker.TaskTracker>();
                var TasksAssignedCount = (TaskTrackerRepository.GetAll().Where(t => t.AssignedToId == CDUserID)).Count();
                return TasksAssignedCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to get the NO of Tasks Pending for a CCO
        /// </summary>
        /// <param name="ProfileUserID"></param>
        /// <returns></returns>
        public int GetNoofTasksPending(int? CDUserID)
        {
            var TaskTrackerRepository = uow.GetGenericRepository<AHC.CD.Entities.TaskTracker.TaskTracker>();
            return TaskTrackerRepository.GetAll().Where(t => t.AssignedToId == CDUserID && (t.Status == "OPEN" || t.Status == "REOPEN")).Count();
        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Assign Multiple Profile to a CCo
        /// </summary>
        /// <param name="profileIds"></param>
        /// <param name="profileUserId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task AssignMultipleProfilesToCCo(List<int?> ProfileIDs, int profileUserId, string userId, string Status)
        {
            try
            {
                var CDUserRepo = uow.GetGenericRepository<CDUser>();
                var providerUserRepo = uow.GetGenericRepository<ProviderUser>();

                var provideruser = providerUserRepo.Get(m => (ProfileIDs.Contains(m.ProfileId) && m.ProfileUser.RoleCode == "CCO"), "ProfileUser").ToList();

                var IdsofProvidersAlreadyAssigned = provideruser.Select(m => m.ProfileId).ToList<int?>();
                var IdsofProvidersNotAssigned = ProfileIDs.Except(IdsofProvidersAlreadyAssigned).ToList<int?>();

                var cdUser = CDUserRepo.Find(u => u.AuthenicateUserId == userId);

                var profileUserRepo = uow.GetGenericRepository<ProfileUser>();
                var CCO = profileUserRepo.Find(l => l.ProfileUserID == profileUserId && l.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString(), "ProvidersUser");
                var CDUserID = (profileUserRepo.Find(l => l.ProfileUserID == profileUserId && l.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())).CDUserID;
                ProviderUser provider = new ProviderUser();

                foreach (var providerid in IdsofProvidersNotAssigned)
                {
                    provider = new ProviderUser() { ProfileId = providerid, AssignedByCDUserId = cdUser.CDUserID, AssignedDate = DateTime.Now, LastModifiedDate = DateTime.Now, StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active };
                    CCO.ProvidersUser.Add(provider);
                }
                profileUserRepo.Update(CCO);
                await profileUserRepo.SaveAsync();
                AssignTasksofaProvidertoAssignedCCO(CDUserID, IdsofProvidersNotAssigned);

                if (Status == "true")
                {
                    UpdateCCOorTLForAlreadyAssignedProviders(IdsofProvidersAlreadyAssigned, profileUserId, cdUser, "CCO", CDUserID);
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

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Assign Multiple Profile to a CCo
        /// </summary>
        /// <param name="profileIds"></param>
        /// <param name="profileUserId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task AssignMultipleProfilesToTL(List<int?> ProfileIDs, int profileUserId, string userId, string Status)
        {
            try
            {
                var CDUserRepo = uow.GetGenericRepository<CDUser>();
                var providerUserRepo = uow.GetGenericRepository<ProviderUser>();

                var provideruser = providerUserRepo.Get(m => (ProfileIDs.Contains(m.ProfileId) && m.ProfileUser.RoleCode == "TL"), "ProfileUser").ToList();

                var IdsofProvidersAlreadyAssigned = provideruser.Select(m => m.ProfileId).ToList<int?>();
                var IdsofProvidersNotAssigned = ProfileIDs.Except(IdsofProvidersAlreadyAssigned).ToList<int?>();

                var cdUser = CDUserRepo.Find(u => u.AuthenicateUserId == userId);

                var profileUserRepo = uow.GetGenericRepository<ProfileUser>();
                var TL = profileUserRepo.Find(l => l.ProfileUserID == profileUserId && l.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString(), "ProvidersUser");
                ProviderUser provider = new ProviderUser();

                foreach (var providerid in IdsofProvidersNotAssigned)
                {
                    provider = new ProviderUser() { ProfileId = providerid, AssignedByCDUserId = cdUser.CDUserID, AssignedDate = DateTime.Now, LastModifiedDate = DateTime.Now, StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active };
                    TL.ProvidersUser.Add(provider);
                }
                profileUserRepo.Update(TL);
                await profileUserRepo.SaveAsync();
                GetCountOfAlreadyAssignedProviders(IdsofProvidersNotAssigned, userId);
                if (Status == "true")
                {
                    UpdateCCOorTLForAlreadyAssignedProviders(IdsofProvidersAlreadyAssigned, profileUserId, cdUser, "TL");
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

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Update CCO/TL for Already Existing
        /// </summary>
        /// <param name="IdsofProvidersAlreadyAssigned"></param>
        /// <param name="profileUserId"></param>
        /// <param name="cdUser"></param>
        /// <returns></returns>
        public void UpdateCCOorTLForAlreadyAssignedProviders(List<int?> IdsofProvidersAlreadyAssigned, int profileUserId, CDUser cdUser, string Role, int? CDUserID = 0)
        {
            try
            {
                var providerUserRepo = uow.GetGenericRepository<ProviderUser>();
                ProviderUser Provider;
                foreach (var Id in IdsofProvidersAlreadyAssigned)
                {
                    Provider = providerUserRepo.Find(p => p.ProfileId == Id && p.ProfileUser.RoleCode == Role);
                    Provider.ProfileUser_ProfileUserID = profileUserId;
                    Provider.AssignedByCDUserId = cdUser.CDUserID;
                    Provider.AssignedDate = DateTime.Now;
                    Provider.LastModifiedDate = DateTime.Now;
                    providerUserRepo.Update(Provider);

                }
                providerUserRepo.SaveAsync();
                if(CDUserID!=0)
                    AssignTasksofaProvidertoAssignedCCO(CDUserID, IdsofProvidersAlreadyAssigned);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get the count of Providers Alreadt Assigned
        /// </summary>
        /// <param name="ProfileIDs"></param>
        /// <returns></returns>
        public int GetCountOfAlreadyAssignedProviders(List<int?> ProfileIDs, string CCorTL)
        {
            int count = 0;
            try
            {
                var ProviderUserRepo = uow.GetGenericRepository<ProviderUser>();
                foreach (var id in ProfileIDs)
                {
                    var provider = ProviderUserRepo.Any(p => (p.ProfileId == id) && (p.ProfileUser.RoleCode == CCorTL));
                    if (provider ==true)
                        count++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return count;
        }


        public void AssignTasksofaProvidertoAssignedCCO(int? CDUserID, List<int?> ProfileIDs)
        {            
            var TaskTrackerRepo = uow.GetGenericRepository<AHC.CD.Entities.TaskTracker.TaskTracker>();
            foreach(var id in ProfileIDs)
            {
                var Task = TaskTrackerRepo.GetAll().Where(t => (t.ProfileID == id) && ((t.StatusType == TaskTrackerStatusType.OPEN)||(t.StatusType == TaskTrackerStatusType.REOPEN)));
                foreach(var t in Task)
                {
                    t.AssignedToId = CDUserID;
                    TaskTrackerRepo.Update(t);
                }
                TaskTrackerRepo.Save();
            }
        }
    }
}
