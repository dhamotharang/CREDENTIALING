using AHC.CD.Business.MasterData;
using AHC.CD.Business.Notification;
using AHC.CD.Data.ADO.Profile;
using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing.CredentialingRequestTracker;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.CredentialingRequest;
using AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal;
using AHC.CD.Entities.Notification;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    internal class RequestForApprovalManager : IRequestForApprovalManager
    {
        private readonly IRequestForApprovalRepo iRequestForApprovalRepo = null;
        private readonly IUnitOfWork uow = null;
        private IMasterDataManager masterDataManager = null;
        private IChangeNotificationManager changeNotificationManager = null;

        public RequestForApprovalManager(IRequestForApprovalRepo iRequestForApprovalRepo, IUnitOfWork uow, IMasterDataManager masterDataManager, IChangeNotificationManager changeNotificationManager)
        {
            this.iRequestForApprovalRepo = iRequestForApprovalRepo;
            this.uow = uow;
            this.masterDataManager = masterDataManager;
            this.changeNotificationManager = changeNotificationManager;
        }

        public async Task<dynamic> GetAllUpdatesAndRenewalsAsync()
        {
            dynamic UpdateAndRenwalDTO = null;
            try
            {
                UpdateAndRenwalDTO = await iRequestForApprovalRepo.GetAllUpdatesAndRenewalsRepo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UpdateAndRenwalDTO;
        }

        public async Task<dynamic> GetAllCredentialRequestsAsync()
        {
            dynamic CredentialRequestDTO = null;
            try
            {
                CredentialRequestDTO = await iRequestForApprovalRepo.GetAllCredentialRequestsRepo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CredentialRequestDTO;
        }

        public async Task<dynamic> GetAllHistoryAsync()
        {
            dynamic HistoryDTO = null;
            try
            {
                HistoryDTO = await iRequestForApprovalRepo.GetAllHistoryRepo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return HistoryDTO;
        }

        public async Task<dynamic> GetCredRequestDataByIDAsync(int ID)
        {
            dynamic CredRequestDataByIDDTO = null;
            try
            {
                CredRequestDataByIDDTO = await iRequestForApprovalRepo.GetCredRequestDataByIDRepo(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CredRequestDataByIDDTO;
        }

        public async Task<dynamic> GetCredRequestHistoryDataByIDAsync(int ID)
        {
            dynamic CredRequestHistoryDataByIDDTO = null;
            try
            {
                CredRequestHistoryDataByIDDTO = await iRequestForApprovalRepo.GetCredRequestHistoryDataByIDRepo(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CredRequestHistoryDataByIDDTO;
        }

        public async Task<bool> SetDecesionForCredRequestByIDAsync(int ID, string ApprovalType, string Reason, string UserID)
        {
            bool Status = false;
            try
            {
                var CredRequestRepo = uow.GetGenericRepository<CredentialingRequest>();
                var CredRequestTrackerRepo = uow.GetGenericRepository<CredentialingRequestTracker>();
                var CredRequestData = await CredRequestRepo.FindAsync(x => x.CredentialingRequestID == ID);
                CredentialingRequestTracker credentialingRequestTrackerData = new CredentialingRequestTracker();
                Mapper.CreateMap<CredentialingRequest, CredentialingRequestTracker>();
                credentialingRequestTrackerData = AutoMapper.Mapper.Map<CredentialingRequest, CredentialingRequestTracker>(CredRequestData);
                credentialingRequestTrackerData.ApprovalStatusType = ApprovalType == "Approved" ? ApprovalStatusType.Approved : ApprovalType=="Rejected"?ApprovalStatusType.Rejected:ApprovalStatusType.Dropped;
                credentialingRequestTrackerData.RejectionReason = Reason;
                if (ApprovalType == "Approved")
                {
                    Status = await CredentialingInitiation(CredRequestData,UserID);
                }
                CredRequestTrackerRepo.Create(credentialingRequestTrackerData);
                CredRequestTrackerRepo.Save();
                CredRequestData.StatusType = StatusType.Inactive;
                CredRequestRepo.Update(CredRequestData);
                CredRequestRepo.Save();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Status;
        }

        public async Task<dynamic> GetAllUpdatesAndRenewalsForProviderAsync(int ID)
        {
            dynamic UpdateAndRenwalDTO = null;
            try
            {
                UpdateAndRenwalDTO = await iRequestForApprovalRepo.GetAllUpdatesAndRenewalsForProviderRepo(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UpdateAndRenwalDTO;
        }

        public async Task<dynamic> GetAllCredentialRequestsForProviderAsync(int ID)
        {
            dynamic CredRequestDataByIDDTO = null;
            try
            {
                CredRequestDataByIDDTO = await iRequestForApprovalRepo.GetAllCredentialRequestsForProviderRepo(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CredRequestDataByIDDTO;
        }

        public async Task<dynamic> GetAllHistoryForProviderAsync(int ID)
        {
            dynamic HistoryDTO = null;
            try
            {
                HistoryDTO = await iRequestForApprovalRepo.GetAllHistoryForProviderRepo(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return HistoryDTO;
        }

        public async Task<int?> GetProfileID(string AuthID)
        {
            var userRepo = uow.GetGenericRepository<CDUser>();
            var user = await userRepo.FindAsync(u => u.AuthenicateUserId == AuthID);
            return user.ProfileId;
        }

        public async Task<bool> SetApprovalByIDs(List<int> trackerIDs, string userAuthID)
        {
            bool status = false;
            try
            {
                var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();
                var userID = await GetCDUserID(userAuthID);

                foreach (var trackerID in trackerIDs)
                {
                    var requestObject = await trackerRepo.FindAsync(p => p.ProfileUpdatesTrackerId == trackerID);

                    requestObject.ApprovalStatus = ApprovalStatusType.Approved.ToString();
                    requestObject.LastModifiedBy = userID;
                    requestObject.LastModifiedDate = DateTime.Now;
                    trackerRepo.Update(requestObject);
                }

                await trackerRepo.SaveAsync();
                status = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return status;
        }

        public async Task AddCredRequestTrackerNotification(List<int> credIDs, string approvalType, string userName)
        {
            try
            {
                UserDashboardNotification notification = null;
                List<Tuple<int, UserDashboardNotification>> userNotifications = new List<Tuple<int, UserDashboardNotification>>();

                var CredRequestRepo = uow.GetGenericRepository<CredentialingRequest>();

                foreach (var credID in credIDs)
                {
                    var CredRequestData = await CredRequestRepo.FindAsync(x => x.CredentialingRequestID == credID);

                    notification = ConstructNotificationObject(CredRequestData, approvalType, userName);

                    var plan = await masterDataManager.GetPlanByIDAsync(CredRequestData.PlanID);

                    notification.ActionPerformed = "Credentialing Request for " + plan.PlanName + " - " + approvalType;

                    userNotifications.Add(new Tuple<int, UserDashboardNotification>(CredRequestData.ProfileID, notification));

                }

                await changeNotificationManager.SaveRequestTrackerNotifcation(userNotifications);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddUpdatesRequestTrackerNotification(List<int> trackerIDs, string approvalType, string userName)
        {
            try
            {
                UserDashboardNotification notification = null;
                List<Tuple<int, UserDashboardNotification>> userNotifications = new List<Tuple<int, UserDashboardNotification>>();

                var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();

                foreach (var trackerID in trackerIDs)
                {
                    var tracker = await trackerRepo.FindAsync(t => t.ProfileUpdatesTrackerId == trackerID);

                    notification = ConstructUpdatesTrackerNotificationObject(tracker, approvalType, userName);

                    userNotifications.Add(new Tuple<int, UserDashboardNotification>(tracker.ProfileId, notification));
                
                }

                await changeNotificationManager.SaveRequestTrackerNotifcation(userNotifications);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Private Methods

        private async Task<int> GetCDUserID(string userAuthID)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = await userRepo.FindAsync(u => u.AuthenicateUserId == userAuthID);
                return user.CDUserID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> CredentialingInitiation(CredentialingRequest CredRequestData, string UserID)
        {
            try
            {
                var credentialingInitiationInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                CredentialingInfo credentialingInitiationInfo = new CredentialingInfo();
                Mapper.CreateMap<CredentialingRequest, CredentialingInfo>().ForMember(d=>d.IsDelegated,o=>o.MapFrom(s=>s.IsDelegated));
                credentialingInitiationInfo = AutoMapper.Mapper.Map<CredentialingRequest, CredentialingInfo>(CredRequestData);
                List<CredentialingLog> credentialingLogList = new List<CredentialingLog>();
                List<CredentialingActivityLog> credActivityLogList = new List<CredentialingActivityLog>();
                int initiator = await GetCDUserID(UserID);
                CredentialingLog credLog = new CredentialingLog();
                credentialingInitiationInfo.InitiatedByID = initiator;
                credentialingInitiationInfo.InitiationDate = DateTime.Now;
                credLog.CredentialingType = CredentialingType.Credentialing;
                CredentialingActivityLog credActivityLog = new CredentialingActivityLog();
                credActivityLog.ActivityByID = initiator;
                credActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                credActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Initiation;
                credActivityLogList.Add(credActivityLog);
                credLog.CredentialingActivityLogs = credActivityLogList;
                credentialingLogList.Add(credLog);
                credentialingInitiationInfo.CredentialingLogs = credentialingLogList;
                credentialingInitiationInfoRepo.Create(credentialingInitiationInfo);
                credentialingInitiationInfoRepo.Save();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private UserDashboardNotification ConstructNotificationObject(CredentialingRequest credRequest, string approvalType, string userName)
        {
            

            UserDashboardNotification notification = new UserDashboardNotification();
            notification.AcknowledgementStatusType = AcknowledgementStatusType.Unread;
            //notification.Action = "Credentialing Request for " + plan.PlanName + " - "+ approvalType; 
            notification.Action = "Credentialing Request";
            notification.ActionPerformedByUser = userName;
            notification.RedirectURL = "/Credentialing/RequestForApproval/Index";
            notification.StatusType = StatusType.Active;

            return notification;
        }

        private UserDashboardNotification ConstructUpdatesTrackerNotificationObject(ProfileUpdatesTracker updatesRequest, string approvalType, string userName)
        {
            //dynamic uniqueData =  updatesRequest.UniqueData != null ? JsonConvert.DeserializeObject(updatesRequest.UniqueData) : "";

            UserDashboardNotification notification = new UserDashboardNotification();
            notification.AcknowledgementStatusType = AcknowledgementStatusType.Unread;
            notification.Action = "Credentialing Request";
            //notification.ActionPerformed = updatesRequest.Section + " - " + updatesRequest.SubSection + (uniqueData != null ? " - " + uniqueData.FieldName : "") + " - " + approvalType;
            notification.ActionPerformed = updatesRequest.Section + " - " + updatesRequest.SubSection + " - " + updatesRequest.Modification + " request - " + approvalType;
            notification.ActionPerformedByUser = userName;
            notification.RedirectURL = "/Profil/MasterProfile/Index";
            notification.StatusType = StatusType.Active;

            return notification;
        }

        #endregion





        
    }
}
