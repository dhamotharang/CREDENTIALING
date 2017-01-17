using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHC.MailService;
using AHC.CD.WebUI.MVC.Models.ProviderViewModel.Shared;
using AHC.CD.Resources.Messages;
using System.Threading.Tasks;
using AHC.CD.Business.Email;
using Newtonsoft.Json;
using AHC.CD.WebUI.MVC.Models.EmailService;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.Exceptions;
using AHC.CD.ErrorLogging;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;
namespace AHC.CD.WebUI.MVC.Controllers
{
   
    public class EmailServiceController : Controller
    {
        private IEmailSender emailSender = null;
        private IEmailServiceManager emailServiceManager = null;
        private IErrorLogger errorLogger = null;
        
        protected ApplicationUserManager _authUserManager;
        protected ApplicationUserManager AuthUserManager
        {
            get
            {
                return _authUserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _authUserManager = value;
            }
        }        

        public EmailServiceController(IEmailSender emailSender, IErrorLogger errorLogger, IEmailServiceManager emailServiceManager)
        {
            this.errorLogger = errorLogger;
            this.emailSender = emailSender;
            this.emailServiceManager = emailServiceManager;
        }
        
        // GET: EmailService

        public ActionResult Index()
        {
            return View();
        }

        public async Task<string> GetAllEmails()
        {
            var data = await emailServiceManager.GetAllEmailInfo();
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        public async Task<string> GetAllEmailTemplates()
        {
            var data = await emailServiceManager.GetAllEmailTemplatesAsync();
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        /// <summary>
        /// Method to fetch active emails with recurrence
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAllActiveFollowUpEmails()
        {
            var data = await emailServiceManager.GetAllActiveFollowUpEmailsAsync();
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        /// <summary>
        /// Method to fetch recieved emails
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAllInboxEmails()
        {
            var data = await emailServiceManager.GetAllInboxEmailsAsync(await GetUserAuthId());
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Method to stack an email to the DB
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddEmail(EmailServiceViewModel email)
        {
            EmailInfo dataEmailInfo = null;
            string status = "true";
            if (email.Body != null)
            {
                email.Body = email.Body.Replace("&lt;", "<");
                email.Body = email.Body.Replace("&gt;", ">");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    dataEmailInfo = AutoMapper.Mapper.Map<EmailServiceViewModel, EmailInfo>(email);
                    dataEmailInfo.SendingDate = DateTime.Now;
                    dataEmailInfo.From = "santosh.senapati@pratian.com";
                    if (dataEmailInfo.IsRecurrenceEnabled == Entities.MasterData.Enums.YesNoOption.YES.ToString())
                    {
                        dataEmailInfo.EmailRecurrenceDetail = new EmailRecurrenceDetail();
                        dataEmailInfo.EmailRecurrenceDetail = AutoMapper.Mapper.Map<EmailServiceViewModel, EmailRecurrenceDetail>(email);
                        dataEmailInfo.EmailRecurrenceDetail.IsRecurrenceScheduledYesNoOption = Entities.MasterData.Enums.YesNoOption.NO;
                        if (dataEmailInfo.EmailRecurrenceDetail.RecurrenceIntervalType == Entities.MasterData.Enums.RecurrenceIntervalType.Daily.ToString())
                        {
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = dataEmailInfo.SendingDate.Value.AddDays(dataEmailInfo.EmailRecurrenceDetail.IntervalFactor.Value);
                        }
                        else if (dataEmailInfo.EmailRecurrenceDetail.RecurrenceIntervalType == Entities.MasterData.Enums.RecurrenceIntervalType.Weekly.ToString())
                        {
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = dataEmailInfo.SendingDate.Value.AddDays(7 * dataEmailInfo.EmailRecurrenceDetail.IntervalFactor.Value);
                        }
                        else if (dataEmailInfo.EmailRecurrenceDetail.RecurrenceIntervalType == Entities.MasterData.Enums.RecurrenceIntervalType.Monthly.ToString())
                        {
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = dataEmailInfo.SendingDate.Value.AddMonths(dataEmailInfo.EmailRecurrenceDetail.IntervalFactor.Value);
                        }
                        else if (dataEmailInfo.EmailRecurrenceDetail.RecurrenceIntervalType == Entities.MasterData.Enums.RecurrenceIntervalType.Quarterly.ToString())
                        {
                            dataEmailInfo.EmailRecurrenceDetail.IntervalFactor = 1;
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = dataEmailInfo.SendingDate.Value.AddMonths(4 + dataEmailInfo.EmailRecurrenceDetail.IntervalFactor.Value);
                        }
                        else if (dataEmailInfo.EmailRecurrenceDetail.RecurrenceIntervalType == Entities.MasterData.Enums.RecurrenceIntervalType.Yearly.ToString())
                        {
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = dataEmailInfo.SendingDate.Value.AddYears(dataEmailInfo.EmailRecurrenceDetail.IntervalFactor.Value);
                        }
                        else
                        {
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = email.DateForCustomRecurrence;
                        }
                    }

                    dataEmailInfo.EmailRecipients = new List<EmailRecipientDetail>();
                    List<string> toList = null;
                    email.To = "testingsingh285@gmail.com";
                    toList = email.To.Split(',').ToList();
                    foreach (var to in toList)
                    {
                        EmailRecipientDetail recipient = new EmailRecipientDetail();
                        recipient.Recipient = to;
                        recipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.To;
                        recipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                        dataEmailInfo.EmailRecipients.Add(recipient);
                    }

                    List<string> ccList = null;
                    if (email.CC != null)
                    {
                        email.CC = "testingsingh285@gmail.com";
                        ccList = email.CC.Split(',').ToList();
                        foreach (var cc in ccList)
                        {
                            EmailRecipientDetail recipient = new EmailRecipientDetail();
                            recipient.Recipient = cc;
                            recipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.CC;
                            recipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                            dataEmailInfo.EmailRecipients.Add(recipient);
                        }
                    }

                    List<string> bccList = null;
                    if (email.BCC != null)
                    {
                        email.BCC = "testingsingh285@gmail.com";
                        bccList = email.BCC.Split(',').ToList();
                        foreach (var bcc in bccList)
                        {
                            EmailRecipientDetail recipient = new EmailRecipientDetail();
                            recipient.Recipient = bcc;
                            recipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.BCC;
                            recipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                            dataEmailInfo.EmailRecipients.Add(recipient);
                        }
                    }

                    await emailServiceManager.SaveComposedEmail(dataEmailInfo);
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.EMAIL_SENT_EXCEPTION;
            }
            return Json(new { status = status, addedEmail = dataEmailInfo }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to stop a followup email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ActionResult> StopFollowUpEmail(int emailInfoID)
        {
            EmailInfo dataEmailInfo = null;
            string status = "true";
            try
            {
                dataEmailInfo = await emailServiceManager.StopFollowUpEmailAsync(emailInfoID);
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.EMAIL_FOLLOW_UP_STOP_EXCEPTION;
            }
            return Json(new { status = status, email = dataEmailInfo }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to stop a followup email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ActionResult> StopFollowUpEmailForSelectReceiversAsync(int emailInfoID, int recipientID)
        {
            EmailInfo dataEmailInfo = null;
            List<int> emailRecipientIDs = new List<int>();
            emailRecipientIDs.Add(recipientID);
            string status = "true";
            try
            {
                dataEmailInfo = await emailServiceManager.StopFollowUpEmailForSelectReceiversAsync(emailInfoID, emailRecipientIDs);
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.EMAIL_FOLLOW_UP_FOR_RECEIVERS_STOP_EXCEPTION;
            }
            return Json(new { status = status, email = dataEmailInfo }, JsonRequestBehavior.AllowGet);
        }

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

        private async Task<string> GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            return user.Id;
        }
    }
}