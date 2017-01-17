using AHC.CD.Business.Notification;
using AHC.CD.Data.EFRepository;
using AHC.CD.Data.Repository;
using AHC.MailService;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.JobSchedulingService
{
    /// <summary>
    /// Author: Santosh
    /// </summary>
    public class CCMMailNotification : IJob
    {
        private IChangeNotificationManager changeNotificationManager = null;

        public CCMMailNotification()
        {
            changeNotificationManager = new ChangeNotificationManager(new EFUnitOfWork(), new EmailSender());
        }
       
        public void Execute(IJobExecutionContext context)
        {
            changeNotificationManager.NotifyChangesForCCM();
        }
    }
}
