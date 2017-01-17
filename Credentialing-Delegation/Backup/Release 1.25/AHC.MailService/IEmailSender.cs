using System;
namespace AHC.MailService
{
    public interface IEmailSender
    {
        bool SendMail(EMailModel emailModel);
    }
}
