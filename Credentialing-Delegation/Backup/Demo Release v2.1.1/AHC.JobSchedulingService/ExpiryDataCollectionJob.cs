using AHC.CD.Business.Notification;
using AHC.CD.Data.EFRepository;
using AHC.MailService;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.JobSchedulingService
{
    /// <summary>
    /// Author: Venkat
    /// Date: 19/03/2015
    /// Executes the job on specified interval of time
    /// </summary>
    /// 
    public class ExpiryDataCollectionJob : IJob
    {
        private IExpiryNotificationManager expiryNotificationManager = null;
        public ExpiryDataCollectionJob()
        {
            expiryNotificationManager = new ExpiryNotificationManager(new EFUnitOfWork(), new EmailSender());
        }
        public void Execute(IJobExecutionContext context)
        {
            expiryNotificationManager.SaveExpiryNotificationAsync();
        }
    }
}
