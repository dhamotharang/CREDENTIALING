using AHC.CD.Data.ADO.Notification;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Notification;
using AHC.CD.Entities;
using AHC.CD.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Notification
{
    public class RealTimeNotificationManager : IRealTimeNotificationManager
    {
        private IRealTimeNotificationRepository realTimeNotificationRepository = null;
        private readonly IUnitOfWork uow = null;

        public RealTimeNotificationManager(IUnitOfWork uow, IRealTimeNotificationRepository realTimeNotificationRepository)
        {
            this.uow = uow;
            this.realTimeNotificationRepository = realTimeNotificationRepository;
        }

        public int GetAllPendingRequestCount(NotificationDelegate.DependencyDelegate dependencyDelegate)
        {
            try
            {
                return realTimeNotificationRepository.GetAllPendingRequestCount(dependencyDelegate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public dynamic GetAllPendingRequest(NotificationDelegate.DependencyDelegate dependencyDelegate)
        {
            try
            {
                return realTimeNotificationRepository.GetAllPendingRequest(dependencyDelegate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public dynamic GetTaskExpiriesCount(NotificationDelegate.DependencyDelegate dependencyDelegate)
        {
            try
            {
                return realTimeNotificationRepository.GetTaskExpiriesCount(dependencyDelegate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCDUserID(string userAuthID)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == userAuthID);
                if (user != null)
                    return user.CDUserID;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
