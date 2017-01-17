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

        public async Task<JsonResult> GetAllEmailIds()
        {
            try
            {
                var emails = await emailServiceManager.GetAllEmails();
                return Json(emails, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = "ADM,CCO")]
        public ActionResult Index()
        {
            //ViewBag.Emails = JsonConvert.SerializeObject(emailServiceManager.GetAllEmails());
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

        public async Task<ActionResult> AddGroupEmail(EmailGroupViewModel groupemail)
        {
            string status = "true";
            EmailGroup datagroupemail = new EmailGroup();
            try
            {
                if (ModelState.IsValid)
                {
                    if (groupemail.EmailIds != "")
                    {
                        groupemail.Emailiddetails = JsonConvert.DeserializeObject<List<string>>(groupemail.EmailIds);
                    }
                    datagroupemail = AutoMapper.Mapper.Map<EmailGroupViewModel, EmailGroup>(groupemail);
                    datagroupemail = await emailServiceManager.AddNewGroupEmail(datagroupemail, groupemail.Emailiddetails);
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return Json(new { status = status, datagroupemail = datagroupemail }, JsonRequestBehavior.AllowGet);
        }

        public async Task<string> GetAllGroupMails()
        {
            try
            {
                var result = await emailServiceManager.GetAllGroupMailIdsAsync();
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to stack an email to the DB
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddEmail(EmailServiceViewModel email)
        {
            if(email.IntervalFactor==null)
            {
                email.IntervalFactor = 1;
            }
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
                    dataEmailInfo.From = System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"];
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
                           // dataEmailInfo.EmailRecurrenceDetail.IntervalFactor = 1;
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = dataEmailInfo.SendingDate.Value.AddMonths(4 * dataEmailInfo.EmailRecurrenceDetail.IntervalFactor.Value);
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
                    //email.To = "testingsingh285@gmail.com";
                    toList = email.To.Split(';').ToList();
                    Dictionary<string, List<string>> Toemailids = await emailServiceManager.CheckGroupMailId(toList);
                    if(Toemailids != null && Toemailids.Count != 0)
                    {
                        foreach (var dictn in Toemailids)
                        {
                            toList.Remove(dictn.Key);
                            toList.AddRange(dictn.Value);
                        }
                    }
                    foreach (var to in toList)
                    {
                        if (to != "")
                        {
                            EmailRecipientDetail recipient = new EmailRecipientDetail();
                            recipient.Recipient = to;
                            recipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.To;
                            recipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                            dataEmailInfo.EmailRecipients.Add(recipient);
                        }
                    }

                    List<string> ccList = null;
                    if (email.CC != null)
                    {
                        //email.CC = "testingsingh285@gmail.com";
                        ccList = email.CC.Split(';').ToList();
                        Dictionary<string, List<string>> CCemailids = await emailServiceManager.CheckGroupMailId(ccList);
                        if (CCemailids != null && CCemailids.Count != 0)
                        {
                            foreach (var dictn in CCemailids)
                            {
                                ccList.Remove(dictn.Key);
                                ccList.AddRange(dictn.Value);
                            }
                        }
                        foreach (var cc in ccList)
                        {
                            if (cc != "")
                            {
                                EmailRecipientDetail recipient = new EmailRecipientDetail();
                                recipient.Recipient = cc;
                                recipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.CC;
                                recipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                                dataEmailInfo.EmailRecipients.Add(recipient);
                            }
                        }
                    }

                    List<string> bccList = null;
                    if (email.BCC != null)
                    {
                        //email.BCC = "testingsingh285@gmail.com";
                        bccList = email.BCC.Split(';').ToList();
                        Dictionary<string, List<string>> BCCemailids = await emailServiceManager.CheckGroupMailId(bccList);
                        if (BCCemailids != null && BCCemailids.Count != 0)
                        {
                            foreach (var dictn in BCCemailids)
                            {
                                bccList.Remove(dictn.Key);
                                bccList.AddRange(dictn.Value);
                            }
                        }
                        foreach (var bcc in bccList)
                        {
                            if (bcc != "")
                            {
                                EmailRecipientDetail recipient = new EmailRecipientDetail();
                                recipient.Recipient = bcc;
                                recipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.BCC;
                                recipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                                dataEmailInfo.EmailRecipients.Add(recipient);
                            }
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
            catch (Exception)
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