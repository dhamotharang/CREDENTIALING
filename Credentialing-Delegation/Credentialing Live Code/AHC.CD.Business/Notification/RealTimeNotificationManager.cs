using AHC.CD.Data.ADO.Notification;
using AHC.CD.Data.Repository.Notification;
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
        private IRealTimeNotificationRepository realTimeNotificationRepository = new RealTimeNotificationADORepository();

        //public RealTimeNotificationManager(IRealTimeNotificationRepository realTimeNotificationRepository)
        //{
        //    this.realTimeNotificationRepository = realTimeNotificationRepository;
        //}

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
    }
}
