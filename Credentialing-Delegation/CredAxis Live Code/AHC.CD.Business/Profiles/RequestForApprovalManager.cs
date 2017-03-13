using AHC.CD.Data.ADO.Profile;
using AHC.CD.Data.Repository;
using AHC.CD.Entities.Credentialing.CredentialingRequestTracker;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.CredentialingRequest;
using AutoMapper;
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
        public RequestForApprovalManager(IRequestForApprovalRepo iRequestForApprovalRepo, IUnitOfWork uow)
        {
            this.iRequestForApprovalRepo = iRequestForApprovalRepo;
            this.uow = uow;
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

        public async Task<bool> SetDecesionForCredRequestByIDAsync(int ID, string ApprovalType, string Reason)
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
                credentialingRequestTrackerData.ApprovalStatusType = ApprovalType == "Approved" ? ApprovalStatusType.Approved : ApprovalStatusType.Rejected;
                credentialingRequestTrackerData.RejectionReason = Reason;
                CredRequestTrackerRepo.Create(credentialingRequestTrackerData);
                CredRequestTrackerRepo.Save();
                CredRequestData.StatusType = StatusType.Inactive;
                CredRequestRepo.Update(CredRequestData);
                CredRequestRepo.Save();
                Status = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Status;
        }
    }
}
