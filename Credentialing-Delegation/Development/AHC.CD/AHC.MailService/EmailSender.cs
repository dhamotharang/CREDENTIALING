using AHC.CD.Data.Repository;
using AHC.CD.Entities.EmailNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterData.Enums;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.WebSockets;
using System.Globalization;
using AHC.CD.Data.EFRepository;

namespace AHC.MailService
{
    /// <summary>
    /// Author: Venkat
    /// Date: 30/10/2014
    /// Sends email with multiple attachments
    /// Required to config the smtp host and port number in the web.config file
    /// </summary>
    public class EmailSender : AHC.MailService.IEmailSender
    {
        private IUnitOfWork uow = null;
        private IGenericRepository<EmailInfo> emailInfoRepository = null;
        

        public EmailSender()
        {

        }

        public EmailSender(IUnitOfWork uow)
        {
            this.uow = uow;
            emailInfoRepository = uow.GetGenericRepository<EmailInfo>();
            //profileRepository = uow.GetProfileRepository();
        }

        public bool SendMail(EMailModel emailModel)
        {            
            bool isSent = true;
            EmailInfo emailinfo = new EmailInfo();
            emailinfo.EmailRecipients = new List<EmailRecipientDetail>();
            MailMessage mailMessage = new MailMessage();
            if (!string.IsNullOrEmpty(emailModel.From))
            {
                mailMessage.From = new MailAddress(emailModel.From);
            }
            //emailModel.To = "testingsingh285@gmail.com";
            string[] toAddressList = emailModel.To.Split(',');
            foreach (string toAddress in toAddressList)
            {
                mailMessage.To.Add(new MailAddress(toAddress));
                //mailMessage.To.Add(toAddress);
            }
            foreach (var item in toAddressList){
                EmailRecipientDetail recipient = new EmailRecipientDetail();
                recipient.Recipient = item;
                recipient.RecipientTypeCategory = AHC.CD.Entities.MasterData.Enums.RecipientType.To;
                recipient.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                emailinfo.EmailRecipients.Add(recipient);
            }
            if (emailModel.Bcc != null)
            {
                //emailModel.Bcc = "testingsingh285@gmail.com";
                string[] bccAddressList = emailModel.Bcc.Split(',');
                foreach (string bccAddress in bccAddressList)
                {
                    mailMessage.Bcc.Add(new MailAddress(bccAddress));
                    //mailMessage.Bcc.Add(bccAddress);

                }
                 foreach (var item in bccAddressList)
	            {
		            EmailRecipientDetail recipient = new EmailRecipientDetail();
                    recipient.Recipient = item;
                    recipient.RecipientTypeCategory = AHC.CD.Entities.MasterData.Enums.RecipientType.To;
                    recipient.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    emailinfo.EmailRecipients.Add(recipient);
	            }
            }          
                
            
            if (emailModel.Cc != null)
            {
                //emailModel.Cc = "testingsingh285@gmail.com";
                string[] ccAddressList = emailModel.Cc.Split(',');
                foreach (string ccAddress in ccAddressList)
                {
                    mailMessage.Bcc.Add(new MailAddress(ccAddress));
                    //mailMessage.Bcc.Add(ccAddress);
                }
                foreach (var item in ccAddressList)
                {
                    EmailRecipientDetail recipient = new EmailRecipientDetail();
                    recipient.Recipient = item;
                    recipient.RecipientTypeCategory = AHC.CD.Entities.MasterData.Enums.RecipientType.To;
                    recipient.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    emailinfo.EmailRecipients.Add(recipient);
                }

            }

            mailMessage.Subject = emailModel.Subject;
            emailinfo.Subject=mailMessage.Subject;
            
            //mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Body = emailModel.Body;
            emailinfo.Body =  mailMessage.Body;
            //mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            emailinfo.SendingDate = DateTime.Now;

            //if (emailModel.Attachments != null && emailModel.Attachments.Count > 0)
            //{
            //    foreach (var file in emailModel.Attachments)
            //    {
            //        mailMessage.Attachments.Add(new Attachment(file.Item1, file.Item2));
            //    }
                
            //}



            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
            mailMessage.Dispose();
            IUnitOfWork unitofwork = new EFUnitOfWork();
            var repository = unitofwork.GetGenericRepository<EmailInfo>();
            repository.Create(emailinfo);
            repository.Save();
            //smtpClient.SendAsync(mailMessage, isSent);
            //Task.Factory.StartNew(() =>
            //{
            //    smtpClient.Send(mailMessage);
            //    mailMessage.Dispose();
            //});

            return isSent;
        }

