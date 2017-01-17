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

                if (!(credentialingInitiationInfoRepo.GetAll().Any(c => c.ProfileID == credentialingInitiationInfo.ProfileID && c.PlanID == credentialingInitiationInfo.PlanID)))
                {
                    var userRepo = uow.GetGenericRepository<CDUser>();
                    var initiator = userRepo.Find(u => u.AuthenicateUserId == userAuthId);
                    CredentialingLog credLog = new CredentialingLog();
                    credentialingInitiationInfo.InitiatedByID = initiator.CDUserID;
                  //  credentialingInitiationInfo.IsDelegated = false;
                    credLog.CredentialingType = CredentialingType.Credentialing;
                    credentialingLogList.Add(credLog);
                    credentialingInitiationInfo.CredentialingLogs = credentialingLogList;

                    List<PlanContract> pc = planContractRepo.GetAll("PlanLOB, PlanLOB.Plan").ToList();
                    //if (pc.Any(p => p.PlanLOB.PlanID == credentialingInitiationInfo.PlanID && p.PlanLOB.Plan.IsDelegated == true))
                    //{
                    //    credentialingInitiationInfo.IsDelegated = true;
                    //}

                    credentialingInitiationInfoRepo.Create(credentialingInitiationInfo);
                    await credentialingInitiationInfoRepo.SaveAsync();
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
                string includeProperties = "Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail";
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var sortData = await credentialingInitiationInfoRepo.GetAsync(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties);
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
                string includeProperties = "Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,LoadedContracts";
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credList = await credentialingInitiationInfoRepo.GetAsync(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties);

                List<CredentialingInfo> decredList = new List<CredentialingInfo>();
                foreach (var d in credList)
                {
                    foreach (var l in d.LoadedContracts)
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



        //public async Task InitiateReCredentialingAsync(int credentialingInfoID, LoadedContract loadedContract)
        //{
        //    var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
        //    //var loadedContractRepo = uow.GetGenericRepository<LoadedContract>();
        //    List<CredentialingLog> credentialingLogList = new List<CredentialingLog>();

        //    try
        //    {
        //        CredentialingInfo credInfo = credentialingInitiationInfoRepo.GetAll("LoadedContracts").Where(x => x.CredentialingInfoID == credentialingInfoID).First();
        //        if (credInfo != null)
        //        {
        //            LoadedContract updateLoadedContract = credInfo.LoadedContracts.Where(x => x.LoadedContractID == loadedContract.LoadedContractID).First();
        //            LoadedContractHistory loadedContractHistory = new LoadedContractHistory();
        //            loadedContractHistory.BusinessEntityID = updateLoadedContract.BusinessEntityID;
        //            loadedContractHistory.CredentialingType = updateLoadedContract.CredentialingType;
        //            loadedContractHistory.CredentialingRequestStatusType = updateLoadedContract.CredentialingRequestStatusType;
        //            loadedContractHistory.LoadedByID = updateLoadedContract.LoadedByID;
        //            loadedContractHistory.LoadedDate = updateLoadedContract.LoadedDate;
        //            loadedContractHistory.LOBID = updateLoadedContract.LOBID;
        //            loadedContractHistory.SpecialtyID = updateLoadedContract.SpecialtyID;
        //            if (updateLoadedContract.LoadedContractHistory == null)
        //            {
        //                updateLoadedContract.LoadedContractHistory = new List<LoadedContractHistory>();
        //            }
        //            updateLoadedContract.LoadedContractHistory.Add(loadedContractHistory);
        //            credentialingInitiationInfoRepo.Update(credInfo);
        //            updateLoadedContract.CredentialingType = AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing;

        //            CredentialingLog credLog = new CredentialingLog();
        //            credLog.CredentialingType = CredentialingType.ReCredentialing;
        //            credentialingLogList.Add(credLog);
        //            credInfo.CredentialingLogs = credentialingLogList;

        //            credentialingInitiationInfoRepo.Update(credInfo);


        //        }
        //        await credentialingInitiationInfoRepo.SaveAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task InitiateDeCredentialingAsync(int credentialingInfoID, LoadedContract loadedContract)
        //{
        //    var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
        //    //var loadedContractRepo = uow.GetGenericRepository<LoadedContract>();

        //    List<CredentialingLog> credentialingLogList = new List<CredentialingLog>();
        //    try
        //    {                
        //        //CredentialingInfo credInfo = credentialingInitiationInfoRepo.GetAll("LoadedContracts").FirstOrDefault(x => x.CredentialingInfoID == credentialingInfoID);
        //        CredentialingInfo credInfo = credentialingInitiationInfoRepo.GetAll("CredentialingLogs").FirstOrDefault(x => x.CredentialingInfoID == credentialingInfoID);

        //        if (credInfo != null)
        //        {
        //            foreach(var a in credInfo.CredentialingLogs)
        //            {
        //                if(a.Credentialing==AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialing.ToString())
        //                {
        //                    throw new AHC.CD.Exceptions.Credentialing.CredentialingException(ExceptionMessage.INITIATE_DECREDENTIALING_EXIST_EXCEPTION);                
        //                }
        //            }
        //            LoadedContract updateLoadedContract = credInfo.LoadedContracts.FirstOrDefault(x => x.LoadedContractID == loadedContract.LoadedContractID);
        //            LoadedContractHistory loadedContractHistory = new LoadedContractHistory();                   

        //            loadedContractHistory.BusinessEntityID = updateLoadedContract.BusinessEntityID;
        //            loadedContractHistory.CredentialingType = updateLoadedContract.CredentialingType;
        //            loadedContractHistory.CredentialingRequestStatusType = updateLoadedContract.CredentialingRequestStatusType;
        //            loadedContractHistory.LoadedByID = updateLoadedContract.LoadedByID;
        //            loadedContractHistory.LoadedDate = updateLoadedContract.LoadedDate;
        //            loadedContractHistory.LOBID = updateLoadedContract.LOBID;
        //            loadedContractHistory.SpecialtyID = updateLoadedContract.SpecialtyID;
        //            if (updateLoadedContract.LoadedContractHistory == null)
        //            {
        //                updateLoadedContract.LoadedContractHistory = new List<LoadedContractHistory>();
        //            }
        //            updateLoadedContract.LoadedContractHistory.Add(loadedContractHistory);
        //            credentialingInitiationInfoRepo.Update(credInfo);
        //            updateLoadedContract.CredentialingType = AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialing;
        //            updateLoadedContract.CredentialingRequestStatusType = AHC.CD.Entities.MasterData.Enums.CredentialingRequestStatusType.Pending;                    

        //            CredentialingLog credLog = new CredentialingLog();
        //            credLog.CredentialingType = CredentialingType.DeCredentialing;
        //            credentialingLogList.Add(credLog);
        //            credInfo.CredentialingLogs = credentialingLogList;


        //            credentialingInitiationInfoRepo.Update(credInfo);
        //        }

        //        await credentialingInitiationInfoRepo.SaveAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


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



        public async Task InitiateReCredentialingAsync(int credentialingInfoID, CredentialingLog CredentialingLog)
        {
            var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
            //var loadedContractRepo = uow.GetGenericRepository<LoadedContract>();
            //List<CredentialingLog> credentialingLogList = new List<CredentialingLog>();

            try
            {
                CredentialingInfo credInfo = credentialingInitiationInfoRepo.GetAll("CredentialingLogs").Where(x => x.CredentialingInfoID == credentialingInfoID).First();
                if (credInfo != null)
                {
                    CredentialingLog credLog = new CredentialingLog();
                    credLog.CredentialingType = CredentialingType.ReCredentialing;
                    credInfo.CredentialingLogs.Add(credLog);

                    credentialingInitiationInfoRepo.Update(credInfo);

                }
                await credentialingInitiationInfoRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InitiateDeCredentialingAsync(int credentialingInfoID, CredentialingLog CredentialingLog)
        {
            var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
            //var loadedContractRepo = uow.GetGenericRepository<LoadedContract>();
            //List<CredentialingLog> credentialingLogList = new List<CredentialingLog>();

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

                    CredentialingLog credLog = new CredentialingLog();
                    credLog.CredentialingType = CredentialingType.DeCredentialing;
                    credInfo.CredentialingLogs.Add(credLog);

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
