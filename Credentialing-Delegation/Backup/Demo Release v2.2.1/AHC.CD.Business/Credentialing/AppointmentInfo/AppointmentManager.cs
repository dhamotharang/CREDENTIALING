using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing.AppointmentInformation;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Exceptions.Credentialing;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.AppointmentInfo
{
    internal class AppointmentManager : IAppointmentManager
    {
        private IUnitOfWork uow = null;

        public AppointmentManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary>
        /// Method to save/update the CCM appointment form for an individual provider
        /// </summary>
        /// <param name="credentialingInfoID"></param>
        /// <param name="credentialingAppointmentDetail"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<int> UpdateAppointmentDetails(int credentialingInfoID, CredentialingAppointmentDetail credentialingAppointmentDetail, string authUserId)
        {
            try
            {
                int userID = GetUserId(authUserId);

                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var includeProperties = new string[]
                {
                    "CredentialingLogs",
                    "CredentialingLogs.CredentialingAppointmentDetail",
                    "CredentialingLogs.CredentialingActivityLogs"                    
                };
                CredentialingInfo updateCredentialingInfo = await credentialingInfoRepo.FindAsync(c => c.CredentialingInfoID == credentialingInfoID, includeProperties);

                if (updateCredentialingInfo.IsDelegated == false)
                {
                    throw new CredentialingException(ExceptionMessage.NOT_DELEGATED_EXCEPTION);
                }

                CredentialingLog latestCredentialingLog = updateCredentialingInfo.CredentialingLogs.OrderByDescending(c => c.LastModifiedDate).FirstOrDefault();

                if (latestCredentialingLog != null)
                {
                    if (latestCredentialingLog.CredentialingActivityLogs.Any(a => a.ActivityType == CD.Entities.MasterData.Enums.ActivityType.PSV && a.ActivityStatusType == AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed))
                    {
                        if (latestCredentialingLog.CredentialingAppointmentDetail == null)
                        {
                            latestCredentialingLog.CredentialingAppointmentDetail = new CredentialingAppointmentDetail();
                        }
                        latestCredentialingLog.CredentialingAppointmentDetail = AutoMapper.Mapper.Map<CredentialingAppointmentDetail, CredentialingAppointmentDetail>(credentialingAppointmentDetail, latestCredentialingLog.CredentialingAppointmentDetail);
                        CredentialingActivityLog addCredentialingActivityLog = new CredentialingActivityLog();
                        addCredentialingActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.CCMAppointment;
                        addCredentialingActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.InProgress;
                        addCredentialingActivityLog.ActivityByID = userID;
                        latestCredentialingLog.CredentialingActivityLogs.Add(addCredentialingActivityLog);
                    }
                    else
                    {
                        throw new CredentialingException(ExceptionMessage.PSV_INCOMPLETE_EXCEPTION);
                    }
                }

                credentialingInfoRepo.Update(updateCredentialingInfo);
                credentialingInfoRepo.Save();

                return updateCredentialingInfo.CredentialingInfoID;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        /// <summary>
        /// Method to schedule CCM appointment for multiple providers at one go
        /// </summary>
        /// <param name="credentialingAppointmentDetails"></param>
        /// <param name="appointmentDate"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<int>> ScheduleAppointmentForMany(List<int> credentialingAppointmentDetails, DateTime appointmentDate, string authUserId)
        {
            try
            {
                int userID = GetUserId(authUserId);

                var credentialingAppointmentDetailRepo = uow.GetGenericRepository<CredentialingAppointmentDetail>();
                IEnumerable<CredentialingAppointmentDetail> allCredentialingAppointmentDetails = await credentialingAppointmentDetailRepo.GetAllAsync();
                //List<CredentialingAppointmentDetail> updateCredentialingAppointmentDetail = new List<CredentialingAppointmentDetail>();
                foreach (var item in allCredentialingAppointmentDetails)
                {
                    foreach (int id in credentialingAppointmentDetails)
                    {
                        if (item.CredentialingAppointmentDetailID.Equals(id))
                        {
                            item.CredentialingAppointmentSchedule = new CredentialingAppointmentSchedule();
                            item.CredentialingAppointmentSchedule.AppointmentDate = appointmentDate;
                            item.CredentialingAppointmentSchedule.AppointmentSetByID = userID;
                            //updateCredentialingAppointmentDetail.Add(item);
                            credentialingAppointmentDetailRepo.Update(item);
                        }
                    }
                }
                credentialingAppointmentDetailRepo.Save();
                return credentialingAppointmentDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to remove the scheduled CCM appointment for an individual provider for Delegated Credentialing
        /// </summary>
        /// <param name="credentialingAppointmentDetailID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<int> RemoveScheduledAppointmentForIndividual(int credentialingAppointmentDetailID, string authUserId)
        {
            try
            {
                int userID = GetUserId(authUserId);

                var credentialingAppointmentDetailRepo = uow.GetGenericRepository<CredentialingAppointmentDetail>();
                CredentialingAppointmentDetail updateCredentialingAppointmentDetail = await credentialingAppointmentDetailRepo.FindAsync(c => c.CredentialingAppointmentDetailID == credentialingAppointmentDetailID, "CredentialingAppointmentSchedule");
                updateCredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate = null;
                credentialingAppointmentDetailRepo.Update(updateCredentialingAppointmentDetail);
                credentialingAppointmentDetailRepo.Save();
                return credentialingAppointmentDetailID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to save the result of CCM Meeting
        /// </summary>
        /// <param name="credentialingAppointmentDetailID"></param>
        /// <param name="credentialingAppointmentResult"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<int> SaveResultForScheduledAppointment(int credentialingAppointmentDetailID, CredentialingAppointmentResult credentialingAppointmentResult, string authUserId)
        {
            try
            {
                int userID = GetUserId(authUserId);

                var credentialingAppointmentDetailRepo = uow.GetGenericRepository<CredentialingAppointmentDetail>();
                CredentialingAppointmentDetail updateCredentialingAppointmentDetail = await credentialingAppointmentDetailRepo.FindAsync(c => c.CredentialingAppointmentDetailID == credentialingAppointmentDetailID, "CredentialingAppointmentResult");
                updateCredentialingAppointmentDetail.CredentialingAppointmentResult = new CredentialingAppointmentResult();
                updateCredentialingAppointmentDetail.CredentialingAppointmentResult = AutoMapper.Mapper.Map<CredentialingAppointmentResult, CredentialingAppointmentResult>(credentialingAppointmentResult, updateCredentialingAppointmentDetail.CredentialingAppointmentResult);
                updateCredentialingAppointmentDetail.CredentialingAppointmentResult.SignedByID = userID;
                credentialingAppointmentDetailRepo.Update(updateCredentialingAppointmentDetail);
                credentialingAppointmentDetailRepo.Save();

                var credentialingLogRepo = uow.GetGenericRepository<CredentialingLog>();
                CredentialingLog updateCredentialingLog = await credentialingLogRepo.FindAsync(c => c.CredentialingAppointmentDetail.CredentialingAppointmentDetailID == credentialingAppointmentDetailID);
                CredentialingActivityLog addCredentialingActivityLog = new CredentialingActivityLog();
                addCredentialingActivityLog.ActivityByID = userID;
                addCredentialingActivityLog.ActivityType = Entities.MasterData.Enums.ActivityType.CCMAppointment;
                addCredentialingActivityLog.ActivityStatusType = Entities.MasterData.Enums.ActivityStatusType.Completed;
                updateCredentialingLog.CredentialingActivityLogs.Add(addCredentialingActivityLog);
                credentialingLogRepo.Update(updateCredentialingLog);
                credentialingLogRepo.Save();

                return credentialingAppointmentDetailID;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        /// <summary>
        /// Private method to get CDUserID
        /// </summary>
        /// <param name="inputUserId"></param>
        /// <returns></returns>
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
    }
}
