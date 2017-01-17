using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Resources.Messages;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.MasterData.Enums;


namespace AHC.CD.Business.Credentialing
{
    internal class IndividualCredentialingManager : IIndividualCredentialingManager
    {
        private IUnitOfWork uow = null;

        public IndividualCredentialingManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary>
        /// To initiate credentialing
        /// </summary>
        /// <param name="credentialingInitiationInfo"></param>
        /// <returns></returns>
        public async Task InitiateCredentialingAsync(CredentialingInfo credentialingInitiationInfo, string userAuthId)
        {
            try
            {
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var planContractRepo = uow.GetGenericRepository<PlanContract>();
                List<CredentialingLog> credentialingLogList = new List<CredentialingLog>();
                List<CredentialingActivityLog> credActivityLogList = new List<CredentialingActivityLog>();

                if (!(credentialingInitiationInfoRepo.GetAll().Any(c => c.ProfileID == credentialingInitiationInfo.ProfileID && c.PlanID == credentialingInitiationInfo.PlanID)))
                {
                    var userRepo = uow.GetGenericRepository<CDUser>();
                    var initiator = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                    CredentialingLog credLog = new CredentialingLog();
                    credentialingInitiationInfo.InitiatedByID = initiator.CDUserID;
                    credLog.CredentialingType = CredentialingType.Credentialing;

                    CredentialingActivityLog credActivityLog = new CredentialingActivityLog();
                    credActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                    credActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Initiation;
                    credActivityLogList.Add(credActivityLog);
                    credLog.CredentialingActivityLogs = credActivityLogList; 
                    credentialingLogList.Add(credLog);
                    credentialingInitiationInfo.CredentialingLogs = credentialingLogList;

                    credentialingInitiationInfoRepo.Create(credentialingInitiationInfo);
                    await credentialingInitiationInfoRepo.SaveAsync();

                    var planRepo = uow.GetGenericRepository<Plan>();

                    Plan updatePlan = planRepo.Find(p => p.PlanID == credentialingInitiationInfo.PlanID);
                    updatePlan.IsDelegated = (Entities.MasterData.Enums.IsDelegated)credentialingInitiationInfo.IsDelegatedYesNoOption;
                    planRepo.Update(updatePlan);
                    planRepo.Save();
                }
                else
                {
                    throw new AHC.CD.Exceptions.Credentialing.CredentialingException(ExceptionMessage.PLAN_PROVIDER_CREDENTIALING_EXISTS);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CredentialingInfo>> GetAllCredentialingsAsync()
        {
            try
            {
                string includeProperties = "Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail, CredentialingLogs.CredentialingActivityLogs";
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var sortData = await credentialingInitiationInfoRepo.GetAsync(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties);
                foreach (var item in sortData)
                {
                    item.CredentialingContractRequests = null;
                    item.CredentialingVerificationInfos = null;
                }
                return sortData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CredentialingInfo>> GetAllDeCredentialingAsync()
        {
            try
            {
                string includeProperties = "Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,LoadedContracts,CredentialingLogs";
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credList = await credentialingInitiationInfoRepo.GetAsync(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties);

                List<CredentialingInfo> decredList = new List<CredentialingInfo>();
                foreach (var d in credList)
                {
                    foreach (var l in d.CredentialingLogs)
                    {
                        if (l.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialing.ToString())
                        {
                            decredList.Add(d);
                        }
                    }
                }
                return decredList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CredentialingInfo>> GetAllReCredentialingAsync()
        {
            try
            {
                string includeProperties = "Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,LoadedContracts,CredentialingLogs";
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credList = await credentialingInitiationInfoRepo.GetAsync(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties);

                List<CredentialingInfo> reCredList = new List<CredentialingInfo>();
                foreach (var d in credList)
                {
                    foreach (var l in d.CredentialingLogs)
                    {
                        if (l.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString())
                        {
                            reCredList.Add(d);
                        }
                    }
                }
                return reCredList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddLoadedContractHistory(LoadedContractHistory loadedContractHistory)
        {
            try
            {
                var loadedContractHistoryRepo = uow.GetGenericRepository<LoadedContractHistory>();
                loadedContractHistoryRepo.Create(loadedContractHistory);
                await loadedContractHistoryRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task InitiateReCredentialingAsync(int credentialingInfoID,string userAuthId, CredentialingLog CredentialingLog)
        {
            var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
            //var planContractRepo = uow.GetGenericRepository<PlanContract>();
            //var loadedContractRepo = uow.GetGenericRepository<LoadedContract>();  
            List<CredentialingActivityLog> credActivityLogList = new List<CredentialingActivityLog>();

            try
            {
                CredentialingInfo credInfo = credentialingInitiationInfoRepo.GetAll("CredentialingLogs").Where(x => x.CredentialingInfoID == credentialingInfoID).First();
                //List<PlanContract> pc = planContractRepo.GetAll("PlanLOB, PlanLOB.Plan").ToList();

                if (credInfo != null)
                {
                    foreach (var a in credInfo.CredentialingLogs)
                    {
                        if (a.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialing.ToString())
                        {
                            throw new AHC.CD.Exceptions.Credentialing.CredentialingException(ExceptionMessage.INITIATE_DECREDENTIALING_EXIST_EXCEPTION);
                        }
                    }

                    var userRepo = uow.GetGenericRepository<CDUser>();
                    var initiator = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                    CredentialingLog credLog = new CredentialingLog();
                    credLog.CredentialingType = CredentialingType.ReCredentialing;

                    CredentialingActivityLog credActivityLog = new CredentialingActivityLog();
                    credActivityLog.ActivityByID = initiator.CDUserID;
                    credActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                    credActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Initiation;

                    credActivityLogList.Add(credActivityLog);
                    credLog.CredentialingActivityLogs = credActivityLogList; 

                    credLog.CredentialingActivityLogs.Add(credActivityLog);                    
                    credInfo.CredentialingLogs.Add(credLog);

                    credInfo.LastModifiedDate =(System.DateTime)credLog.LastModifiedDate;



                    credentialingInitiationInfoRepo.Update(credInfo);

                }
                await credentialingInitiationInfoRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InitiateDeCredentialingAsync(int credentialingInfoID,string userAuthId, CredentialingLog CredentialingLog)
        {
            var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
            //var loadedContractRepo = uow.GetGenericRepository<LoadedContract>();
            //List<CredentialingLog> credentialingLogList = new List<CredentialingLog>();
            List<CredentialingActivityLog> credActivityLogList = new List<CredentialingActivityLog>();

            try
            {
                CredentialingInfo credInfo = credentialingInitiationInfoRepo.GetAll("CredentialingLogs").Where(x => x.CredentialingInfoID == credentialingInfoID).First();
                if (credInfo != null)
                {
                    foreach (var a in credInfo.CredentialingLogs)
                    {
                        if (a.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialing.ToString())
                        {
                            throw new AHC.CD.Exceptions.Credentialing.CredentialingException(ExceptionMessage.INITIATE_DECREDENTIALING_EXIST_EXCEPTION);
                        }
                    }

                    var userRepo = uow.GetGenericRepository<CDUser>();
                    var initiator = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                    CredentialingLog credLog = new CredentialingLog();
                    credLog.CredentialingType = CredentialingType.DeCredentialing;               

                    CredentialingActivityLog credActivityLog = new CredentialingActivityLog();
                    credActivityLog.ActivityByID = initiator.CDUserID;
                    credActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                    credActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Initiation;

                    credActivityLogList.Add(credActivityLog);
                    credLog.CredentialingActivityLogs = credActivityLogList;

                    credLog.CredentialingActivityLogs.Add(credActivityLog);
                    credInfo.CredentialingLogs.Add(credLog);

                    credInfo.LastModifiedDate = (System.DateTime)credLog.LastModifiedDate;

                    credentialingInitiationInfoRepo.Update(credInfo);
                }
                await credentialingInitiationInfoRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
