using AHC.MailService;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.JobSchedulingService
{
    public class EmailNotificationJob : IJob
    {
        IEmailSender emailService = null;
        public EmailNotificationJob()
        {
            emailService = new EmailSender();
        }
        public void Execute(IJobExecutionContext context)
        {
            EMailModel eModel = new EMailModel();
            eModel.To = "venkatshiva.reddy@gmail.com";
            eModel.Subject = "Testing Expiration Notification Service";
            eModel.Body = "This is to test the certificate expiration notification service";
            emailService.SendMail(eModel);
        }
    }
}
