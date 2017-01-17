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
        public void InitiateCredentialingAsync(CredentialingInfo credentialingInitiationInfo, string userAuthId)
        {
            try
            {
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var planContractRepo = uow.GetGenericRepository<PlanContract>();
                List<CredentialingLog> credentialingLogList = new List<CredentialingLog>();
                List<CredentialingActivityLog> credActivityLogList = new List<CredentialingActivityLog>();

                //if (!(credentialingInitiationInfoRepo.GetAll().Any(c => c.ProfileID == credentialingInitiationInfo.ProfileID && c.PlanID == credentialingInitiationInfo.PlanID)))
                //{
                var userRepo = uow.GetGenericRepository<CDUser>();
                var initiator = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                CredentialingLog credLog = new CredentialingLog();
                credentialingInitiationInfo.InitiatedByID = initiator.CDUserID;
                credLog.CredentialingType = CredentialingType.Credentialing;

                CredentialingActivityLog credActivityLog = new CredentialingActivityLog();
                credActivityLog.ActivityByID = initiator.CDUserID;
                credActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                credActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Initiation;
                credActivityLogList.Add(credActivityLog);
                credLog.CredentialingActivityLogs = credActivityLogList;
                credentialingLogList.Add(credLog);
                credentialingInitiationInfo.CredentialingLogs = credentialingLogList;
                credentialingInitiationInfoRepo.Create(credentialingInitiationInfo);
                credentialingInitiationInfoRepo.Save();

                var planRepo = uow.GetGenericRepository<Plan>();

                Plan updatePlan = planRepo.Find(p => p.PlanID == credentialingInitiationInfo.PlanID);
                updatePlan.IsDelegated = (Entities.MasterData.Enums.IsDelegated)credentialingInitiationInfo.IsDelegatedYesNoOption;
                planRepo.Update(updatePlan);
                planRepo.Save();
                //}
                //else
                //{
                //    throw new AHC.CD.Exceptions.Credentialing.CredentialingException(ExceptionMessage.PLAN_PROVIDER_CREDENTIALING_EXISTS);
                //}
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
                List<CredentialingInfo> credList = new List<CredentialingInfo>();
                string includeProperties = "Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail, CredentialingLogs.CredentialingActivityLogs";
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                //var sortData = await credentialingInitiationInfoRepo.GetAsync(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties);
                var sortData = await credentialingInitiationInfoRepo.GetAsync(s => s.Status == Entities.MasterData.Enums.StatusType.Active.ToString() && s.Profile.Status == Entities.MasterData.Enums.StatusType.Active.ToString(), includeProperties);
                foreach (var item in sortData)
                {
                    item.CredentialingContractRequests = null;
                    item.CredentialingVerificationInfos = null;
                    if (item.CredentialingLogs.Any(s => s.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString()) && !(item.CredentialingLogs.Any(s => s.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialing.ToString())))
                    {
                        foreach(var data in item.CredentialingLogs)
                        {
                            if(data.Credentialing==AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString())
                            {
                                data.CredentialingActivityLogs = data.CredentialingActivityLogs.OrderBy(y => y.LastModifiedDate).ToList();
                                if(!(data.CredentialingActivityLogs.Any(x=>x.Activity==AHC.CD.Entities.MasterData.Enums.ActivityType.Closure.ToString() || x.Activity==AHC.CD.Entities.MasterData.Enums.ActivityType.Report.ToString() && x.ActivityStatus==AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed.ToString()))){
                                    credList.Add(item);
                                    break;
                                }
                            }
                        }
                    }
                }

                return credList;
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
                string includeProperties = "Plan, Profile,Profile.PersonalDetail,Profile.PersonalDetail.ProviderTitles.ProviderType, Profile.OtherIdentificationNumber, CredentialingContractRequests, Profile.PersonalDetail,CredentialingLogs,CredentialingLogs.CredentialingActivityLogs, CredentialingContractRequests.ContractGrid.LOB, CredentialingContractRequests.ContractGrid.ProfilePracticeLocation.Facility, CredentialingContractRequests.ContractGrid.ProfileSpecialty.Specialty, CredentialingContractRequests.ContractGrid.Report, CredentialingContractRequests.ContractGrid.BusinessEntity";
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credList1 = await credentialingInitiationInfoRepo.GetAsync(s => (s.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() || s.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()) && s.Profile.Status == Entities.MasterData.Enums.StatusType.Active.ToString(), includeProperties);

                List<CredentialingInfo> credList2 = new List<CredentialingInfo>();
                foreach (var cL in credList1)
                {
                    if (cL.CredentialingLogs.Any(x=>x.Credentialing==AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                    {
                        credList2.Add(cL);
                    }
                }
                foreach (var d in credList2)
                {
                    foreach (var ContractObj in d.CredentialingContractRequests)
                    {
                        foreach (var GridObj in ContractObj.ContractGrid)
                        {
                            GridObj.CredentialingInfo = null;
                        }
                    }
                }
                return credList2;
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
                string includeProperties = "Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,CredentialingLogs,CredentialingLogs.CredentialingActivityLogs,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists,CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentResult";
                var reCredentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var reCredentialingList = await reCredentialingInitiationInfoRepo.GetAsync(s => (s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() || s.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()) && s.Profile.Status == Entities.MasterData.Enums.StatusType.Active.ToString(), includeProperties);

                List<CredentialingInfo> reCredList = new List<CredentialingInfo>();
                foreach (var item in reCredentialingList)
                {

                    if (item.CredentialingLogs.Any(s => s.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) && !(item.CredentialingLogs.Any(s => s.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialing.ToString())))
                    {
                        foreach(var s in item.CredentialingLogs)
                        {
                            s.CredentialingActivityLogs = s.CredentialingActivityLogs.OrderBy(y => y.LastModifiedDate).ToList();
                            if (!(s.CredentialingActivityLogs.Any(x => x.Activity == AHC.CD.Entities.MasterData.Enums.ActivityType.Closure.ToString() || x.Activity == AHC.CD.Entities.MasterData.Enums.ActivityType.Report.ToString() && x.ActivityStatus == AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed.ToString())))
                            {
                                reCredList.Add(item);
                            }
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



        public int InitiateReCredentialingAsync(int credentialingInfoID, CredentialingInfo credentialingInitiationInfo, string userAuthId, int[] CredentialingContractRequestsArray)
        {

            try
            {
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credentialingInitiationConReqRepo = uow.GetGenericRepository<CredentialingContractRequest>();
                var PlanRepo = uow.GetGenericRepository<Plan>();
                Plan plan = PlanRepo.GetAll().Where(x => x.PlanID == credentialingInitiationInfo.PlanID).First();
                CredentialingInfo credInfo = credentialingInitiationInfoRepo.GetAll("CredentialingLogs").Where(x => x.CredentialingInfoID == credentialingInfoID).First();
                var credentialingInitiationConReqList1 = credentialingInitiationConReqRepo.GetAll("ContractSpecialties,ContractPracticeLocations,ContractLOBs,ContractGrid,ContractGrid.Report").ToList();
                List<CredentialingContractRequest> credentialingInitiationConReqList = new List<CredentialingContractRequest>();
                //if (credInfo != null)
                //{
                //    foreach (var a in credInfo.CredentialingLogs)
                //    {
                //        if (a.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialing.ToString())
                //        {
                //            throw new AHC.CD.Exceptions.Credentialing.CredentialingException(ExceptionMessage.INITIATE_DECREDENTIALING_EXIST_EXCEPTION);
                //        }
                //    }
                //}
                if (CredentialingContractRequestsArray != null)
                {
                    for (var i = 0; i < CredentialingContractRequestsArray.Length; i++)
                    {
                        foreach (var data in credentialingInitiationConReqList1)
                        {
                            if (data.CredentialingContractRequestID == CredentialingContractRequestsArray[i])
                            {
                                
                                data.ContractRequestStatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                                credentialingInitiationConReqRepo.Update(data);
                                credentialingInitiationConReqRepo.Save();
                                List<ContractGrid> ContractGridCondition1 = new List<ContractGrid>();
                                for (int cg = 0; cg < data.ContractGrid.Count; cg++)
                                {
                                    if (data.ContractGrid.ElementAt(cg).Status == Entities.MasterData.Enums.StatusType.Inactive.ToString())
                                    {
                                        if (data.ContractGrid.FirstOrDefault(c => c.ContractGridID == data.ContractGrid.ElementAt(cg).ContractGridID) != null)
                                        {
                                            ContractGridCondition1.Add(data.ContractGrid.ElementAt(cg));
                                        }
                                    }
                                }
                                for (int cg = 0; cg < ContractGridCondition1.Count; cg++)
                                {
                                    for (int cg1 = 0; cg1 < data.ContractGrid.Count; cg1++)
                                    {
                                        if (ContractGridCondition1.ElementAt(cg).ContractGridID == data.ContractGrid.ElementAt(cg1).ContractGridID)
                                        {
                                            data.ContractGrid.Remove(data.ContractGrid.ElementAt(cg1));
                                            break;
                                        }
                                    }
                                }
                                List<ContractGrid> ContractGridCondition2 = new List<ContractGrid>();
                                for (int cg = 0; cg < data.ContractGrid.Count; cg++)
                                {
                                    if (data.ContractGrid.ElementAt(cg).Report != null)
                                    {
                                        if (data.ContractGrid.ElementAt(cg).Report.CredentialingApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Rejected.ToString())
                                        {
                                            ContractGridCondition2.Add(data.ContractGrid.ElementAt(cg));
                                        }
                                    }
                                    else
                                    {
                                        ContractGridCondition2.Add(data.ContractGrid.ElementAt(cg));
                                    }
                                }
                                for (int cg = 0; cg < ContractGridCondition2.Count; cg++)
                                {
                                    for (int cg1 = 0; cg1 < data.ContractGrid.Count; cg1++)
                                    {
                                        if (ContractGridCondition2.ElementAt(cg).ContractGridID == data.ContractGrid.ElementAt(cg1).ContractGridID)
                                        {
                                            data.ContractGrid.Remove(data.ContractGrid.ElementAt(cg1));
                                            break;
                                        }
                                    }
                                }
                                List<ContractGrid> ContractGridCondition3 = new List<ContractGrid>();
                                for (int cg = 0; cg < data.ContractGrid.Count; cg++)
                                {
                                    if (data.ContractGrid.ElementAt(cg).Report != null)
                                    {
                                        if (data.ContractGrid.ElementAt(cg).Report.CredentialingApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Rejected.ToString())
                                        {
                                            ContractGridCondition3.Add(data.ContractGrid.ElementAt(cg));
                                        }
                                    }
                                    else
                                    {
                                        ContractGridCondition3.Add(data.ContractGrid.ElementAt(cg));
                                    }
                                }
                                for (int cg = 0; cg < ContractGridCondition3.Count; cg++)
                                {
                                    for (int cg1 = 0; cg1 < data.ContractGrid.Count; cg1++)
                                    {
                                        if (ContractGridCondition3.ElementAt(cg).ContractGridID == data.ContractGrid.ElementAt(cg1).ContractGridID)
                                        {
                                            data.ContractGrid.Remove(data.ContractGrid.ElementAt(cg1));
                                            break;
                                        }
                                    }
                                }
                                List<ContractGrid> ContractGridCondition4 = new List<ContractGrid>();
                                for (int cg = 0; cg < data.ContractGrid.Count; cg++)
                                {
                                    if (data.ContractGrid.ElementAt(cg).Report != null)
                                    {

                                        if (data.ContractGrid.ElementAt(cg).Report.TerminationDate <= DateTime.Today)
                                        {
                                            ContractGridCondition4.Add(data.ContractGrid.ElementAt(cg));
                                        }
                                    }
                                    else
                                    {
                                        ContractGridCondition4.Add(data.ContractGrid.ElementAt(cg));
                                    }
                                }
                                for (int cg = 0; cg < ContractGridCondition4.Count; cg++)
                                {
                                    for (int cg1 = 0; cg1 < data.ContractGrid.Count; cg1++)
                                    {
                                        if (ContractGridCondition4.ElementAt(cg).ContractGridID == data.ContractGrid.ElementAt(cg1).ContractGridID)
                                        {
                                            data.ContractGrid.Remove(data.ContractGrid.ElementAt(cg1));
                                            break;
                                        }
                                    }
                                }
                                if (data.ContractGrid.Count == 0)
                                {
                                    continue;
                                }
                                else
                                {

                                    //data.CredentialingRequestIDReference = data.CredentialingContractRequestID;
                                    credentialingInitiationConReqList.Add(data);
                                }
                            }
                        }
                    }

                }
                //if (NonCredentialingContractRequestsArray != null)
                //{
                //    for (var i = 0; i < NonCredentialingContractRequestsArray.Length; i++)
                //    {
                //        foreach (var data in credentialingInitiationConReqList1)
                //        {
                //            if (data.CredentialingContractRequestID == NonCredentialingContractRequestsArray[i])
                //            {
                //                data.ContractRequestStatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                //                credentialingInitiationConReqRepo.Update(data);
                //            }
                //        }
                //    }
                //    credentialingInitiationConReqRepo.Save();
                //}
                //credInfo.CredentialingContractRequests = new List<CredentialingContractRequest>();
                List<CredentialingLog> credentialingLogList = new List<CredentialingLog>();
                List<CredentialingActivityLog> credActivityLogList = new List<CredentialingActivityLog>();
                var userRepo = uow.GetGenericRepository<CDUser>();
                var initiator = userRepo.Find(u => u.AuthenicateUserId == userAuthId);
                CredentialingLog credLog = new CredentialingLog();
                credentialingInitiationInfo.InitiatedByID = initiator.CDUserID;
                credLog.CredentialingType = CredentialingType.ReCredentialing;

                CredentialingActivityLog credActivityLog = new CredentialingActivityLog();
                credActivityLog.ActivityByID = initiator.CDUserID;
                credActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                credActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Initiation;
                credActivityLogList.Add(credActivityLog);
                credentialingInitiationInfo.IsDelegatedYesNoOption = plan.IsDelegated == AHC.CD.Entities.MasterData.Enums.IsDelegated.YES ? AHC.CD.Entities.MasterData.Enums.YesNoOption.YES : AHC.CD.Entities.MasterData.Enums.YesNoOption.NO;
                if (credentialingInitiationInfo.IsDelegated == Entities.MasterData.Enums.YesNoOption.NO.ToString())
                {
                    credActivityLog.ActivityByID = initiator.CDUserID;
                    credActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                    credActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Loading;
                    credActivityLogList.Add(credActivityLog);
                }

                credLog.CredentialingActivityLogs = credActivityLogList;
                credentialingLogList.Add(credLog);
                credentialingInitiationInfo.CredentialingLogs = credentialingLogList;
                foreach (var data1 in credentialingInitiationConReqList)
                {
                    data1.ContractRequestStatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    data1.CredentialingRequestIDReference = data1.CredentialingContractRequestID;
                    foreach (var data2 in data1.ContractGrid)
                    {
                        data2.Report = new CredentialingContractInfoFromPlan();
                        data2.ContractGridID = 0;
                        data2.CredentialingInfo = null;
                        data2.CredentialingInfoID = null;
                        data2.ContractGridStatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    }
                }

                credentialingInitiationInfo.CredentialingContractRequests = credentialingInitiationConReqList;
                credentialingInitiationInfoRepo.Create(credentialingInitiationInfo);
                credentialingInitiationInfoRepo.Save();
                CredentialingInfo credentialingInfo = credentialingInitiationInfoRepo.Find(x => x.CredentialingInfoID == credentialingInitiationInfo.CredentialingInfoID, "CredentialingContractRequests,CredentialingContractRequests.ContractGrid");
                foreach (var data1 in credentialingInfo.CredentialingContractRequests)
                {
                    foreach (var data2 in data1.ContractGrid)
                    {
                        if (credentialingInfo.IsDelegated == Entities.MasterData.Enums.YesNoOption.NO.ToString())
                        {
                            data2.InitialCredentialingDate = null;
                            if (data2.Report != null) {
                                data2.Report.ParticipatingStatus = "RECREDENTIALING IN PROCESS";
                            }
                        }
                        data2.CredentialingInfoID = credentialingInfo.CredentialingInfoID;
                    }
                }
                credentialingInitiationInfoRepo.Update(credentialingInfo);
                credentialingInitiationInfoRepo.Save();
                return credentialingInitiationInfo.CredentialingInfoID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InitiateDeCredentialingAsync(int credentialingInfoID, int contractRequestId, int gridId, string userAuthId)
        {
            var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
            string includeProperties = "CredentialingLogs,CredentialingContractRequests.ContractGrid.Report,Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,CredentialingLogs.CredentialingActivityLogs,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists,CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentResult,CredentialingLogs.CredentialingVerificationInfo";
            try
            {
                CredentialingInfo credInfo = credentialingInitiationInfoRepo.Find(x => x.CredentialingInfoID == credentialingInfoID,includeProperties);
                if (credInfo != null)
                {
                    foreach (var b in credInfo.CredentialingContractRequests)
                    {
                        if (b.CredentialingContractRequestID == contractRequestId)
                        {
                            foreach (var c in b.ContractGrid)
                            {
                                if (c.ContractGridID == gridId)
                                {
                                    if (c.Report == null)
                                    {
                                        c.Report = new CredentialingContractInfoFromPlan();
                                    }
                                    c.Report.TerminationDate = DateTime.Now;
                                    c.Report.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                                    c.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                                }

                            }

                        }
                    }
                    var userRepo = uow.GetGenericRepository<CDUser>();
                    var initiator = userRepo.Find(u => u.AuthenicateUserId == userAuthId);
                    if (!(credInfo.CredentialingLogs.Any(x=>x.Credentialing==AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString())))
                    {
                        List<CredentialingActivityLog> credActivityLogList = new List<CredentialingActivityLog>();
                        CredentialingLog credLog = new CredentialingLog();
                        credLog.CredentialingType = CredentialingType.DeCredentialingInitiated;
                        credInfo.CredentialingLogs.Add(credLog);
                        credInfo.LastModifiedDate = (System.DateTime)credLog.LastModifiedDate;
                    }
                    credentialingInitiationInfoRepo.Update(credInfo);
                }
                credentialingInitiationInfoRepo.Save();
                return credInfo.CredentialingInfoID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CompleteDeCredentialingAsync(int[] credentialingInfoIDs, string userAuthId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var initiator = userRepo.Find(u => u.AuthenicateUserId == userAuthId);
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                string includeProperties = "CredentialingLogs,CredentialingContractRequests.ContractGrid.Report,Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,CredentialingLogs.CredentialingActivityLogs,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists,CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentResult,CredentialingLogs.CredentialingVerificationInfo";
                foreach (var CredID in credentialingInfoIDs)
                {
                    CredentialingInfo credInfo = credentialingInitiationInfoRepo.Find(x => x.CredentialingInfoID == CredID, includeProperties);
                    CredentialingLog credentialingLog = credInfo.CredentialingLogs.ToList().Last();
                    List<ContractGrid> ContractGrids = new List<ContractGrid>();
                    foreach (var item in credInfo.CredentialingContractRequests)
                    {
                        foreach (var grid in item.ContractGrid)
                        {
                            ContractGrids.Add(grid);
                        }
                    }
                    int flag = 0;
                    foreach (var item in ContractGrids)
                    {
                        if (item.StatusType == AHC.CD.Entities.MasterData.Enums.StatusType.Active)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        List<CredentialingActivityLog> credActivityLogList = new List<CredentialingActivityLog>();
                        CredentialingLog credLog = new CredentialingLog();
                        credLog.CredentialingType = CredentialingType.DeCredentialing;
                        credInfo.CredentialingLogs.Add(credLog);
                        credInfo.LastModifiedDate = (System.DateTime)credLog.LastModifiedDate;
                        credentialingInitiationInfoRepo.Update(credInfo);
                        credentialingInitiationInfoRepo.Save(); 
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Object>> getAllCredentialinginfoByContractRequest(int ProviderID, int PlanID)
        {
            try
            {
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                string IncludeProperties = "CredentialingLogs,CredentialingLogs.CredentialingActivityLogs,CredentialingContractRequests.ContractGrid.Report,CredentialingContractRequests.ContractGrid,CredentialingContractRequests.ContractPracticeLocations,CredentialingContractRequests,CredentialingContractRequests.ContractLOBs,CredentialingContractRequests.ContractSpecialties, CredentialingContractRequests.ContractGrid.Report, CredentialingContractRequests.ContractGrid.ProfileSpecialty.Specialty, CredentialingContractRequests.ContractGrid.ProfilePracticeLocation.Facility, CredentialingContractRequests.ContractGrid.BusinessEntity, CredentialingContractRequests.ContractGrid.LOB";
                var credentialingInfo = await credentialingInitiationInfoRepo.GetAllAsync(IncludeProperties);
                List<CredentialingInfo> credentialingInfo1 = new List<CredentialingInfo>();
                List<Object> CredentialingContractRequestsReference = new List<Object>();


                //foreach (var data in credentialingInfo)
                //{
                //    int flag = 0;
                //    foreach(var data1 in data.CredentialingLogs)
                //    {
                //        if (data1.Credentialing==AHC.CD.Entities.MasterData.Enums.CredentialingType.Dropped.ToString())
                //        {
                //            flag = 1;
                //            break;
                //        }
                //    }
                //    if (flag==0)
                //    {
                //        credentialingInfo2.Add(data);
                //    }
                //}



                foreach (var data in credentialingInfo)
                {
                    if (data.ProfileID == ProviderID && data.PlanID == PlanID)
                    {
                        foreach (var data1 in data.CredentialingContractRequests)
                        {
                            foreach (var data2 in data1.ContractGrid)
                            {
                                data2.CredentialingInfo = null;
                            }
                        }
                        credentialingInfo1.Add(data);
                    }
                }

                foreach (var data1 in credentialingInfo1)
                {
                    foreach (var data2 in data1.CredentialingContractRequests)
                    {
                        if (data2.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString() && data2.ContractRequestStatus == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                        {
                            var ContractGrids1 = new List<ContractGrid>();
                            //bool ContractGridTableStatus = false;
                            foreach (var data3 in data2.ContractGrid)
                            {
                                if (data3.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString())
                                {
                                    ContractGrids1.Add(data3);
                                }

                            }
                            foreach (var tempData in ContractGrids1)
                            {
                                data2.ContractGrid.Remove(tempData);
                            }
                            int counter = 0;
                            int gridLength = data2.ContractGrid.Count;
                            if (gridLength > 0)
                            {
                                foreach (var data4 in data2.ContractGrid)
                                {
                                    if (data4.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                                    {
                                        if (data4.Report != null)
                                        {
                                            if (data4.Report.AdminFee == null && data4.Report.CAP == null && data4.Report.ContractDocumentPath == null && data4.Report.CredentialedDate == null && data4.Report.CredentialingApprovalStatus == null && data4.Report.GroupID == null && data4.Report.InitiatedDate == null && data4.Report.PanelStatus == null && data4.Report.PercentageOfRisk == null && data4.Report.ProviderID == null && data4.Report.ReCredentialingDate == null && data4.Report.Status == null && data4.Report.StopLossFee == null && data4.Report.TerminationDate == null && data4.Report.WelcomeLetterPath == null)
                                            {
                                                counter++;
                                            }
                                        }
                                    }
                                }
                            }
                            int counter1 = 0;
                            int gridLength1 = data2.ContractGrid.Count;
                            if (gridLength1 > 0)
                            {
                                foreach (var data5 in data2.ContractGrid)
                                {
                                    if (data5.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                                    {
                                        if (data5.Report != null)
                                        {
                                            if (data5.Report.CredentialingApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Rejected.ToString())
                                            {
                                                counter1++;
                                            }
                                        }
                                    }
                                }
                            }

                            int counter2 = 0;
                            int gridLength2 = data2.ContractGrid.Count;
                            if (gridLength2 > 0)
                            {
                                foreach (var data6 in data2.ContractGrid)
                                {
                                    if (data6.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                                    {
                                        if (data6.Report != null)
                                        {
                                            if (data6.Report.TerminationDate != null && data6.Report.TerminationDate < DateTime.Now)
                                            {
                                                counter2++;
                                            }
                                        }
                                    }
                                }
                            }
                            if (gridLength == counter && gridLength > 0)
                            {
                                //ContractGridTableStatus = true;
                                var tmp = new
                                {
                                    TableRowStatus = false,
                                    credID = data1.CredentialingInfoID,
                                    CredentialingContractRequests = data2,
                                };
                                CredentialingContractRequestsReference.Add(tmp);
                            }
                            else if (gridLength1 == counter1 && gridLength1 > 0)
                            {
                                continue;
                            }
                            else if (gridLength2 == counter2 && gridLength2 > 0)
                            {
                                continue;
                            }
                            else
                            {
                                int ContractGridCount = data2.ContractGrid.Count;
                                if (ContractGridCount > 0)
                                {
                                    int NullStatus = 0;
                                    int ApprovStatus = 0;
                                    int ApprovStatuswithTermination = 0;

                                    int RejectStatus = 0;
                                    int TerminateStatus = 0;
                                    foreach (var data7 in data2.ContractGrid)
                                    {
                                        if (data7.Report != null)
                                        {

                                            if (data7.Report.CredentialingApprovalStatus == null || data7.Report.CredentialingApprovalStatus == string.Empty)
                                            {
                                                NullStatus++;
                                            }
                                            if (data7.Report.CredentialingApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Rejected.ToString())
                                            {
                                                RejectStatus++;
                                            }
                                            if (data7.Report.CredentialingApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Approved.ToString())
                                            {
                                                ApprovStatus++;
                                                if (data7.Report.TerminationDate != null && data7.Report.TerminationDate > DateTime.Now)
                                                {
                                                    ApprovStatuswithTermination++;
                                                }
                                                if (data7.Report.TerminationDate == null)
                                                {
                                                    TerminateStatus = 1;
                                                }
                                            }
                                        }
                                    }
                                    var resultD = RejectStatus + ApprovStatus;
                                    if (resultD != 0 && resultD == ContractGridCount && ApprovStatus == ApprovStatuswithTermination)
                                    {
                                        var tmp = new
                                        {
                                            TableRowStatus = true,
                                            credID = data1.CredentialingInfoID,
                                            CredentialingContractRequests = data2,
                                        };
                                        CredentialingContractRequestsReference.Add(tmp);

                                    }
                                    else if (NullStatus == ContractGridCount)
                                    {
                                        var tmp = new
                                        {
                                            TableRowStatus = false,
                                            credID = data1.CredentialingInfoID,
                                            CredentialingContractRequests = data2,
                                        };
                                        CredentialingContractRequestsReference.Add(tmp);
                                    }
                                    else if (RejectStatus == ContractGridCount)
                                    {
                                        continue;
                                    }
                                    else if (RejectStatus + (ApprovStatus - ApprovStatuswithTermination) == ContractGridCount && TerminateStatus == 0)
                                    {
                                        continue;
                                    }
                                    else if (resultD != 0 && resultD == ContractGridCount && (RejectStatus + (ApprovStatus - ApprovStatuswithTermination)) < ContractGridCount)
                                    {
                                        bool TableStatus = false;
                                        if (TerminateStatus == 1)
                                        {
                                            TableStatus = false;
                                        }
                                        else
                                        {
                                            TableStatus = true;
                                        }
                                        var tmp = new
                                        {
                                            TableRowStatus = TableStatus,
                                            credID = data1.CredentialingInfoID,
                                            CredentialingContractRequests = data2,
                                        };
                                        CredentialingContractRequestsReference.Add(tmp);
                                    }

                                    else if (resultD < ContractGridCount)
                                    {
                                        var tmp = new
                                        {
                                            TableRowStatus = false,
                                            credID = data1.CredentialingInfoID,
                                            CredentialingContractRequests = data2,
                                        };
                                        CredentialingContractRequestsReference.Add(tmp);
                                    }

                                    else
                                    {
                                        var tmp = new
                                        {
                                            TableRowStatus = false,
                                            credID = data1.CredentialingInfoID,
                                            CredentialingContractRequests = data2,
                                        };
                                        CredentialingContractRequestsReference.Add(tmp);
                                    }
                                }
                            }
                        }
                    }
                }

                return CredentialingContractRequestsReference;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<object>> getAllCredentialinginfoByContractGrid(int ProviderID, int PlanID)
        {
            var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
            string IncludeProperties = "CredentialingLogs,CredentialingLogs.CredentialingActivityLogs,CredentialingContractRequests.ContractGrid.Report,CredentialingContractRequests.ContractGrid,CredentialingContractRequests.ContractPracticeLocations,CredentialingContractRequests,CredentialingContractRequests.ContractLOBs,CredentialingContractRequests.ContractSpecialties, CredentialingContractRequests.ContractGrid.Report, CredentialingContractRequests.ContractGrid.ProfileSpecialty.Specialty, CredentialingContractRequests.ContractGrid.ProfilePracticeLocation.Facility, CredentialingContractRequests.ContractGrid.BusinessEntity, CredentialingContractRequests.ContractGrid.LOB";
            var credentialingInfo = await credentialingInitiationInfoRepo.GetAllAsync(IncludeProperties);
            List<CredentialingInfo> credentialingInfo1 = new List<CredentialingInfo>();
            List<Object> CredentialingContractGridsReference = new List<Object>();

            foreach (var data in credentialingInfo)
            {
                if (data.ProfileID == ProviderID && data.PlanID == PlanID)
                {
                    foreach (var data1 in data.CredentialingContractRequests)
                    {
                        foreach (var data2 in data1.ContractGrid)
                        {
                            data2.CredentialingInfo = null;
                        }
                    }
                    credentialingInfo1.Add(data);
                }
            }
            foreach (var data1 in credentialingInfo1)
            {
                foreach (var data2 in data1.CredentialingContractRequests)
                {
                    if (data2.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString() && data2.ContractRequestStatus == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                    {
                        foreach (var data3 in data2.ContractGrid)
                        {
                            if (data3.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString() && data3.Report != null)
                            {
                                if (data3.Report.CredentialingApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Approved.ToString() && data3.Report.TerminationDate != null && data3.Report.TerminationDate>DateTime.Now)
                                {
                                    var ContractGridTemporaryObject = new
                                    {
                                        CredInfoID = data1.CredentialingInfoID,
                                        CredRequestID=data2.CredentialingContractRequestID,
                                        ContractGridObject = data3
                                    };
                                    CredentialingContractGridsReference.Add(ContractGridTemporaryObject);
                                }
                            }
                        }
                    }
                }
            }
            return CredentialingContractGridsReference;
        }

        public async Task<IEnumerable<Plan>> getAllPlanListforCredentialinginfoByContractRequest(int ProviderID)
        {
            try
            {
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                string IncludeProperties = "Plan";
                var credentialingInfo = await credentialingInitiationInfoRepo.GetAllAsync(IncludeProperties);
                List<CredentialingInfo> credentialingInfo1 = new List<CredentialingInfo>();
                List<Plan> Plan = new List<Plan>();
                foreach (var data in credentialingInfo)
                {
                    if (data.ProfileID == ProviderID)
                    {
                        Plan.Add(data.Plan);
                    }
                }
                int count = 0;
                List<Plan> arr = new List<Plan>();
                foreach (var p in Plan)
                {
                    if (count == 0)
                    {
                        arr.Add(p);
                        count++;
                    }
                    else
                    {
                        var count1 = 0;
                        for (var i = 0; i < arr.Count; i++)
                        {
                            if (p.PlanID == arr[i].PlanID)
                            {
                                count1++;
                            }
                        }
                        if (count1 == 0)
                        {
                            arr.Add(p);
                            count++;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                return arr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<CredentialingInfo>> GetCredInfoAsync(int credentialingInfoID)
        {
            try
            {
                string includeProperties = "Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,CredentialingLogs,CredentialingLogs.CredentialingActivityLogs";
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credList = credentialingInitiationInfoRepo.GetAsync(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties);
                return await credList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Object>> getCredentialingContractRequestForAllPlan(int ProviderID, int[] PlanIDs)
        {
            try
            {
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                string IncludeProperties = "Plan,CredentialingContractRequests.ContractGrid.Report,CredentialingContractRequests.ContractGrid,CredentialingContractRequests.ContractPracticeLocations,CredentialingContractRequests,CredentialingContractRequests.ContractLOBs,CredentialingContractRequests.ContractSpecialties, CredentialingContractRequests.ContractGrid.Report, CredentialingContractRequests.ContractGrid.ProfileSpecialty.Specialty, CredentialingContractRequests.ContractGrid.ProfilePracticeLocation.Facility, CredentialingContractRequests.ContractGrid.BusinessEntity, CredentialingContractRequests.ContractGrid.LOB";
                var credentialingInfo = await credentialingInitiationInfoRepo.GetAllAsync(IncludeProperties);
                List<CredentialingInfo> credentialingInfo1 = new List<CredentialingInfo>();
                List<Object> CredentialingContractRequestsReference = new List<Object>();
                foreach (var data in credentialingInfo)
                {
                    foreach (var planid in PlanIDs)
                    {
                        if (data.ProfileID == ProviderID && data.PlanID == planid)
                        {
                            foreach (var data1 in data.CredentialingContractRequests)
                            {
                                foreach (var data2 in data1.ContractGrid)
                                {
                                    data2.CredentialingInfo = null;
                                }

                            }
                            credentialingInfo1.Add(data);
                        }
                    }
                }
                foreach (var data1 in credentialingInfo1)
                {
                    foreach (var data2 in data1.CredentialingContractRequests)
                    {
                        if (data2.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString() && data2.ContractRequestStatus == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                        {
                            var ContractGrids1 = new List<ContractGrid>();
                            //bool ContractGridTableStatus = false;
                            foreach (var data3 in data2.ContractGrid)
                            {
                                if (data3.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString())
                                {
                                    ContractGrids1.Add(data3);
                                }

                            }
                            foreach (var tempData in ContractGrids1)
                            {
                                data2.ContractGrid.Remove(tempData);
                            }
                            int counter = 0;
                            int gridLength = data2.ContractGrid.Count;
                            if (gridLength > 0)
                            {
                                foreach (var data4 in data2.ContractGrid)
                                {
                                    if (data4.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                                    {
                                        if (data4.Report != null)
                                        {
                                            if (data4.Report.AdminFee == null && data4.Report.CAP == null && data4.Report.ContractDocumentPath == null && data4.Report.CredentialedDate == null && data4.Report.CredentialingApprovalStatus == null && data4.Report.GroupID == null && data4.Report.InitiatedDate == null && data4.Report.PanelStatus == null && data4.Report.PercentageOfRisk == null && data4.Report.ProviderID == null && data4.Report.ReCredentialingDate == null && data4.Report.Status == null && data4.Report.StopLossFee == null && data4.Report.TerminationDate == null && data4.Report.WelcomeLetterPath == null)
                                            {
                                                counter++;
                                            }
                                        }
                                    }
                                }
                            }
                            int counter1 = 0;
                            int gridLength1 = data2.ContractGrid.Count;
                            if (gridLength1 > 0)
                            {
                                foreach (var data5 in data2.ContractGrid)
                                {
                                    if (data5.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                                    {
                                        if (data5.Report != null)
                                        {
                                            if (data5.Report.CredentialingApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Rejected.ToString())
                                            {
                                                counter1++;
                                            }
                                        }
                                    }
                                }
                            }

                            int counter2 = 0;
                            int gridLength2 = data2.ContractGrid.Count;
                            if (gridLength2 > 0)
                            {
                                foreach (var data6 in data2.ContractGrid)
                                {
                                    if (data6.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                                    {
                                        if (data6.Report != null)
                                        {
                                            if (data6.Report.TerminationDate != null && data6.Report.TerminationDate < DateTime.Now)
                                            {
                                                counter2++;
                                            }
                                        }
                                    }
                                }
                            }
                            if (gridLength == counter && gridLength > 0)
                            {
                                //ContractGridTableStatus = true;
                                var tmp = new
                                {
                                    TableRowStatus = false,
                                    credID = data1.CredentialingInfoID,
                                    PlanObj=data1.Plan,
                                    CredentialingContractRequests = data2,
                                };
                                CredentialingContractRequestsReference.Add(tmp);
                            }
                            else if (gridLength1 == counter1 && gridLength1 > 0)
                            {
                                continue;
                            }
                            else if (gridLength2 == counter2 && gridLength2 > 0)
                            {
                                continue;
                            }
                            else
                            {
                                int ContractGridCount = data2.ContractGrid.Count;
                                if (ContractGridCount > 0)
                                {
                                    int NullStatus = 0;
                                    int ApprovStatus = 0;
                                    int ApprovStatuswithTermination = 0;

                                    int RejectStatus = 0;
                                    int TerminateStatus = 0;
                                    foreach (var data7 in data2.ContractGrid)
                                    {
                                        if (data7.Report != null)
                                        {

                                            if (data7.Report.CredentialingApprovalStatus == null || data7.Report.CredentialingApprovalStatus == string.Empty)
                                            {
                                                NullStatus++;
                                            }
                                            if (data7.Report.CredentialingApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Rejected.ToString())
                                            {
                                                RejectStatus++;
                                            }
                                            if (data7.Report.CredentialingApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Approved.ToString())
                                            {
                                                ApprovStatus++;
                                                if (data7.Report.TerminationDate != null && data7.Report.TerminationDate > DateTime.Now)
                                                {
                                                    ApprovStatuswithTermination++;
                                                }
                                                if (data7.Report.TerminationDate == null)
                                                {
                                                    TerminateStatus = 1;
                                                }
                                            }
                                        }
                                    }
                                    var resultD = RejectStatus + ApprovStatus;
                                    if (resultD != 0 && resultD == ContractGridCount && ApprovStatus == ApprovStatuswithTermination)
                                    {
                                        var tmp = new
                                        {
                                            TableRowStatus = true,
                                            credID = data1.CredentialingInfoID,
                                            PlanObj = data1.Plan,
                                            CredentialingContractRequests = data2,
                                        };
                                        CredentialingContractRequestsReference.Add(tmp);

                                    }
                                    else if (NullStatus == ContractGridCount)
                                    {
                                        var tmp = new
                                        {
                                            TableRowStatus = false,
                                            credID = data1.CredentialingInfoID,
                                            PlanObj = data1.Plan,
                                            CredentialingContractRequests = data2,
                                        };
                                        CredentialingContractRequestsReference.Add(tmp);
                                    }
                                    else if (RejectStatus == ContractGridCount)
                                    {
                                        continue;
                                    }
                                    else if (RejectStatus + (ApprovStatus - ApprovStatuswithTermination) == ContractGridCount && TerminateStatus == 0)
                                    {
                                        continue;
                                    }
                                    else if (resultD != 0 && resultD == ContractGridCount && (RejectStatus + (ApprovStatus - ApprovStatuswithTermination)) < ContractGridCount)
                                    {
                                        bool TableStatus = false;
                                        if (TerminateStatus == 1)
                                        {
                                            TableStatus = false;
                                        }
                                        else
                                        {
                                            TableStatus = true;
                                        }
                                        var tmp = new
                                        {
                                            TableRowStatus = TableStatus,
                                            credID = data1.CredentialingInfoID,
                                            PlanObj = data1.Plan,
                                            CredentialingContractRequests = data2,
                                        };
                                        CredentialingContractRequestsReference.Add(tmp);
                                    }

                                    else if (resultD < ContractGridCount)
                                    {
                                        var tmp = new
                                        {
                                            TableRowStatus = false,
                                            credID = data1.CredentialingInfoID,
                                            PlanObj = data1.Plan,
                                            CredentialingContractRequests = data2,
                                        };
                                        CredentialingContractRequestsReference.Add(tmp);
                                    }

                                    else
                                    {
                                        var tmp = new
                                        {
                                            TableRowStatus = false,
                                            credID = data1.CredentialingInfoID,
                                            PlanObj = data1.Plan,
                                            CredentialingContractRequests = data2,
                                        };
                                        CredentialingContractRequestsReference.Add(tmp);
                                    }
                                }
                            }
                        }
                    }
                }

                return CredentialingContractRequestsReference;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CredentialingInfo>> InitiateDrop(int CDUserId, int[] InfoidArray)
        {
            try
            {
                string IncludeProperties = "CredentialingLogs.CredentialingActivityLogs,CredentialingContractRequests";
                var credInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credentialingInitiationConReqRepo = uow.GetGenericRepository<CredentialingContractRequest>();
                List<CredentialingInfo> credList = new List<CredentialingInfo>();
                foreach (int CredId in InfoidArray)
                {


                    CredentialingInfo resultSet = await credInfoRepo.FindAsync(c => c.CredentialingInfoID == CredId, IncludeProperties);
                    int RecredID = resultSet.CredentialingInfoID;
                    resultSet.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                    foreach (var credReq in resultSet.CredentialingContractRequests)
                    {
                        credReq.ContractRequestStatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                        if (credReq.CredentialingRequestIDReference != null)
                        {
                            CredentialingContractRequest credentialingContractRequest = credentialingInitiationConReqRepo.Find(x => x.CredentialingContractRequestID == credReq.CredentialingRequestIDReference);
                            credentialingContractRequest.ContractRequestStatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                            credentialingInitiationConReqRepo.Update(credentialingContractRequest);
                            credentialingInitiationConReqRepo.Save();
                        }
                    }

                    CredentialingLog credLog = new CredentialingLog();
                    credLog.CredentialingType = AHC.CD.Entities.MasterData.Enums.CredentialingType.Dropped;

                    CredentialingActivityLog activityLog = new CredentialingActivityLog();
                    activityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                    activityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Dropped;
                    activityLog.ActivityByID = CDUserId;

                    credLog.CredentialingActivityLogs = new List<CredentialingActivityLog>();
                    credLog.CredentialingActivityLogs.Add(activityLog);
                    resultSet.CredentialingLogs.Add(credLog);

                    credList.Add(resultSet);
                    //foreach (var data1 in resultSet.CredentialingContractRequests)
                    //{
                    //    foreach (var data2 in data1.ContractGrid)
                    //    {
                    //        data2.CredentialingInfoID = RecredID;
                    //    }
                    //}
                    credInfoRepo.Update(resultSet);
                    credInfoRepo.Save();
                    //CredentialingInfo credentialingInfo = credInfoRepo.Find(x => x.CredentialingInfoID == RecredID, "CredentialingContractRequests,CredentialingContractRequests.ContractGrid");
                    //foreach (var data1 in credentialingInfo.CredentialingContractRequests)
                    //{
                    //    foreach (var data2 in data1.ContractGrid)
                    //    {
                    //        data2.CredentialingInfoID = credentialingInfo.CredentialingInfoID;
                    //    }
                    //}
                    //credInfoRepo.Update(credentialingInfo);
                    //credInfoRepo.Save();
                }

                return credList;
            }
            catch (Exception)
            {

                throw;
            }
        }





    }
}