        /// <summary>
        /// Method to send emails stacked in the DB
        /// </summary>
        /// <returns></returns>
        public bool SendMail()
        {
            List<EmailInfo> emailStack = emailInfoRepository.Get(e => e.Status != StatusType.Inactive.ToString(), "EmailAttachments, EmailRecipients.EmailTracker, EmailRecurrenceDetail").ToList();

            try
            {
                if (emailStack != null && emailStack.Count != 0)
                {
                    foreach (EmailInfo email in emailStack)
                    {
                        //var b = email.SendingDate.ToString().Split(' ')[0];
                        //b = DateTime.Parse(b).ToString("MM/dd/yyyy");
                        
                        if ((email.SendingDate.HasValue ? (email.SendingDate.Value.Date<=System.DateTime.Now.Date?true:false) : false) && (email.IsRecurrenceEnabled == YesNoOption.YES.ToString() ? ((email.EmailRecurrenceDetail.FromDate.HasValue ? email.EmailRecurrenceDetail.FromDate.Value.Date <= DateTime.Now.Date : true) && (email.EmailRecurrenceDetail.ToDate.HasValue ? email.EmailRecurrenceDetail.ToDate.Value.Date >= DateTime.Now.Date : true)) : true))
                        {
                            string toList = "";
                            string ccList = "";
                            string bccList = "";
                            foreach (EmailRecipientDetail emailRecipient in email.EmailRecipients)
                            {
                                EmailTracker emailTracker = new EmailTracker();
                                if (emailRecipient.Status != StatusType.Inactive.ToString() && (emailRecipient.EmailTracker == null || emailRecipient.EmailTracker.Count == 0))
                                {
                                    if (emailRecipient.RecipientType == RecipientType.To.ToString())
                                    {
                                        toList = toList + emailRecipient.Recipient + ",";
                                    }
                                    else if (emailRecipient.RecipientType == RecipientType.CC.ToString())
                                    {
                                        ccList = ccList + emailRecipient.Recipient + ",";
                                    }
                                    else if (emailRecipient.RecipientType == RecipientType.BCC.ToString())
                                    {
                                        bccList = bccList + emailRecipient.Recipient + ",";
                                    } if (emailRecipient.EmailTracker == null)
                                    {
                                        emailRecipient.EmailTracker = new List<EmailTracker>();
                                    }
                                    emailTracker.EmailStatusTypeCategory = EmailStatusType.Sent;
                                    emailTracker.EmailTrackerDate = DateTime.Now;
                                    emailTracker.StatusType = StatusType.Active;
                                    emailRecipient.EmailTracker.Add(emailTracker);
                                }
                            }

                            if (toList != "")
                            {
                                MailMessage mailMessage = new MailMessage();
                                if (!string.IsNullOrEmpty(email.From))
                                {
                                    mailMessage.From = new MailAddress(email.From);
                                }

                                toList = toList.TrimEnd(',');
                                ccList = ccList.TrimEnd(',');
                                bccList = bccList.TrimEnd(',');

                                string[] toAddressList = toList.Split(',');
                                foreach (string toAddress in toAddressList)
                                {
                                    if (toAddress.Contains('@') && toAddress.Contains('.'))
                                    {
                                        mailMessage.To.Add(new MailAddress(toAddress));
                                    }
                                }

                                if (bccList != "")
                                {
                                    string[] bccAddressList = bccList.Split(',');
                                    foreach (string bccAddress in bccAddressList)
                                    {
                                        if (bccAddress.Contains('@') && bccAddress.Contains('.'))
                                        {
                                            mailMessage.Bcc.Add(new MailAddress(bccAddress));
                                        }
                                    }
                                }

                                if (ccList != "")
                                {
                                    string[] ccAddressList = ccList.Split(',');
                                    foreach (string ccAddress in ccAddressList)
                                    {
                                        if (ccAddress.Contains('@') && ccAddress.Contains('.'))
                                        {
                                            mailMessage.CC.Add(new MailAddress(ccAddress));
                                        }
                                    }
                                }

                                mailMessage.Subject = email.Subject;
                                mailMessage.Body = email.Body;
                                mailMessage.Body = mailMessage.Body.Replace("[Provider Name]", toList);                                    
                                mailMessage.IsBodyHtml = true;

                                if (email.EmailAttachments != null && email.EmailAttachments.Count != 0)
                                {
                                    foreach (EmailAttachment attachment in email.EmailAttachments)
                                    {
                                        Attachment att = new Attachment(attachment.AttachmentServerPath);
                                        mailMessage.Attachments.Add(att);
                                    }
                                }

                                SmtpClient smtpClient = new SmtpClient();
                                smtpClient.Timeout = 2000000;
                                try
                                {
                                    smtpClient.Send(mailMessage);
                                }
                                catch (System.Net.Mail.SmtpFailedRecipientsException ex)
                                {
                                    EmailTracker failureEmailTracker = new EmailTracker();
                                    foreach (var emailRecipient in email.EmailRecipients)
                                    {
                                        if (emailRecipient.Recipient.Equals(ex.FailedRecipient.TrimStart('<').TrimEnd('>')))
                                        {
                                            if (emailRecipient.EmailTracker == null)
                                            {
                                                emailRecipient.EmailTracker = new List<EmailTracker>();
                                            }
                                            failureEmailTracker.EmailStatusTypeCategory = EmailStatusType.SendingFailed;
                                            failureEmailTracker.EmailTrackerDate = DateTime.Now;
                                            failureEmailTracker.StatusType = StatusType.Active;
                                            emailRecipient.EmailTracker.Add(failureEmailTracker);
                                        }
                                    }
                                }
                                catch (System.Net.Mail.SmtpException ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    mailMessage.Dispose();
                                }
                            }
                            emailInfoRepository.Update(email);
                            emailInfoRepository.Save();
                            if (email.IsRecurrenceEnabled == YesNoOption.YES.ToString())
                            {
                                if (email.EmailRecurrenceDetail.IsRecurrenceScheduled != YesNoOption.YES.ToString())
                                {
                                    AddEmailForNextMailingDate(email.EmailInfoID);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// Private method to add an existing email with the next mailing date.
        /// </summary>
        /// <param name="emailInfoID"></param>
        private void AddEmailForNextMailingDate(int emailInfoID)
        {
            try
            {
                EmailInfo email = emailInfoRepository.Find(e => e.EmailInfoID == emailInfoID, "EmailRecurrenceDetail, EmailRecipients");
                EmailInfo newEmailInfo = new EmailInfo();

                newEmailInfo.Body = email.Body;
                newEmailInfo.EmailNotificationTypeCategory = email.EmailNotificationTypeCategory;
                newEmailInfo.From = email.From;
                newEmailInfo.IsRecurrenceEnabledYesNoOption = email.IsRecurrenceEnabledYesNoOption;
                newEmailInfo.SendingDate = email.SendingDate;
                newEmailInfo.StatusType = email.StatusType;
                newEmailInfo.Subject = email.Subject;

                newEmailInfo.EmailRecurrenceDetail = new EmailRecurrenceDetail();
                //newEmailInfo.EmailRecurrenceDetail.CustomDaysInWeeks = email.EmailRecurrenceDetail.CustomDaysInWeeks;
                newEmailInfo.EmailRecurrenceDetail.IntervalFactor = email.EmailRecurrenceDetail.IntervalFactor;
                newEmailInfo.EmailRecurrenceDetail.NextMailingDate = email.EmailRecurrenceDetail.NextMailingDate;
                newEmailInfo.EmailRecurrenceDetail.RecurrenceIntervalTypeCategory = email.EmailRecurrenceDetail.RecurrenceIntervalTypeCategory;
                newEmailInfo.EmailRecurrenceDetail.StatusType = email.EmailRecurrenceDetail.StatusType;

                newEmailInfo.EmailRecipients = new List<EmailRecipientDetail>();
                foreach (EmailRecipientDetail recipient in email.EmailRecipients)
                {
                    EmailRecipientDetail newRecipient = new EmailRecipientDetail();
                    newRecipient.Recipient = recipient.Recipient;
                    newRecipient.RecipientTypeCategory = recipient.RecipientTypeCategory;
                    newRecipient.StatusType = recipient.StatusType;
                    newEmailInfo.EmailRecipients.Add(newRecipient);
                }

                newEmailInfo.SendingDate = email.EmailRecurrenceDetail.NextMailingDate;
                if (email.EmailRecurrenceDetail.NextMailingDate != null)
                {
                    if (email.EmailRecurrenceDetail.RecurrenceIntervalType == RecurrenceIntervalType.Daily.ToString())
                    {
                        newEmailInfo.EmailRecurrenceDetail.NextMailingDate = email.EmailRecurrenceDetail.NextMailingDate.Value.AddDays(email.EmailRecurrenceDetail.IntervalFactor.Value);
                    }
                    else if (email.EmailRecurrenceDetail.RecurrenceIntervalType == RecurrenceIntervalType.Weekly.ToString())
                    {
                        newEmailInfo.EmailRecurrenceDetail.NextMailingDate = email.EmailRecurrenceDetail.NextMailingDate.Value.AddDays(7 * email.EmailRecurrenceDetail.IntervalFactor.Value);
                    }
                    else if (email.EmailRecurrenceDetail.RecurrenceIntervalType == RecurrenceIntervalType.Monthly.ToString())
                    {
                        newEmailInfo.EmailRecurrenceDetail.NextMailingDate = email.EmailRecurrenceDetail.NextMailingDate.Value.AddMonths(email.EmailRecurrenceDetail.IntervalFactor.Value);
                    }
                    else if (email.EmailRecurrenceDetail.RecurrenceIntervalType == RecurrenceIntervalType.Quarterly.ToString())
                    {
                        newEmailInfo.EmailRecurrenceDetail.NextMailingDate = email.EmailRecurrenceDetail.NextMailingDate.Value.AddMonths(DateTime.Now.Month + 4);
                    }
                    else if (email.EmailRecurrenceDetail.RecurrenceIntervalType == RecurrenceIntervalType.Yearly.ToString())
                    {
                        newEmailInfo.EmailRecurrenceDetail.NextMailingDate = email.EmailRecurrenceDetail.NextMailingDate.Value.AddYears(email.EmailRecurrenceDetail.IntervalFactor.Value);
                    }
                    emailInfoRepository.Create(newEmailInfo);
                }
                email.EmailRecurrenceDetail.IsRecurrenceScheduledYesNoOption = YesNoOption.YES;
                emailInfoRepository.Update(email);

                emailInfoRepository.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void ReceiveMails()
        //{

        //    Imap client = new Imap();
        //    // connect to server

        //    client.Connect("imap.gmail.com", 993, SslMode.Implicit);

        //    // authenticate
        //    client.Login("username", "password");

        //    // select folder
        //    client.SelectFolder("Inbox");

        //    int NoOfEmailsPerPage = 10;
        //    int totalEmails = client.CurrentFolder.TotalMessageCount;
        //    // get message list - envelope headers
        //    ImapMessageCollection messages = client.GetMessageList(ImapListFields.Envelope);

        //    // display info about each message


        //    foreach (ImapMessageInfo message in messages)
        //    {

        //        TableCell noCell = new TableCell();

        //        noCell.CssClass = "emails-table-cell";

        //        noCell.Text = Convert.ToString(message.To);
        //        TableCell fromCell = new TableCell();
        //        fromCell.CssClass = "emails-table-cell";
        //        fromCell.Text = Convert.ToString(message.From);
        //        TableCell subjectCell = new TableCell();
        //        subjectCell.CssClass = "emails-table-cell";
        //        subjectCell.Style["width"] = "300px";
        //        subjectCell.Text = Convert.ToString(message.Subject);
        //        TableCell dateCell = new TableCell();
        //        dateCell.CssClass = "emails-table-cell";
        //        if (message.Date.OriginalTime != DateTime.MinValue)
        //            dateCell.Text = message.Date.OriginalTime.ToString();
        //        TableRow emailRow = new TableRow();
        //        emailRow.Cells.Add(noCell);
        //        emailRow.Cells.Add(fromCell);
        //        emailRow.Cells.Add(subjectCell);
        //        emailRow.Cells.Add(dateCell);
        //        EmailsTable.Rows.AddAt(2 + 0, emailRow);

        //    }
        //    int totalPages;
        //    int mod = totalEmails % NoOfEmailsPerPage;
        //    if (mod == 0)
        //        totalPages = totalEmails / NoOfEmailsPerPage;
        //    else
        //        totalPages = ((totalEmails - mod) / NoOfEmailsPerPage) + 1;



        //}
    }
}
