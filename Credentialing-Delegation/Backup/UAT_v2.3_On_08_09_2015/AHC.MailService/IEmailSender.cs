using AHC.CD.Entities.EmailNotifications;
using System;
namespace AHC.MailService
{
    public interface IEmailSender
    {
        bool SendMail(EMailModel emailModel);
        bool SendMail();
    }
}
