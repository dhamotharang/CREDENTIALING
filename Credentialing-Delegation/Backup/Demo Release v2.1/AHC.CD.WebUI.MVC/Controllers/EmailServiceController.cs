using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHC.MailService;
using AHC.CD.WebUI.MVC.Models.ProviderViewModel.Shared;
using AHC.CD.Resources.Messages;
namespace AHC.CD.WebUI.MVC.Controllers
{
   
    public class EmailServiceController : Controller
    {
        private IEmailSender emailSender = null;
        public EmailServiceController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }
        
        // GET: EmailService

        public String Send(EmailViewModel emailViewModel)
        {
            // validate the view model
            // convert the view model to EMailModel

            string message;

            try
            {
                EMailModel emailModel = null;
               // emailModel = EmailTransformer.TransformToEmailModel(emailViewModel);
                emailSender.SendMail(emailModel);
                message = CompletionMessages.EMAIL_SENT_SUCCESSFULLY;
            }
            catch (Exception )
            {
                message = CompletionMessages.EMAIL_SENT_UNSUCCESSFULLY;

            }

            return message;

        }
    }
}