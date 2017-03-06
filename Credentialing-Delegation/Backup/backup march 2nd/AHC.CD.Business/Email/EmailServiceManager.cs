﻿using AHC.CD.Business.BusinessModels.EmailGroup;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Search;
using AHC.CD.Data.ADO.AspnetUser;
using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Resources.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AHC.CD.Business.Email
{
    public class EmailServiceManager : IEmailServiceManager
    {
        private IUnitOfWork uow = null;
        private IGenericRepository<EmailInfo> emailInfoRepository = null;
        private readonly IDocumentsManager iDocumentsManager = null;
        private IUserDetails aspnetusersRepository = null;
        public EmailServiceManager(IUnitOfWork uow, IDocumentsManager iDocumentsManager)
        {
            this.uow = uow;
            this.iDocumentsManager = iDocumentsManager;
            this.aspnetusersRepository = new UserDetails();
            emailInfoRepository = uow.GetGenericRepository<EmailInfo>();
        }

        /// <summary>
        /// To get Emails
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>

        public async Task<object> GetAllEmailInfo()
        {
            try
            {
                var emailInfoList = new List<EmailInfo>();
                var emailInfoRepo = uow.GetGenericRepository<EmailInfo>();
                var emailInfo = await emailInfoRepo.GetAsync(e => e.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), "EmailRecipients.EmailTracker, EmailAttachments");

                foreach (var item in emailInfo)
                {
                    if (item.EmailRecipients != null && item.SendingDate.Value.Date <= System.DateTime.Now.Date)
                    {
                        foreach (var recipient in item.EmailRecipients)
                        {
                            if (recipient.EmailTracker != null)
                            {
                                emailInfoList.Add(item);
                                break;
                            }
                        }
                    }
                    else
                    {
                        var a = item.SendingDate.Value.Date;
                    }
                }

                return emailInfoList.OrderByDescending(e => e.SendingDate).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to fetch all active emails with recurrence
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EmailInfo>> GetAllActiveFollowUpEmailsAsync()
        {
            var emailServiceRepo = uow.GetGenericRepository<EmailInfo>();
            List<EmailInfo> followUpEmails = new List<EmailInfo>();
            try
            {
                IEnumerable<EmailInfo> activeEmails = await emailServiceRepo.GetAsync(e => e.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString() && e.IsRecurrenceEnabled == Entities.MasterData.Enums.YesNoOption.YES.ToString(), "EmailRecipients.EmailTracker, EmailRecurrenceDetail,EmailAttachments");
                foreach (EmailInfo followUpEmail in activeEmails)
                {
                    if (followUpEmail.SendingDate.HasValue ? followUpEmail.SendingDate.Value.Date > DateTime.Now.Date : false)
                    {
                        followUpEmails.Add(followUpEmail);
                    }
                    //var a = followUpEmail.SendingDate.HasValue;
                    //var b = followUpEmail.EmailRecurrenceDetail != null ? (followUpEmail.EmailRecurrenceDetail.NextMailingDate.HasValue ? followUpEmail.EmailRecurrenceDetail.NextMailingDate.Value >= DateTime.Now.Date : false) : false;
                    //var c = followUpEmail.EmailRecurrenceDetail != null ? (followUpEmail.EmailRecurrenceDetail.ToDate.HasValue ? followUpEmail.EmailRecurrenceDetail.ToDate.Value >= DateTime.Now.Date : true) : false;
                    //var d = followUpEmail.EmailRecurrenceDetail != null ? (followUpEmail.EmailRecurrenceDetail.IsRecurrenceScheduled == Entities.MasterData.Enums.YesNoOption.YES.ToString() ? true : false) : false;
                    //if (followUpEmail.SendingDate.HasValue && (followUpEmail.EmailRecurrenceDetail != null ? (followUpEmail.EmailRecurrenceDetail.NextMailingDate.HasValue ? followUpEmail.EmailRecurrenceDetail.NextMailingDate.Value >= DateTime.Now.Date : false) : false) && (followUpEmail.EmailRecurrenceDetail != null ? (followUpEmail.EmailRecurrenceDetail.ToDate.HasValue ? followUpEmail.EmailRecurrenceDetail.ToDate.Value >= DateTime.Now.Date : true) : false) && (followUpEmail.EmailRecurrenceDetail != null ? (followUpEmail.EmailRecurrenceDetail.IsRecurrenceScheduled != Entities.MasterData.Enums.YesNoOption.YES.ToString()) : false))
                    //{

                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            followUpEmails = followUpEmails.OrderBy(e => e.EmailRecurrenceDetail.NextMailingDate).ToList();
            return followUpEmails.OrderBy(e => e.EmailRecurrenceDetail.NextMailingDate).ToList();
        }

        public async Task<IEnumerable<EmailInfo>> GetAllInboxEmailsAsync(string authID)
        {
            int? profileID = GetUserId(authID);
            var emailServiceRepo = uow.GetGenericRepository<EmailInfo>();
            List<EmailInfo> recievedEmails = new List<EmailInfo>();
            try
            {
                IEnumerable<EmailInfo> activeEmails = await emailServiceRepo.GetAsync(e => e.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString(), "EmailRecipients.EmailTracker");
                foreach (EmailInfo email in activeEmails)
                {
                    foreach (EmailRecipientDetail recipient in email.EmailRecipients)
                    {
                        if (recipient.ProfileID == profileID && ((recipient.EmailTracker != null) && (recipient.EmailTracker.Count != 0)) ? (recipient.EmailTracker.Any(t => t.EmailStatusType == Entities.MasterData.Enums.EmailStatusType.SendingFailed.ToString()) ? false : true) : false)
                        {
                            recievedEmails.Add(email);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recievedEmails;
        }

        /// <summary>
        /// Method to save Composed Email so that it can be picked up by the scheduler
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public EmailInfo SaveComposedEmail(EmailInfo email)
        {
            var emailInfoRepo = uow.GetGenericRepository<EmailInfo>();
            try
            {
                foreach (EmailRecipientDetail recipient in email.EmailRecipients)
                {
                    recipient.ProfileID = GetProfileID(recipient.Recipient);
                }
                //email = emailInfoRepository.Find(e => e.EmailInfoID == email.EmailInfoID, "EmailAttachments, EmailRecipients.EmailTracker, EmailRecurrenceDetail");
                if (email != null)
                {
                    InstantMailSending(email);
                }
                //emailInfoRepo.Create(email);
                //await emailInfoRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return email;
        }

        /// <summary>
        /// For Instant Mail Send
        /// </summary>
        /// <param name="email"></param>
        private void InstantMailSending(EmailInfo email)
        {
            try
            {
                if ((email.SendingDate.HasValue ? true : false) && (email.IsRecurrenceEnabled == YesNoOption.YES.ToString() ? ((email.EmailRecurrenceDetail.FromDate.HasValue ? email.EmailRecurrenceDetail.FromDate.Value.Date >= DateTime.Now.Date : true) && (email.EmailRecurrenceDetail.ToDate.HasValue ? email.EmailRecurrenceDetail.ToDate.Value.Date >= DateTime.Now.Date : true)) : true))
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

                        var profiles = GetallProfiles();
                        var contactdetails = GetallContactDetails();
                        int contactdetailId = 0;
                        foreach (var item in contactdetails)
                        {
                            foreach (var emailid in item.EmailIDs)
                            {
                                if (emailid.EmailAddress == toAddressList[0])
                                {
                                    contactdetailId = item.ContactDetailID;
                                    break;
                                }
                            }
                        }
                        var res1 = profiles.Where(x => x.ContactDetail.ContactDetailID == contactdetailId).FirstOrDefault();
                        mailMessage.Body = email.Body;
                        int length = toAddressList.Length;
                        foreach (string toAddress in toAddressList)
                        {
                            if (length == 1)
                            {
                                if (toAddress.Contains('@') && toAddress.Contains('.'))
                                {
                                    if (res1 != null)
                                    {
                                        mailMessage.Body = mailMessage.Body.Replace("[Provider Name]", res1.PersonalDetail.Salutation + " " + res1.PersonalDetail.FirstName + res1.PersonalDetail.MiddleName + " " + res1.PersonalDetail.LastName);
                                    }
                                    else
                                    {
                                        string name = toAddress.Split('@')[0];
                                        mailMessage.Body = mailMessage.Body.Replace("[Provider Name]", name);
                                    }
                                    mailMessage.To.Add(new MailAddress(toAddress));
                                }
                            }
                            else
                            {
                                if (toAddress.Contains('@') && toAddress.Contains('.'))
                                {
                                    mailMessage.Body = mailMessage.Body.Replace("[Provider Name]", "All");
                                    mailMessage.To.Add(new MailAddress(toAddress));
                                }
                            }
                        }
                        email.Body = mailMessage.Body;
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

                        //mailMessage.Body = email.Body;
                        StringBuilder body = new StringBuilder(mailMessage.Body);
                        //body.Append("<br/><br/><br/><b>This is a system generated email, please do not respond. Send all inquiries directly to credentialing@accesshealthcarellc.net");
                        //body.Append("<br/><br/><b>This is a system generated email, please do not respond. Send all inquiries directly to credentialing@credaxis.com");
                        //body.Append(" and for call inquiries, our Credentialing Specialists are available to assist with all questions Monday- Friday 8a.m.-5PM and you may contact us via telephone at 352-796-9994.</b>");
                        body.Append("<br/><br/><b>");
                        body.Append(System.Configuration.ConfigurationManager.AppSettings["DisclaimerMessage"]);
                        body.Append("</b>");
                        //body.Append("<b>This is a System Generated mail, Please don't respond to this..</b>");
                        mailMessage.Body = body.ToString();
                        email.Body = body.ToString();


                        //mailMessage.Body = mailMessage.Body.Replace("[Provider Name]", toList);
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
                    emailInfoRepository.Create(email);
                    //emailInfoRepository.Update(email);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AddEmailForNextMailingDate(int emailInfoID)
        {
            try
            {
                EmailInfo email = emailInfoRepository.Find(e => e.EmailInfoID == emailInfoID, "EmailRecurrenceDetail, EmailRecipients,EmailAttachments");
                EmailInfo newEmailInfo = new EmailInfo();

                newEmailInfo.Body = email.Body;
                newEmailInfo.EmailNotificationTypeCategory = email.EmailNotificationTypeCategory;
                newEmailInfo.From = email.From;
                newEmailInfo.IsRecurrenceEnabledYesNoOption = email.IsRecurrenceEnabledYesNoOption;
                newEmailInfo.SendingDate = email.SendingDate;
                newEmailInfo.StatusType = email.StatusType;
                newEmailInfo.Subject = email.Subject;
                newEmailInfo.EmailAttachments = new List<EmailAttachment>();

                foreach (EmailAttachment EmailAttachments in email.EmailAttachments)
                {
                    EmailAttachment EA = new EmailAttachment();
                    EA.AttachmentRelativePath = EmailAttachments.AttachmentRelativePath;
                    EA.AttachmentServerPath = EmailAttachments.AttachmentServerPath;
                    EA.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    newEmailInfo.EmailAttachments.Add(EA);
                }
                newEmailInfo.EmailRecurrenceDetail = new EmailRecurrenceDetail();
                //newEmailInfo.EmailRecurrenceDetail.CustomDaysInWeeks = email.EmailRecurrenceDetail.CustomDaysInWeeks;
                newEmailInfo.EmailRecurrenceDetail.IntervalFactor = email.EmailRecurrenceDetail.IntervalFactor;
                newEmailInfo.EmailRecurrenceDetail.NextMailingDate = email.EmailRecurrenceDetail.NextMailingDate;
                newEmailInfo.EmailRecurrenceDetail.RecurrenceIntervalTypeCategory = email.EmailRecurrenceDetail.RecurrenceIntervalTypeCategory;
                newEmailInfo.EmailRecurrenceDetail.StatusType = email.EmailRecurrenceDetail.StatusType;
                newEmailInfo.EmailRecurrenceDetail.IsRecurrenceScheduledYesNoOption = YesNoOption.YES;
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
                email.EmailRecurrenceDetail.IsRecurrenceScheduledYesNoOption = YesNoOption.NO;
                emailInfoRepository.Update(email);

                emailInfoRepository.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region GroupEmail

        /// <summary>
        /// Method to Add a new Group Email and OtherUser.
        /// By Tulasidhar
        /// </summary>
        /// <param name="groupemail"></param>
        /// <param name="Dictionary"></param>
        /// <returns></returns>
        public async Task<object> AddNewGroupEmail(EmailGroup groupemail, Dictionary<string, string> Dictionary, string AuthId)
        {
            try
            {
                List<object> OtherUsers = new List<object>();
                var GroupMailRepo = uow.GetGenericRepository<EmailGroup>();
                var OtherUserRepo = uow.GetGenericRepository<OtherUser>();
                var cduserrepo = uow.GetGenericRepository<CDUser>();
                var cduser = cduserrepo.Find(x => x.AuthenicateUserId == AuthId);
                groupemail.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                groupemail.CreatedBy = cduser.CDUserID;
                groupemail.LastUpdatedBy = cduser.CDUserID;
                groupemail.CDUserGroupMails = new List<CDUser_GroupEmail>();
                GroupMailRepo.Create(groupemail);
                await GroupMailRepo.SaveAsync();
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (var item in Dictionary)
                {
                    CDUser_GroupEmail cdusergrpmail = new CDUser_GroupEmail { LastUpdatedBy = cduser.CDUserID };
                    if (Regex.IsMatch(item.Key, @"[a-zA-Z]"))
                    {
                        OtherUser user = new OtherUser();
                        user.EmailId = item.Value;
                        user.FullName = item.Key;
                        OtherUserRepo.Create(user);
                        await OtherUserRepo.SaveAsync();
                        cdusergrpmail.CDUserId = user.CDUserID;
                        cdusergrpmail.EmailGroupID = groupemail.EmailGroupID;
                        cdusergrpmail.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                        groupemail.CDUserGroupMails.Add(cdusergrpmail);
                        dict.Add(user.CDUserID.ToString(), item.Value);
                        OtherUsers.Add(new { CDuserId = user.CDUserID, FullName = user.FullName, EmailId = user.EmailId });
                    }
                    else
                    {
                        cdusergrpmail.CDUserId = int.Parse(item.Key);
                        cdusergrpmail.EmailGroupID = groupemail.EmailGroupID;
                        cdusergrpmail.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                        groupemail.CDUserGroupMails.Add(cdusergrpmail);
                        dict.Add(item.Key, item.Value);
                    }
                }
                GroupMailRepo.Update(groupemail);
                await GroupMailRepo.SaveAsync();
                object result = new { OtherUsers = OtherUsers, EmailGroupId = groupemail.EmailGroupID, Emails = dict, CreatedBy = cduser };
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<object> UpdateGroupMailasync(EmailGroup groupMail, Dictionary<string, string> Dictionary, string AuthId)
        {
            try
            {
                List<object> OtherUsers = new List<object>();
                var GroupMailRepo = uow.GetGenericRepository<EmailGroup>();
                var Cduser_GroupRepo = uow.GetGenericRepository<CDUser_GroupEmail>();
                var OtherUserRepo = uow.GetGenericRepository<OtherUser>();
                var cduserId = GetCDUserIdFromAuthId(AuthId);
                var existingGrpMail = GroupMailRepo.Find(x => x.EmailGroupID == groupMail.EmailGroupID);
                existingGrpMail.EmailGroupName = groupMail.EmailGroupName;
                existingGrpMail.LastModifiedDate = DateTime.Now;
                existingGrpMail.Description = groupMail.Description;
                existingGrpMail.LastUpdatedBy = cduserId;
                GroupMailRepo.Update(existingGrpMail);
                await GroupMailRepo.SaveAsync();
                var res = Cduser_GroupRepo.Get(x => x.EmailGroupID == groupMail.EmailGroupID, "CDUser").ToList();
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (var item in Dictionary)
                {
                    if (Regex.IsMatch(item.Key, @"[a-zA-Z]") || !(res.Any(x => x.CDUserId == int.Parse(item.Key))))
                    {
                        CDUser_GroupEmail cdusergrpmail = new CDUser_GroupEmail { LastUpdatedBy = cduserId };
                        if (Regex.IsMatch(item.Key, @"[a-zA-Z]"))
                        {
                            OtherUser user = new OtherUser();
                            user.EmailId = item.Value;
                            user.FullName = item.Key;
                            user.StatusType = StatusType.Active;
                            OtherUserRepo.Create(user);
                            await OtherUserRepo.SaveAsync();
                            cdusergrpmail.CDUserId = user.CDUserID;
                            cdusergrpmail.EmailGroupID = groupMail.EmailGroupID;
                            cdusergrpmail.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                            Cduser_GroupRepo.Create(cdusergrpmail);
                            await Cduser_GroupRepo.SaveAsync();
                            dict.Add(item.Key, item.Value);
                            OtherUsers.Add(new { CDuserId = user.CDUserID, FullName = user.FullName, EmailId = user.EmailId });
                        }
                        else
                        {
                            cdusergrpmail.CDUserId = int.Parse(item.Key);
                            cdusergrpmail.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                            cdusergrpmail.EmailGroupID = groupMail.EmailGroupID;
                            Cduser_GroupRepo.Create(cdusergrpmail);
                            await Cduser_GroupRepo.SaveAsync();
                            dict.Add(item.Key, item.Value);
                        }
                    }
                    else
                    {
                        dict.Add(item.Key, res.Find(x => x.CDUser.CDUserID == int.Parse(item.Key)).CDUser.EmailId);
                        res.RemoveAt(res.FindIndex(x => x.CDUserId == int.Parse(item.Key)));
                    }
                }
                foreach (var item in res)
                {
                    item.LastUpdatedBy = cduserId;
                    item.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                    Cduser_GroupRepo.Update(item);
                }
                await Cduser_GroupRepo.SaveAsync();
                object result = new { OtherUsers = OtherUsers, Emails = dict };
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        ///  Update for Group Mail -- Tulasidhar
        /// </summary>
        /// <param name="groupMail"></param>
        /// <param name="Dictionary"></param>
        /// <returns></returns>
        public async Task<object> UpdateGroupMailasyncUnUsed(EmailGroup groupMail, Dictionary<string, string> Dictionary, string AuthId)
        {
            try
            {
                List<object> OtherUsers = new List<object>();
                var GroupMailRepo = uow.GetGenericRepository<EmailGroup>();
                var OtherUserRepo = uow.GetGenericRepository<OtherUser>();
                var cduserId = GetCDUserIdFromAuthId(AuthId);
                var existingGrpMail = GroupMailRepo.Find(x => x.EmailGroupID == groupMail.EmailGroupID, "CDUserGroupMails.CDUser");
                existingGrpMail.EmailGroupName = groupMail.EmailGroupName;
                existingGrpMail.LastModifiedDate = DateTime.Now;
                existingGrpMail.Description = groupMail.Description;
                existingGrpMail.LastUpdatedBy = cduserId;
                if (existingGrpMail.CDUserGroupMails == null) { existingGrpMail.CDUserGroupMails = new List<CDUser_GroupEmail>(); }
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (var item in Dictionary)
                {
                    if (!existingGrpMail.CDUserGroupMails.Any(x => x.CDUserId == int.Parse(item.Key)))
                    {
                        CDUser_GroupEmail cdusergrpmail = new CDUser_GroupEmail { LastUpdatedBy = cduserId };
                        if (Regex.IsMatch(item.Key, @"[a-zA-Z]"))
                        {
                            OtherUser user = new OtherUser();
                            user.EmailId = item.Value;
                            user.FullName = item.Key;
                            OtherUserRepo.Create(user);
                            await OtherUserRepo.SaveAsync();
                            cdusergrpmail.CDUserId = user.CDUserID;
                            cdusergrpmail.EmailGroupID = groupMail.EmailGroupID;
                            cdusergrpmail.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                            existingGrpMail.CDUserGroupMails.Add(cdusergrpmail);
                            dict.Add(user.CDUserID.ToString(), item.Value);
                            OtherUsers.Add(new { CDuserId = user.CDUserID, FullName = user.FullName, EmailId = user.EmailId });
                        }
                        else
                        {
                            cdusergrpmail.CDUserId = int.Parse(item.Key);
                            cdusergrpmail.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                            cdusergrpmail.EmailGroupID = groupMail.EmailGroupID;
                            existingGrpMail.CDUserGroupMails.Add(cdusergrpmail);
                            dict.Add(item.Key, item.Value);
                        }
                    }
                    else
                    {
                        dict.Add(item.Key, existingGrpMail.CDUserGroupMails.Find(x => x.CDUser.CDUserID == int.Parse(item.Key)).CDUser.EmailId);
                        existingGrpMail.CDUserGroupMails.RemoveAt(existingGrpMail.CDUserGroupMails.FindIndex(x => x.CDUser.CDUserID == int.Parse(item.Key)));
                        existingGrpMail.CDUserGroupMails.Where(x => x.CDUserId == int.Parse(item.Key)).FirstOrDefault().LastUpdatedBy = cduserId;
                        existingGrpMail.CDUserGroupMails.Where(x => x.CDUserId == int.Parse(item.Key)).FirstOrDefault().StatusType = StatusType.Inactive;
                    }
                }
                GroupMailRepo.Update(existingGrpMail);
                await GroupMailRepo.SaveAsync();
                object result = new { OtherUsers = OtherUsers, Emails = dict };
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Remove Individual Mail from a group instead of bulky update -- Tulasidhar
        /// </summary>
        /// <param name="Cduser_GroupMailId"></param>
        /// <returns></returns>
        public async Task<string> RemoveIndividualMailFromGroupAsync(int Cduser_GroupMailId)
        {
            try
            {
                string status = "";
                var Cduser_GroupRepo = uow.GetGenericRepository<CDUser_GroupEmail>();
                var result = Cduser_GroupRepo.Find(x => x.CDUserGroupEmailID == Cduser_GroupMailId);
                if (result != null)
                {
                    result.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                    Cduser_GroupRepo.Update(result);
                    await Cduser_GroupRepo.SaveAsync();
                    status = "true";
                }
                return status;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Method to Inactivate a Group Email -- Tulasidhar
        /// </summary>
        /// <param name="EmailGroupId"></param>
        /// <returns></returns>
        public async Task<string> InactivateEmailGroupAsync(int EmailGroupId, string AuthId)
        {
            try
            {
                string status = "";
                int cduserId = GetCDUserIdFromAuthId(AuthId);
                var GroupMailRepo = uow.GetGenericRepository<EmailGroup>();
                var result = GroupMailRepo.Find(x => x.EmailGroupID == EmailGroupId);
                if (result != null)
                {
                    result.LastUpdatedBy = cduserId;
                    result.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                    GroupMailRepo.Update(result);
                    await GroupMailRepo.SaveAsync();
                    status = "true";
                }
                return status;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<string> GetAllEmailsForaGroup(int EmailGroupId)
        {
            try
            {

                List<string> emails = new List<string>();
                var CduserGrpMailRepo = uow.GetGenericRepository<CDUser_GroupEmail>();
                var res = CduserGrpMailRepo.GetAll("CDUser").Where(f => f.EmailGroupID == EmailGroupId).ToList();
                foreach (var user in res)
                {
                    if (user.CDUser.AuthenicateUserId != null)
                    {
                        var userDetails = aspnetusersRepository.GetUserDetailsByUserID(user.CDUser.AuthenicateUserId);
                        emails.Add(userDetails.Email);
                    }
                    else
                    {
                        emails.Add(user.CDUser.EmailId);
                    }
                }


                return emails;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> ActivateEmailGroupAsync(int EmailGroupId, string AuthId)
        {
            try
            {
                string status = "";
                int cduserId = GetCDUserIdFromAuthId(AuthId);
                var GroupMailRepo = uow.GetGenericRepository<EmailGroup>();
                var result = GroupMailRepo.Find(x => x.EmailGroupID == EmailGroupId);
                if (result != null)
                {
                    result.LastUpdatedBy = cduserId;
                    result.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    GroupMailRepo.Update(result);
                    await GroupMailRepo.SaveAsync();
                    status = "true";
                }
                return status;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Method to get All GroupMailIds.
        /// By Tulasidhar
        /// </summary>
        /// <returns></returns>
        //public async Task<IEnumerable<EmailGroup>> GetAllGroupMailIdsAsync()
        //{
        //    try
        //    {
        //        var groupemailRepo = uow.GetGenericRepository<EmailGroup>();
        //        var groupmailIds = await groupemailRepo.GetAllAsync();
        //        return groupmailIds;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        /// <summary>
        /// Method to check if a group Mail exists or not and get corresponding MailIds.
        /// By Tulasidhar
        /// </summary>
        /// <param name="Tolist"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<string>>> CheckGroupMailId(List<string> Tolist)
        {
            try
            {
                Dictionary<string, List<string>> FinalList = new Dictionary<string, List<string>>();
                var groupemailRepo = uow.GetGenericRepository<EmailGroup>();
                var Cduser_GrpMailrepo = uow.GetGenericRepository<CDUser_GroupEmail>();
                var res = await groupemailRepo.GetAllAsync();
                foreach (var item in Tolist)
                {
                    var grp = res.Where(x => x.EmailGroupName.ToLower() == item.ToLower()).FirstOrDefault();
                    if (grp != null)
                    {
                        var EmailGrpId = grp.EmailGroupID;
                        List<string> result = (from key in Cduser_GrpMailrepo.Get(x => x.EmailGroupID == grp.EmailGroupID, "CDUser")
                                               select key.CDUser.EmailId).ToList();
                        FinalList.Add(item, result);

                        //var dfdfjl = grp.CDUserGroupMails.Select(x => x.CDUser.EmailId).ToList();
                    }
                }
                return FinalList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Get All Group Mails with corresponding Emails -- Tulasidhar
        /// </summary>
        /// <returns></returns>
        public async Task<List<GroupMailDTO>> GetAllGroupMailsAsync(string AuthId)
        {
            try
            {
                var GrpEmails = await uow.GetGenericRepository<EmailGroup>().GetAllAsync("LastUpdateByUser,CreatedByUser,CDUserGroupMails.CDUser.CDRoles.CDRole, CDUserGroupMails.CDUser.Profile.ContractInfoes.ContractGroupInfoes.PracticingGroup, CDUserGroupMails.CDUser.Profile.OtherIdentificationNumber, CDUserGroupMails.CDUser.Profile.PersonalDetail.ProviderLevel");
                List<GroupMailDTO> result = new List<GroupMailDTO>();
                var cduserId = GetCDUserIdFromAuthId(AuthId);
                foreach (var item in GrpEmails)
                {
                    item.CDUserGroupMails = item.CDUserGroupMails.Where(x => x.Status != StatusType.Inactive.ToString()).ToList();
                    GroupMailDTO dto = new GroupMailDTO();
                    dto.CreatedBy = item.CreatedByUser;
                    dto.Description = item.Description;
                    dto.EmailGroupId = item.EmailGroupID;
                    dto.EmailGroupName = item.EmailGroupName;
                    dto.Status = item.Status;
                    dto.CurrentCDuserId = cduserId;
                    dto.Emails = item.CDUserGroupMails.ToDictionary(z => z.CDUserId, z => z.CDUser.EmailId);
                    dto.LastUpdatedBy = item.LastUpdateByUser;
                    dto.GroupMailUserDetails = new List<SearchUserforGroupMailDTO>();
                    item.CDUserGroupMails.ForEach(x => { dto.GroupMailUserDetails.Add(ConstructResultForGroupMail(x.CDUser)); });
                    result.Add(dto);
                }
                return result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private SearchUserforGroupMailDTO ConstructResultForGroupMail(CDUser Cduser)
        {
            SearchUserforGroupMailDTO user = new SearchUserforGroupMailDTO();
            if (Cduser.Profile == null && Cduser.Status != StatusType.Inactive.ToString() && Cduser.AuthenicateUserId != null)
            {
                user.CDuserId = Cduser.CDUserID;
                user.EmailIds = Cduser.EmailId;
                user.FullName = Cduser.AuthenicateUserId;
                user.Roles = Cduser.CDRoles.ToList().Select(x => x.CDRole.Name).ToList();
                user.UserType = "User";
            }
            else if (Cduser.Profile != null && Cduser.Profile.Status != StatusType.Inactive.ToString())
            {
                user.CDuserId = Cduser.CDUserID;
                user.EmailIds = Cduser.EmailId;
                user.FirstName = Cduser.Profile.PersonalDetail.FirstName;
                user.LastName = Cduser.Profile.PersonalDetail.LastName;
                user.FullName = user.FirstName + " " + (Cduser.Profile.PersonalDetail.MiddleName != null ? Cduser.Profile.PersonalDetail.MiddleName + " " : "") + user.LastName;
                user.Roles = Cduser.CDRoles.ToList().Select(x => x.CDRole.Name).ToList();
                user.ProviderLevel = Cduser.Profile.PersonalDetail.ProviderLevel == null ? null : Cduser.Profile.PersonalDetail.ProviderLevel.Name;
                user.ProviderRelationship = Cduser.Profile.ContractInfoes.Any(x => x.ContractStatus != ContractStatus.Inactive.ToString()) == false ? null : Cduser.Profile.ContractInfoes.Where(x => x.ContractStatus != ContractStatus.Inactive.ToString()).FirstOrDefault().ProviderRelationship;
                //user.IPA = Cduser.Profile.ContractInfoes.Any(x => x.ContractStatus != ContractStatus.Inactive.ToString()) == false ? null : Cduser.Profile.ContractInfoes.Where(x => x.ContractStatus != ContractStatus.Inactive.ToString()).FirstOrDefault().ContractGroupInfoes.Select(x => x.PracticingGroup.Group.Name).ToList();
                user.IPA = Cduser.Profile.ContractInfoes != null ? GetGroupNames(Cduser.Profile) : null;
                user.ProfileImagePath = Cduser.Profile.ProfilePhotoPath;
                user.UserType = "Provider";
                user.NPINumber = Cduser.Profile.OtherIdentificationNumber.NPINumber.ToString();
            }
            else if (Cduser.Profile == null && Cduser.Status != StatusType.Inactive.ToString() && Cduser.AuthenicateUserId == null)
            {
                var OtherUser = uow.GetGenericRepository<OtherUser>().Find(x => x.CDUserID == Cduser.CDUserID);
                if (OtherUser != null)
                {
                    user.FirstName = OtherUser.FirstName;
                    user.LastName = OtherUser.LastName;
                    user.EmailIds = OtherUser.EmailId;
                    user.FullName = OtherUser.FullName;
                    user.CDuserId = OtherUser.CDUserID;
                    user.UserType = "OtherUser";
                }
            }
            return user;
        }

        public List<string> GetAllGroupMailNamesasync()
        {
            try
            {
                var groupMailRepo = uow.GetGenericRepository<EmailGroup>();
                List<string> res = groupMailRepo.Get(x => x.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).Select(y => y.EmailGroupName).ToList();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        public async Task<List<CDUser>> GetAllCDusersasync()
        {
            try
            {
                var cduserrepo = uow.GetGenericRepository<CDUser>();
                var otheruserrepo = uow.GetGenericRepository<OtherUser>();
                var cdusers = await cduserrepo.GetAllAsync();
                var otherusers = await otheruserrepo.GetAllAsync();
                List<CDUser> cdusersresult = new List<CDUser>();
                cdusersresult = cdusers.ToList();
                cdusersresult.AddRange(otherusers);
                foreach (var item in cdusersresult)
                {
                    item.CDRoles = null;
                }
                return cdusersresult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Private method to get profile ID if the recipient exists in the system
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private int? GetProfileID(string emailAddress)
        {
            var profileRepo = uow.GetGenericRepository<Profile>();
            int? profileID = null;
            var profiles = profileRepo.Get(p => p.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString(), "ContactDetail.EmailIDs");
            foreach (Profile profile in profiles)
            {
                if ((profile.ContactDetail != null) ? (profile.ContactDetail.EmailIDs != null) ? profile.ContactDetail.EmailIDs.Any(e => e.EmailAddress.ToLower().Equals(emailAddress.ToLower())) : false : false)
                {
                    profileID = profile.ProfileID;
                }
            }
            return profileID;
            //int? profileID = profileRepo.Find(p => p.ContactDetail.EmailIDs.Any(e => e.EmailAddress.ToLower().Equals(p.ToString().ToLower()))).ProfileID;
            //throw new NotImplementedException();
        }


        public async Task<IEnumerable<EmailTemplate>> GetAllEmailTemplatesAsync()
        {
            try
            {
                var emailTemplateRepo = uow.GetGenericRepository<EmailTemplate>();
                IEnumerable<EmailTemplate> emailTemplates = await emailTemplateRepo.GetAsync(e => e.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString());
                return emailTemplates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to inactivate an Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<EmailInfo> StopFollowUpEmailAsync(int emailInfoID)
        {
            var emailServiceRepository = uow.GetGenericRepository<EmailInfo>();
            EmailInfo emailToBeStopped = await emailServiceRepository.FindAsync(e => e.EmailInfoID == emailInfoID, "EmailRecipients.EmailTracker, EmailRecurrenceDetail");
            try
            {
                emailToBeStopped.StatusType = Entities.MasterData.Enums.StatusType.Inactive;
                emailServiceRepository.Update(emailToBeStopped);
                await emailServiceRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return emailToBeStopped;
        }

        /// <summary>
        /// Method to inactivate a recipient in an email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="emailRecipientIDs"></param>
        /// <returns></returns>
        public async Task<EmailInfo> StopFollowUpEmailForSelectReceiversAsync(int emailInfoID, List<int> emailRecipientIDs)
        {
            var emailServiceRepository = uow.GetGenericRepository<EmailInfo>();
            EmailInfo emailToBeStopped = await emailServiceRepository.FindAsync(e => e.EmailInfoID == emailInfoID, "EmailRecipients.EmailTracker, EmailRecurrenceDetail");
            try
            {
                foreach (EmailRecipientDetail recipient in emailToBeStopped.EmailRecipients)
                {
                    foreach (var r in emailRecipientIDs)
                    {
                        if (recipient.EmailRecipientDetailID.Equals(r))
                        {
                            recipient.StatusType = Entities.MasterData.Enums.StatusType.Inactive;
                        }
                    }
                }
                if (emailToBeStopped.EmailRecipients.All(e => e.Status == Entities.MasterData.Enums.StatusType.Inactive.ToString()))
                {
                    emailToBeStopped.StatusType = Entities.MasterData.Enums.StatusType.Inactive;
                }
                emailServiceRepository.Update(emailToBeStopped);
                await emailServiceRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return emailToBeStopped;
        }

        private int? GetUserId(string authUserId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == authUserId);
                return user.ProfileId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> GetAllEmails()
        {
            List<string> emails = new List<string>();
            try
            {
                var emailrepo = uow.GetGenericRepository<EmailDetail>();
                var res = await emailrepo.GetAllAsync();
                foreach (var item in res)
                {
                    emails.Add(item.EmailAddress);
                }
                return emails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<EmailDetail> GetallEmailDetailsforProviderName()
        {
            List<EmailDetail> emails = new List<EmailDetail>();
            try
            {
                var emailrepo = uow.GetGenericRepository<EmailDetail>();
                emails = emailrepo.GetAll().ToList();
                return emails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<string> GetGroupNames(Profile provider)
        {
            List<string> groupNames = new List<string>();
            foreach (var contractInfo in provider.ContractInfoes)
            {
                if (contractInfo.ContractStatus == ContractStatus.Accepted.ToString())
                {
                    foreach (var contractGroupInfo in contractInfo.ContractGroupInfoes)
                    {
                        if (contractGroupInfo.ContractGroupStatus == ContractGroupStatus.Accepted.ToString() && contractGroupInfo.Status != StatusType.Inactive.ToString())
                        {
                            groupNames.Add(contractGroupInfo.PracticingGroup.Group.Name);
                        }

                    }
                }
            }
            return groupNames;
        }
        private List<ContactDetail> GetallContactDetails()
        {
            List<ContactDetail> contactdetails = new List<ContactDetail>();
            try
            {
                var Contactrepo = uow.GetGenericRepository<ContactDetail>();
                contactdetails = Contactrepo.GetAll("EmailIDs").ToList();
                return contactdetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Profile> GetallProfiles()
        {
            List<Profile> profiles = new List<Profile>();
            try
            {
                var profilerepo = uow.GetGenericRepository<Profile>();
                profiles = profilerepo.GetAll("ContactDetail").ToList();
                //foreach (var item in res)
                //{
                //    profiles.Add(item);
                //}
                return profiles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCDUserIdFromAuthId(string UserAuthId)
        {
            var userRepo = uow.GetGenericRepository<CDUser>();
            var user = userRepo.Find(u => u.AuthenicateUserId == UserAuthId);

            return user.CDUserID;
        }


        public string SavePDFFile(string PDFFile)
        {
            string documentPath = "";
            try
            {
                documentPath = iDocumentsManager.DocumentCheckListPDF(PDFFile, DocumentRootPath.DocumentCheckListPDFPATH);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return documentPath;
        }

        public string SaveDelegatedPlanPDFFile(byte[] pdfbytes, int ccID)
        {
            string documentPath = "";
            try
            {
                documentPath = iDocumentsManager.DelegatedLoadToPlanPDF(pdfbytes, DocumentRootPath.DELEGATED_LOADTOPLAN_DOCUMENT_PATH, ccID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return documentPath;
        }
        public string SaveDocumentChecklistPDFFile(byte[] pdfbytes, int ccID)
        {
            string documentPath = "";
            try
            {
                documentPath = iDocumentsManager.DocumentationCheckListPDF(pdfbytes, DocumentRootPath.DOCUMENTATION_CHECKLIST_DOCUMENT_PATH, ccID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return documentPath;
        }
    }
}