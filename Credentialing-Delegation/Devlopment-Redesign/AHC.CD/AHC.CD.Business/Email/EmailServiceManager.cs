using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Email
{
    public class EmailServiceManager : IEmailServiceManager
    {
        private IUnitOfWork uow = null;

        public EmailServiceManager(IUnitOfWork uow)
        {
            this.uow = uow;
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
                    if (item.EmailRecipients != null)
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
                }

                return emailInfoList;
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
                    if (followUpEmail.SendingDate.HasValue && (followUpEmail.EmailRecurrenceDetail != null ? (followUpEmail.EmailRecurrenceDetail.NextMailingDate.HasValue ? followUpEmail.EmailRecurrenceDetail.NextMailingDate.Value >= DateTime.Now.Date : false) : false) && (followUpEmail.EmailRecurrenceDetail != null ? (followUpEmail.EmailRecurrenceDetail.ToDate.HasValue ? followUpEmail.EmailRecurrenceDetail.ToDate.Value <= DateTime.Now.Date : true) : false) && (followUpEmail.EmailRecurrenceDetail != null ? (followUpEmail.EmailRecurrenceDetail.IsRecurrenceScheduled != Entities.MasterData.Enums.YesNoOption.YES.ToString()) : false))
                    {
                        followUpEmails.Add(followUpEmail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return followUpEmails;
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
                        if (recipient.ProfileID == profileID && ((recipient.EmailTracker != null) && (recipient.EmailTracker.Count != 0)) ? (recipient.EmailTracker.Any(t => t.EmailStatusType == Entities.MasterData.Enums.EmailStatusType.SendingFailed.ToString()) ? false : true ) : false)
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
                emailInfoRepo.Create(email);
                await emailInfoRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return email;
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
    }
}
