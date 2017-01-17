using AHC.CD.Business.Notification;
using AHC.CD.Data.EFRepository;
using AHC.MailService;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.JobSchedulingService
{
    /// <summary>
    /// Author: Venkat
    /// Date: 19/03/2015
    /// Executes the job on specified interval of time
    /// </summary>
    /// 
    public class ExpiryEmailNotificationJob : IJob
    {
        private IExpiryNotificationManager expiryNotificationManager = null;
        
        public ExpiryEmailNotificationJob()
        {
            expiryNotificationManager = new ExpiryNotificationManager(new EFUnitOfWork(), new EmailSender());
        }
        public void Execute(IJobExecutionContext context)
        {
            expiryNotificationManager.NotifyExpiries();
        }
    }
}
    