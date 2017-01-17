using AHC.CD.Data.Repository;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.CnD
{
    internal class ApplicationManager : IApplicationManager
    {
        private IUnitOfWork uow = null;

        public ApplicationManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<CredentialingInfo> GetCredentialingInfoByIdAsync(int credInfo)
        {
            try
            {
                var credInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                CredentialingInfo resultSet = await credInfoRepo.FindAsync(c => c.CredentialingInfoID == credInfo, "CredentialingContractRequests.ContractSpecialties.ProfileSpecialty, CredentialingContractRequests.ContractPracticeLocations.ProfilePracticeLocation, CredentialingContractRequests.ContractLOBs.LOB, CredentialingContractRequests.BusinessEntity, CredentialingContractRequests.ContractGrid, Plan, Profile, Profile.SpecialtyDetails.Specialty, Profile.PracticeLocationDetails.Facility, Profile.PracticeLocationDetails.PracticeProviders, Profile.PersonalDetail.ProviderTitles.ProviderType, CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule");
                foreach (var item in resultSet.CredentialingContractRequests)
                {
                    if (item.ContractGrid == null)
                    {
                        item.ContractGrid = new List<ContractGrid>();
                    }
                    foreach (var item2 in item.ContractGrid)
                    {
                        item2.CredentialingInfo = null;
                    }
                }
                return resultSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<object> GetCredentialingInfoById(int profileId, int planId)
        {
            //try
            //{
            //    var credInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
            //    CredentialingInfo resultSet = await credInfoRepo.FindAsync(c => c.CredentialingInfoID == credInfo, "Plan, Profile, Profile.SpecialtyDetails.Specialty, Profile.PracticeLocationDetails.PracticeProviders, Profile.PersonalDetail.ProviderTitles.ProviderType");
            //    return resultSet;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            try
            {
                var includeProperties = new string[]
                {
                    "Profile",
                    "Profile.PersonalDetail",
                    "Plan",
                    "CredentialingLogs.CredentialingActivityLogs",
                    //"CredentialingLogs.CredentialingActivityLogs.ActivityBy",
                    //"CredentialingLogs.CredentialingActivityLogs.ActivityBy.Profile",
                    //"CredentialingLogs.CredentialingActivityLogs.ActivityBy.Profile.PersonalDetail"
                };
                var credInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credInfo = await credInfoRepo.FindAsync(p => p.ProfileID == profileId && p.PlanID == planId, includeProperties);

                if (credInfo == null)
                    throw new Exception("Invalid Credentials");

                return new
                {
                    credInfo = credInfo == null ? null : credInfo,
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
        }


        public async Task<CredentialingInfo> GetCredentialingFilterInfoByIdAsync(int credInfo)
        {
            try
            {
                var credInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var includeProperties = "CredentialingLogs,CredentialingLogs.CredentialingActivityLogs,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians,Profile.PersonalDetail,Profile.PersonalDetail.ProviderTitles.ProviderType,Profile.SpecialtyDetails.Specialty,Plan,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentResult,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists.Specialty";
                CredentialingInfo resultSet = await credInfoRepo.FindAsync(c => c.CredentialingInfoID == credInfo, includeProperties);
                CredentialingLog credentialingLog = resultSet.CredentialingLogs.OrderByDescending(c => c.LastModifiedDate).FirstOrDefault();
                if (credentialingLog != null)
                {
                    resultSet.CredentialingLogs = new List<CredentialingLog>();
                    resultSet.CredentialingLogs.Add(credentialingLog);
                }
                return resultSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
