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
    /// Author: Shalabh
    /// This job is to manage sending mails
    /// </summary>
    public class EmailNotificationSchedulingJob : IJob
    {
        private IEmailSender emailSender = null;

        public EmailNotificationSchedulingJob()
        {
            emailSender = new EmailSender(new EFUnitOfWork());
        }

        public void Execute(IJobExecutionContext context)        
        {
            emailSender.SendMail();
            //throw new NotImplementedException();
        }
    }
}
