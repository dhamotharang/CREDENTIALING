using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.Credentialing.PSVInformation;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.Entities.PackageGenerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
                CredentialingInfo resultSet = await credInfoRepo.FindAsync(c => c.CredentialingInfoID == credInfo, "CredentialingContractRequests.PackageGeneratorReport,CredentialingContractRequests.ContractSpecialties.ProfileSpecialty,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentResult, CredentialingContractRequests.ContractPracticeLocations.ProfilePracticeLocation, CredentialingContractRequests.ContractLOBs.LOB, CredentialingContractRequests.BusinessEntity, CredentialingContractRequests.ContractGrid, Plan, Profile, Profile.SpecialtyDetails.Specialty, Profile.SpecialtyDetails.SpecialtyBoardCertifiedDetail.SpecialtyBoard, Profile.PracticeLocationDetails.Facility, Profile.PracticeLocationDetails.PracticeProviders, Profile.PersonalDetail.ProviderTitles.ProviderType, CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule, Profile.PracticeLocationDetails.BillingContactPerson, Profile.PracticeLocationDetails.OfficeHour, Profile.PracticeLocationDetails.PracticeProviders,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingActivityLogs.ActivityBy");
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
                foreach (var item3 in resultSet.CredentialingLogs)
                {
                    foreach (var item4 in item3.CredentialingActivityLogs)
                    {
                        item4.ActivityBy.CDRoles = null;
                    }
                }
                return resultSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetCredentialingInfoByProfileId(int profileID)
        {

            try
            {
                string includeProperties = "Plan, Profile, Profile.OtherIdentificationNumber, Profile.PersonalDetail,Profile.SpecialtyDetails.Specialty,Profile.PersonalDetail.ProviderTitles.ProviderType,Profile.ContractInfoes.ContractGroupInfoes.PracticingGroup.Group,CredentialingLogs, CredentialingContractRequests.ContractGrid.BusinessEntity, CredentialingContractRequests.ContractGrid.LOB, CredentialingContractRequests.ContractGrid.ProfilePracticeLocation.Facility, CredentialingContractRequests.ContractGrid.ProfileSpecialty.Specialty, CredentialingContractRequests.ContractGrid.Report";
                var credInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credInfo = await credInfoRepo.GetAsync(c => c.ProfileID == profileID, includeProperties);

                if (credInfo == null)
                    throw new Exception("Invalid Credentials");

                foreach (CredentialingInfo p in credInfo)
                {
                    foreach (CredentialingContractRequest ContractObj in p.CredentialingContractRequests)
                    {

                        foreach (ContractGrid GridObj in ContractObj.ContractGrid)
                        {

                            GridObj.CredentialingInfo = null;

                        }

                    }

                }

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

        public async Task<object> GetCredentialingInfoById(int credInfoID)
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
                    "Profile.SpecialtyDetails",
                    "Profile.SpecialtyDetails.Specialty",
                    "Plan",
                    "CredentialingLogs.CredentialingActivityLogs.Activities",     
                    "CredentialingLogs.CredentialingAppointmentDetail",
                    "CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists",
                    "CredentialingLogs.CredentialingActivityLogs.ActivityBy",
                    //"CredentialingLogs.CredentialingActivityLogs.ActivityBy.Profile",
                    //"CredentialingLogs.CredentialingActivityLogs.ActivityBy.Profile.PersonalDetail"
                };
                var credInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credInfo = await credInfoRepo.FindAsync(c => c.CredentialingInfoID == credInfoID, includeProperties);
               
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
                var includeProperties = "CredentialingLogs,CredentialingLogs.CredentialingActivityLogs,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentResult,CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians,Profile.PersonalDetail,Profile.PersonalDetail.ProviderTitles.ProviderType,Profile.SpecialtyDetails.Specialty,Plan,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentResult,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists.Specialty";
                CredentialingInfo resultSet = await credInfoRepo.FindAsync(c => c.CredentialingInfoID == credInfo, includeProperties);
                List<CredentialingLog> credentialingLog = new List<CredentialingLog>();
                foreach (var Data in resultSet.CredentialingLogs)
                {
                    if (Data.Credentialing == AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString())
                    {
                        credentialingLog.Add(Data);
                    }
                }
                foreach (var Data in resultSet.CredentialingLogs)
                {
                    if (Data.Credentialing != AHC.CD.Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString())
                    {
                        credentialingLog.Add(Data);
                    }
                }
                resultSet.CredentialingLogs = new List<CredentialingLog>();
                resultSet.CredentialingLogs = credentialingLog;
                return resultSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<string> SetCredentialingInfoStatusById(int credInfoID, string authId)
        {
            try
            {
                int userID = GetUserId(authId);
                var credInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var includeProperties = new string[]
                {
                    "CredentialingLogs",
                    "CredentialingLogs.CredentialingAppointmentDetail",
                    "CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians",
                    "CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists",
                    "CredentialingLogs.CredentialingActivityLogs"                    
                };

                CredentialingInfo resultSet = await credInfoRepo.FindAsync(c => c.CredentialingInfoID == credInfoID, includeProperties);
                //resultSet.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;

                //------------update  logs--------------------------------
                CredentialingLog latestCredentialingLog = resultSet.CredentialingLogs.OrderByDescending(c => c.LastModifiedDate).Where(x=>x.Credentialing == CD.Entities.MasterData.Enums.CredentialingType.Credentialing.ToString()).FirstOrDefault();
                if (latestCredentialingLog != null)
                {
                    CredentialingActivityLog credentialingActivityLog = new CredentialingActivityLog();
                    credentialingActivityLog.ActivityByID = userID;
                    credentialingActivityLog.ActivityType = CD.Entities.MasterData.Enums.ActivityType.Closure;
                    credentialingActivityLog.ActivityStatusType = CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                    latestCredentialingLog.CredentialingActivityLogs.Add(credentialingActivityLog);
                }
                credInfoRepo.Update(resultSet);
                credInfoRepo.Save();

                return "true";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int GetUserId(string authUserId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == authUserId);
                return user.CDUserID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TimelineActivity> AddTimelineActivityAsync(int credInfoId, TimelineActivity dataModelTimelineActivity)
        {
            try
            {
                var credInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var includeProperties = new string[]
                {
                    "CredentialingLogs.CredentialingActivityLogs.Activities"                    
                };

                CredentialingInfo resultSet = await credInfoRepo.FindAsync(c => c.CredentialingInfoID == credInfoId, includeProperties);

                //------------update  logs--------------------------------
                CredentialingLog firstCredentialingLog = resultSet.CredentialingLogs.FirstOrDefault();
                dataModelTimelineActivity.ActivityStatusType = CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                var isPresent = false;

                if (firstCredentialingLog != null)
                {
                    foreach (CredentialingActivityLog log in firstCredentialingLog.CredentialingActivityLogs)
                    {
                        if (log.Activity == "Timeline")
                        {
                            isPresent = true;
                            log.Activities.Add(dataModelTimelineActivity);
                            break;
                        }
                    }

                    if (!isPresent)
                    {
                        CredentialingActivityLog credentialingActivityLog = new CredentialingActivityLog();
                        credentialingActivityLog.ActivityByID = dataModelTimelineActivity.ActivityByID;
                        credentialingActivityLog.ActivityType = CD.Entities.MasterData.Enums.ActivityType.Timeline;
                        credentialingActivityLog.ActivityStatusType = CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                        credentialingActivityLog.Activities = new List<TimelineActivity>();
                        credentialingActivityLog.Activities.Add(dataModelTimelineActivity);
                        firstCredentialingLog.CredentialingActivityLogs.Add(credentialingActivityLog);
                    }

                }
                credInfoRepo.Update(resultSet);
                credInfoRepo.Save();
               // dataModelTimelineActivity.ActivityBy = GetCDUserByIdAsync((int)dataModelTimelineActivity.ActivityByID);
                //dataModelTimelineActivity.ActivityBy.CDRoles = null;
                return dataModelTimelineActivity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private CDUser GetCDUserByIdAsync(int cduserId)
        {
            try
            {
                var cduserrepo = uow.GetGenericRepository<CDUser>();
                var result = cduserrepo.Get(x => x.CDUserID == cduserId);
                result.FirstOrDefault().CDRoles = null;
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to return all generated packages of a Credentialing Contract Request
        /// </summary>
        /// <param name="credInfo"></param>
        /// <returns></returns>
        public async Task<List<EmailAttachment>> GetGeneratedPackagesAsync(int credInfo)
        {
            CredentialingContractRequest credRequest = await uow.GetGenericRepository<CredentialingContractRequest>().FindAsync(c => c.CredentialingContractRequestID == credInfo, "PackageGeneratorReport");
            List<EmailAttachment> allAttachments = new List<EmailAttachment>();
            if (credRequest.PackageGeneratorReport != null)
            {
                try
                {
                    string[] lookupDictionary = credRequest.PackageGeneratorReport.PackageGeneratorReportCode.Split('"');
                    for (int i = 0; i < lookupDictionary.Length; i++)
                    {
                        if (lookupDictionary[i] == "FilePath")
                        {
                            EmailAttachment att = new EmailAttachment();
                            att.AttachmentRelativePath = lookupDictionary[i + 2];
                            att.AttachmentServerPath = HttpContext.Current.Server.MapPath(att.AttachmentRelativePath);
                            if (att.AttachmentServerPath != "")
                            {
                                allAttachments.Add(att);
                            }
                        }
                    }
                    //foreach (PackageGenerator package in credRequest.PackageGenerators.ToList())
                    //{
                    //    EmailAttachment att = new EmailAttachment();
                    //    att.AttachmentRelativePath = package.PackageFilePath;
                    //    att.AttachmentServerPath = HttpContext.Current.Server.MapPath(package.PackageFilePath);
                    //    allAttachments.Add(att);
                    //}
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return allAttachments;
            //throw new NotImplementedException();
        }

    }
}
