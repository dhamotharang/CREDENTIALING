using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.Demographics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Email
{
    public class EmailServiceManager : IEmailServiceManager
    {
        private IUnitOfWork uow = null;
        private IGenericRepository<EmailInfo> emailInfoRepository = null;
        public EmailServiceManager(IUnitOfWork uow)
        {
            this.uow = uow;
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

                return emailInfoList.OrderByDescending(e=>e.SendingDate).ToList();
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
        public async Task<EmailInfo> SaveComposedEmail(EmailInfo email)
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
                if ((email.SendingDate.HasValue ? true : false) && (email.IsRecurrenceEnabled == YesNoOption.YES.ToString() ? ((email.EmailRecurrenceDetail.FromDate.HasValue ? email.EmailRecurrenceDetail.FromDate.Value.Date >= DateTime.Now.Date:true) && (email.EmailRecurrenceDetail.ToDate.HasValue ? email.EmailRecurrenceDetail.ToDate.Value.Date >= DateTime.Now.Date : true)) : true))
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

                foreach(EmailAttachment EmailAttachments in email.EmailAttachments)
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

        /// <summary>
        /// Method to Add a new Group Email.
        /// By Tulasidhar
        /// </summary>
        /// <param name="groupemail"></param>
        /// <param name="emailIds"></param>
        /// <returns></returns>
        public async Task<EmailGroup> AddNewGroupEmail(EmailGroup groupemail, List<string> emailIds)
        {
            try
            {
                var emaildetailRepo = uow.GetGenericRepository<EmailDetail>();
                var groupemailrepo = uow.GetGenericRepository<EmailGroup>();
                var res = await groupemailrepo.GetAllAsync();
                if(res.Any(x=>x.EmailGroupName.ToLower() == groupemail.EmailGroupName.ToLower()))
                {
                    throw new Exception("Group Email already Exists");
                }
                EmailGroup emailgroup = new EmailGroup();
                emailgroup.GroupEmailDetails = new List<EmailDetail>();
                emailgroup.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                emailgroup.EmailGroupName = groupemail.EmailGroupName;
                
                var Listofemails = emaildetailRepo.GetAll();
                foreach (var item in emailIds)
                {
                    emailgroup.GroupEmailDetails.Add(Listofemails.Where(x => x.EmailAddress == item).First());
                }
                
                groupemailrepo.Create(emailgroup);
                await groupemailrepo.SaveAsync();
                return emailgroup;
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
        public async Task<IEnumerable<EmailGroup>> GetAllGroupMailIdsAsync()
        {
            try
            {
                var groupemailRepo = uow.GetGenericRepository<EmailGroup>();
                var groupmailIds = await groupemailRepo.GetAllAsync();
                return groupmailIds;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

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
                Dictionary<string,List<string>> grpmail = new Dictionary<string,List<string>>();
                EmailGroup grp = new EmailGroup();
                var groupemailRepo = uow.GetGenericRepository<EmailGroup>();
                var res = await groupemailRepo.GetAllAsync("GroupEmailDetails");
                foreach (var item in Tolist)
                {
                    grp = res.Where(x => x.EmailGroupName.ToLower() == item.ToLower()).FirstOrDefault();
                    if(grp != null)
                    {
                        List<string> emailIds = new List<string>();
                        if (grp.GroupEmailDetails != null)
                        {
                            foreach (var emailid in grp.GroupEmailDetails)
                            {
                                emailIds.Add(emailid.EmailAddress);
                            }
                            grpmail.Add(item, emailIds);
                        }
                    }
                }
                return grpmail;
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
    }
}
