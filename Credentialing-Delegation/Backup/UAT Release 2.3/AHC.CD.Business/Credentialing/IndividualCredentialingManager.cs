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
                var sortData = await credentialingInitiationInfoRepo.GetAsync(s => s.Status == Entities.MasterData.Enums.StatusType.Active.ToString() || s.Status == Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties);
                foreach (var item in sortData)
                {
                    item.CredentialingContractRequests = null;
                    item.CredentialingVerificationInfos = null;
                    if (item.CredentialingLogs.First().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString() && (item.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString() || item.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.Dropped.ToString() || item.CredentialingLogs.Last().Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()))
                    {
                        credList.Add(item);
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
                var credList1 = await credentialingInitiationInfoRepo.GetAsync(s => s.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() || s.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString(), includeProperties);

                List<CredentialingInfo> credList2 = new List<CredentialingInfo>();
                foreach (var cL in credList1)
                {
                    if (cL.CredentialingLogs.Last().Credentialing == Entities.MasterData.Enums.CredentialingType.DeCredentialing.ToString() || cL.CredentialingLogs.Last().Credentialing == Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString())
                    {
                        credList2.Add(cL);
                    }
                }

                var credList = from provider in credList2
                               group provider by new
                               {
                                   provider.PlanID,
                                   provider.ProfileID
                               }
                                   into g
                                   select new CredentialingInfo
                                   {
                                       CredentialingInfoID = g.Select(c => c.CredentialingInfoID).Last(),
                                       ProfileID = g.Key.ProfileID,
                                       Profile = g.Select(c => c.Profile).FirstOrDefault(),
                                       PlanID = g.Key.PlanID,
                                       Plan = g.Select(c => c.Plan).Last(),
                                       InitiatedByID = g.Select(c => c.InitiatedByID).Last(),
                                       IsDelegatedYesNoOption = g.Select(c => c.IsDelegatedYesNoOption).Last(),
                                       LastModifiedDate = g.Select(c => c.LastModifiedDate).Max(),
                                       InitiationDate = g.Select(c => c.InitiationDate).Last(),
                                       CredentialingVerificationInfos = g.Select(c => c.CredentialingVerificationInfos).Last(),
                                       CredentialingLogs = g.Select(c => c.CredentialingLogs).Last(),
                                       InitiatedBy = g.Select(c => c.InitiatedBy).Last(),
                                       CredentialingContractRequests = g.Select(c => c.CredentialingContractRequests).Last()

                                   };


                List<CredentialingInfo> decredList = new List<CredentialingInfo>();
                foreach (var d in credList)
                {
                    foreach (CredentialingContractRequest ContractObj in d.CredentialingContractRequests)
                    {

                        foreach (ContractGrid GridObj in ContractObj.ContractGrid)
                        {

                            GridObj.CredentialingInfo = null;

                        }

                    }
                    int flag = 0;
                    foreach (var item in d.CredentialingLogs)
                    {

                        if (item.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialing.ToString() || item.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString())
                        {
                            flag = 1;
                            break;
                        }

                    }
                    if (flag == 1)
                    {
                        decredList.Add(d);
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
                string includeProperties = "Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,CredentialingLogs,CredentialingLogs.CredentialingActivityLogs,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists,CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentResult";
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credList = await credentialingInitiationInfoRepo.GetAsync(s => s.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() || s.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString(), includeProperties);

                List<CredentialingInfo> reCredList = new List<CredentialingInfo>();
                foreach (var d in credList)
                {
                    bool stat = true;
                    foreach (var k in d.CredentialingLogs)
                    {
                        if (k.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialing.ToString())
                        {
                            stat = false;
                            break;
                        }
                    }

                    foreach (var l in d.CredentialingLogs)
                    {
                        if ((d.CredentialingLogs.First().Credentialing != Entities.MasterData.Enums.CredentialingType.Credentialing.ToString()) && (l.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString() || l.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialingInitiated.ToString()) && stat)
                        {
                            reCredList.Add(d);
                            break;
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
                var PlanRepo=uow.GetGenericRepository<Plan>();
                Plan plan=PlanRepo.GetAll().Where(x=>x.PlanID==credentialingInitiationInfo.PlanID).First();
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
                                    if(data.ContractGrid.ElementAt(cg).Report!=null){
                                        if (data.ContractGrid.ElementAt(cg).Report.CredentialingApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Rejected.ToString())
                                        {
                                            ContractGridCondition2.Add(data.ContractGrid.ElementAt(cg));
                                        }
                                    }
                                    else{
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
                    foreach (var data2 in data1.ContractGrid)
                    {
                        data2.Report = new CredentialingContractInfoFromPlan();
                    }
                }
                credentialingInitiationInfo.CredentialingContractRequests = credentialingInitiationConReqList;
                credentialingInitiationInfoRepo.Create(credentialingInitiationInfo);
                credentialingInitiationInfoRepo.Save();
                return credentialingInitiationInfo.CredentialingInfoID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InitiateDeCredentialingAsync(int count, int credentialingInfoID, int contractRequestId, int gridId, string userAuthId, CredentialingLog CredentialingLog)
        {
            var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
            //var loadedContractRepo = uow.GetGenericRepository<LoadedContract>();
            //List<CredentialingLog> credentialingLogList = new List<CredentialingLog>();

            string includeProperties = "CredentialingLogs,CredentialingContractRequests.ContractGrid.Report,Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,CredentialingLogs.CredentialingActivityLogs,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists,CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentResult,CredentialingLogs.CredentialingVerificationInfo";

            try
            {
                CredentialingInfo credInfo = credentialingInitiationInfoRepo.GetAll(includeProperties).Where(x => x.CredentialingInfoID == credentialingInfoID).First();
                CredentialingLog credentialingLog = credInfo.CredentialingLogs.ToList().Last();

                if (credInfo != null)
                {
                    //foreach (var a in credInfo.CredentialingLogs)
                    //{
                    //    if (a.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.DeCredentialing.ToString())
                    //    {
                    //        throw new AHC.CD.Exceptions.Credentialing.CredentialingException(ExceptionMessage.INITIATE_DECREDENTIALING_EXIST_EXCEPTION);
                    //    }
                    //}

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
                    if (count == 0)
                    {
                        List<CredentialingActivityLog> credActivityLogList = new List<CredentialingActivityLog>();
                        CredentialingLog credLog = new CredentialingLog();
                        credLog.CredentialingType = CredentialingType.DeCredentialingInitiated;
                        credLog.CredentialingAppointmentDetail = credentialingLog.CredentialingAppointmentDetail;
                        credLog.CredentialingVerificationInfo = credentialingLog.CredentialingVerificationInfo;
                        CredentialingActivityLog credActivityLog = credentialingLog.CredentialingActivityLogs.Last();
                        //credActivityLog.ActivityByID = initiator.CDUserID;
                        //credActivityLog.ActivityStatusType = credentialingLog.CredentialingActivityLogs.Last().ActivityStatusType;
                        //credActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Initiation;

                        credActivityLogList.Add(credActivityLog);
                        credLog.CredentialingActivityLogs = credActivityLogList;

                        //credLog.CredentialingActivityLogs.Add(credActivityLog);
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
                // List<CredentialingActivityLog> credActivityLogList = new List<CredentialingActivityLog>();
                string includeProperties = "CredentialingLogs,CredentialingContractRequests.ContractGrid.Report,Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,CredentialingLogs.CredentialingActivityLogs,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists,CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentResult,CredentialingLogs.CredentialingVerificationInfo";

                // string includeProperties = "CredentialingLogs,CredentialingContractRequests.ContractGrid.Report";
                CredentialingInfo credInfo = credentialingInitiationInfoRepo.GetAll(includeProperties).Where(x => x.CredentialingInfoID == credentialingInfoIDs[0]).First();

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
                    credLog.CredentialingAppointmentDetail = credentialingLog.CredentialingAppointmentDetail;
                    credLog.CredentialingVerificationInfo = credentialingLog.CredentialingVerificationInfo;
                    CredentialingActivityLog credActivityLog = credentialingLog.CredentialingActivityLogs.Last();
                    //credActivityLog.ActivityByID = initiator.CDUserID;
                    //credActivityLog.ActivityStatusType = credentialingLog.CredentialingActivityLogs.Last().ActivityStatusType;
                    //credActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Initiation;

                    credActivityLogList.Add(credActivityLog);
                    credLog.CredentialingActivityLogs = credActivityLogList;

                    //credLog.CredentialingActivityLogs.Add(credActivityLog);
                    credInfo.CredentialingLogs.Add(credLog);

                    credInfo.LastModifiedDate = (System.DateTime)credLog.LastModifiedDate;


                    //CredentialingLog credLog = new CredentialingLog();
                    //credLog.CredentialingType = CredentialingType.DeCredentialing;

                    //CredentialingActivityLog credActivityLog = new CredentialingActivityLog();
                    //credActivityLog.ActivityByID = initiator.CDUserID;
                    //credActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                    //credActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Initiation;

                    //credActivityLogList.Add(credActivityLog);
                    //credLog.CredentialingActivityLogs = credActivityLogList;

                    //credLog.CredentialingActivityLogs.Add(credActivityLog);
                    //credInfo.CredentialingLogs.Add(credLog);
                    credentialingInitiationInfoRepo.Update(credInfo);
                    credentialingInitiationInfoRepo.Save();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CredentialingInfo>> getAllCredentialinginfoByContractRequest(int ProviderID, int PlanID)
        {
            try
            {
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                string IncludeProperties = "CredentialingLogs,CredentialingLogs.CredentialingActivityLogs,CredentialingContractRequests.ContractGrid.Report,CredentialingContractRequests.ContractGrid,CredentialingContractRequests.ContractPracticeLocations,CredentialingContractRequests,CredentialingContractRequests.ContractLOBs,CredentialingContractRequests.ContractSpecialties, CredentialingContractRequests.ContractGrid.Report, CredentialingContractRequests.ContractGrid.ProfileSpecialty.Specialty, CredentialingContractRequests.ContractGrid.ProfilePracticeLocation.Facility, CredentialingContractRequests.ContractGrid.BusinessEntity, CredentialingContractRequests.ContractGrid.LOB";
                var credentialingInfo = await credentialingInitiationInfoRepo.GetAllAsync(IncludeProperties);
                List<CredentialingInfo> credentialingInfo1 = new List<CredentialingInfo>();
                List<CredentialingInfo> credentialingInfo2 = new List<CredentialingInfo>();

                foreach (var data in credentialingInfo)
                {
                    int flag = 0;
                    foreach(var data1 in data.CredentialingLogs)
                    {
                        if (data1.Credentialing==AHC.CD.Entities.MasterData.Enums.CredentialingType.Dropped.ToString())
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag==0)
                    {
                        credentialingInfo2.Add(data);
                    }
                }

                foreach (var data in credentialingInfo2)
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

                return credentialingInfo1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public async Task<IEnumerable<CredentialingInfo>> getCredentialingContractRequestForAllPlan(int ProviderID, int[] PlanIDs)
        {
            try
            {
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                string IncludeProperties = "Plan,CredentialingContractRequests.ContractGrid.Report,CredentialingContractRequests.ContractGrid,CredentialingContractRequests.ContractPracticeLocations,CredentialingContractRequests,CredentialingContractRequests.ContractLOBs,CredentialingContractRequests.ContractSpecialties, CredentialingContractRequests.ContractGrid.Report, CredentialingContractRequests.ContractGrid.ProfileSpecialty.Specialty, CredentialingContractRequests.ContractGrid.ProfilePracticeLocation.Facility, CredentialingContractRequests.ContractGrid.BusinessEntity, CredentialingContractRequests.ContractGrid.LOB";
                var credentialingInfo = await credentialingInitiationInfoRepo.GetAllAsync(IncludeProperties);
                List<CredentialingInfo> credentialingInfo1 = new List<CredentialingInfo>();
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
                return credentialingInfo1;
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
                List<CredentialingInfo> credList = new List<CredentialingInfo>();
                
                foreach (int CredId in InfoidArray)
                {

                    var credInfoRepo = uow.GetGenericRepository<CredentialingInfo>();

                    CredentialingInfo resultSet = await credInfoRepo.FindAsync(c => c.CredentialingInfoID == CredId, "CredentialingLogs.CredentialingActivityLogs");
                    //resultSet.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;

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

                    credInfoRepo.Update(resultSet);
                    credInfoRepo.Save();

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
