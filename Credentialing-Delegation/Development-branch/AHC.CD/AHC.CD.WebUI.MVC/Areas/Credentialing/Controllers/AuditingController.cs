using AHC.CD.Business.Credentialing.CnD;
using AHC.CD.Business.Email;
using AHC.CD.Business.PackageGeneration;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Models.EmailService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Business;
using System.IO;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class AuditingController : Controller
    {

        private IPackageGeneratorManager packageManager = null;
        private IErrorLogger errorLogger = null;
        private IEmailServiceManager emailServiceManager = null;

        public AuditingController(IPackageGeneratorManager packageManager, IErrorLogger errorLogger, IEmailServiceManager emailServiceManager)
        {
            this.packageManager = packageManager;
            this.errorLogger = errorLogger;
            this.emailServiceManager = emailServiceManager;
        }

        //
        // GET: /Credentialing/Auditing/
        [Authorize(Roles = "CCO")]
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> SendPackageGeneratorEmail(EmailServiceViewModel email, string doclist)
        {

            string status = "true";
            EmailInfo dataEmailInfo = null;
            if (email.Body != null)
            {
                email.Body = email.Body.Replace("&lt;", "<");
                email.Body = email.Body.Replace("&gt;", ">");
            }
            if (email.IntervalFactor == null)
            {
                email.IntervalFactor = 1;
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
                    //email.To = "testingsingh285@gmail.com";
                    toList = email.To.Split(';').ToList();
                    Dictionary<string, List<string>> Toemailids = await emailServiceManager.CheckGroupMailId(toList);
                    if (Toemailids != null && Toemailids.Count != 0)
                    {
                        foreach (var dictn in Toemailids)
                        {
                            toList.Remove(dictn.Key);
                            toList.AddRange(dictn.Value);
                        }
                    }
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
                            EmailRecipientDetail recipient = new EmailRecipientDetail();
                            recipient.Recipient = bcc;
                            recipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.BCC;
                            recipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                            dataEmailInfo.EmailRecipients.Add(recipient);
                        }
                    }
                    List<EmailAttachment> allAttachments = new List<EmailAttachment>();
                    List<string> docs=new List<string>();
                    docs = JsonConvert.DeserializeObject<List<string>>(doclist);
                    long totalattachmentsSize = 0;
                    foreach (var docpath in docs)
                    {
                        EmailAttachment attachment = new EmailAttachment();
                        attachment.AttachmentRelativePath = docpath;
                        attachment.AttachmentServerPath = HttpContext.Server.MapPath(attachment.AttachmentRelativePath);
                        totalattachmentsSize = totalattachmentsSize + new FileInfo(attachment.AttachmentServerPath).Length;
                        if (totalattachmentsSize > 3000000) 
                        {
                            status = "File Size Exceeded";
                            throw new Exception("File Size Exceeded");
                        }
                        allAttachments.Add(attachment);
                    }
                    allAttachments.AddRange(dataEmailInfo.EmailAttachments);
                    if (allAttachments != null && allAttachments.Count > 0)
                    {
                        dataEmailInfo.EmailAttachments = new List<EmailAttachment>();
                        dataEmailInfo.EmailAttachments = allAttachments;
                    }
                    else
                    {
                        dataEmailInfo.EmailAttachments = allAttachments;
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
                if (status != "File Size Exceeded")
                {
                    status = ExceptionMessage.EMAIL_SENT_EXCEPTION;
                }
            }
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public string GetAllPackageGenerationTracker()
        {
            var trackers = packageManager.GetAllPackageGenerationTracker();

            return JsonConvert.SerializeObject(trackers);
        }
       
    }
}