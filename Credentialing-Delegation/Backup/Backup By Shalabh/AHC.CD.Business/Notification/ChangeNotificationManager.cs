using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.Notification;
using AHC.MailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Notification
{
    public class ChangeNotificationManager : IChangeNotificationManager
    {
        private IGenericRepository<ChangeNotificationDetail> changeNotificationRepository;
        private IGenericRepository<CDUser> cdUserRepo;
        private IGenericRepository<EmailInfo> emailInfoRepository;
        private IGenericRepository<EmailTemplate> emailTemplateRepository;
        private IProfileRepository profileRepository;
        private IEmailSender emailSender;
        public ChangeNotificationManager(IUnitOfWork uow, IEmailSender emailSender)
        {
            emailInfoRepository = uow.GetGenericRepository<EmailInfo>();
            emailTemplateRepository = uow.GetGenericRepository<EmailTemplate>();
            cdUserRepo = uow.GetGenericRepository<CDUser>();
            changeNotificationRepository = uow.GetGenericRepository<ChangeNotificationDetail>();
            profileRepository = uow.GetProfileRepository();
            this.emailSender = emailSender;
        }
        public async Task SaveNotificationDetailAsync(Entities.Notification.ChangeNotificationDetail changeNotificationDetail)
        {
            var profile = profileRepository.Get(p => p.ProfileID == changeNotificationDetail.ProfileID).FirstOrDefault();
            if (profile == null) return;
            string providerMidleName="";
            if (profile.PersonalDetail != null)
            {
                string providerFirstName = (profile.PersonalDetail.FirstName.Contains("Not Available"))?"":profile.PersonalDetail.FirstName;
                if (profile.PersonalDetail.MiddleName != null)
                {
                    providerMidleName = (profile.PersonalDetail.MiddleName.Contains("Not Available")) ? "" : profile.PersonalDetail.MiddleName;
                }
                string providerLastName = (profile.PersonalDetail.LastName.Contains("Not Available")) ? "" : profile.PersonalDetail.LastName;
                string providerSolutation = (profile.PersonalDetail.Salutation.Contains("Not Available")) ? "" : profile.PersonalDetail.Salutation; ;
                string providerFullName = providerSolutation + " " + providerFirstName + " " + providerMidleName + " " + providerLastName;
                changeNotificationDetail.ProviderFullName = providerFullName;
            }
            if (profile.ContactDetail != null && profile.ContactDetail.EmailIDs != null)
            {
                changeNotificationDetail.ProviderEmailID = profileRepository.Find(changeNotificationDetail.ProfileID).ContactDetail.EmailIDs.First().EmailAddress;
            }
            if(profile.OtherIdentificationNumber != null)
            {
                changeNotificationDetail.NPINumber = profile.OtherIdentificationNumber.NPINumber;
            }
            changeNotificationRepository.Create(changeNotificationDetail);
            await changeNotificationRepository.SaveAsync();

            CDUser cdUser = cdUserRepo.Find(c => c.ProfileId == profile.ProfileID, "DashboardNotifications");

            if (cdUser.DashboardNotifications == null)
            {
                cdUser.DashboardNotifications = new List<UserDashboardNotification>();
            }
            cdUser.DashboardNotifications.Add(new UserDashboardNotification
            {
                StatusType = Entities.MasterData.Enums.StatusType.Active,
                AcknowledgementStatusType = Entities.MasterData.Enums.AcknowledgementStatusType.Unread,
                Action = "Profile Management",
                ActionPerformed = changeNotificationDetail.SectionName + " - " + changeNotificationDetail.ActionPerformed,
                ActionPerformedByUser = changeNotificationDetail.ActionPerformedUser,
                RedirectURL = "/Profile/MasterProfile/Index"
            });
            cdUserRepo.Update(cdUser);
            await cdUserRepo.SaveAsync();
        }


        public List<ChangeNotificationDetail> GetChangeNotificationDetails(string actionPerformedUser=null)
        {
            if (string.IsNullOrEmpty(actionPerformedUser))
                return changeNotificationRepository.GetAll().ToList<ChangeNotificationDetail>();
            return changeNotificationRepository.GetAll().Where(c => c.ActionPerformedUser.Equals(actionPerformedUser)).ToList<ChangeNotificationDetail>();
        }

        private bool DiscardChangeNotificationDetails(int profileID)
        {
            var item = changeNotificationRepository.Get(cd => cd.ProfileID == profileID);
            if (item != null || item.Count() == 0)
            {
                foreach (var eachItem in item)
                {
                    changeNotificationRepository.Delete(eachItem);
                }
                changeNotificationRepository.Save();
            }            
                        
            return true;
        }

        public bool NotifyChanges()
        {
            var changeDetails = (from cnd in GetChangeNotificationDetails()
                                 orderby cnd.ProfileID
                                 select cnd).ToList();

            if (changeDetails == null || changeDetails.Count() == 0)
                return false;

            var profileIds = (from cd in changeDetails
                              select cd.ProfileID).Distinct();

            foreach (var profileID in profileIds)
            {
                List<ChangeNotificationDetail> cDetails = changeDetails.Where(cd => cd.ProfileID == profileID).Select(cd => cd).ToList<ChangeNotificationDetail>();
                EMailModel emailModel = getEmailModel(cDetails);
                List<EmailInfo> emailInfoes = emailInfoRepository.GetAll("EmailRecipients.EmailTracker, EmailRecurrenceDetail").ToList();

                if (emailModel != null)
                {
                    EmailInfo emailInfo = new EmailInfo();
                    emailInfo.From = emailModel.From;
                    emailInfo.Subject = emailModel.Subject;
                    emailInfo.Body = emailModel.Body;
                    emailInfo.EmailNotificationTypeCategory = Entities.MasterData.Enums.EmailNotificationType.Alert;

                    emailInfo.IsRecurrenceEnabledYesNoOption = Entities.MasterData.Enums.YesNoOption.NO;
                    emailInfo.SendingDate = DateTime.Now;
                    //emailInfo.EmailRecurrenceDetail = new EmailRecurrenceDetail();
                    //emailInfo.EmailRecurrenceDetail.RecurrenceIntervalTypeCategory = Entities.MasterData.Enums.RecurrenceIntervalType.Daily;
                    //emailInfo.EmailRecurrenceDetail.StatusType = Entities.MasterData.Enums.StatusType.Active;
                    //emailInfo.EmailRecurrenceDetail.IntervalFactor = 3;

                    emailInfo.StatusType = Entities.MasterData.Enums.StatusType.Active;
                    if (emailModel.To != null)
                    {
                        foreach (string recipient in emailModel.To.Split(','))
                        {
                            EmailRecipientDetail emailRecipient = new EmailRecipientDetail();
                            emailRecipient.Recipient = recipient;
                            emailRecipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.To;
                            emailRecipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                            if (emailInfo.EmailRecipients == null)
                            {
                                emailInfo.EmailRecipients = new List<EmailRecipientDetail>();
                            }
                            emailInfo.EmailRecipients.Add(emailRecipient);
                        }
                    }
                    if (emailModel.Cc != null)
                    {
                        foreach (string recipient in emailModel.Cc.Split(','))
                        {
                            EmailRecipientDetail emailRecipient = new EmailRecipientDetail();
                            emailRecipient.Recipient = recipient;
                            emailRecipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.CC;
                            emailRecipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                            if (emailInfo.EmailRecipients == null)
                            {
                                emailInfo.EmailRecipients = new List<EmailRecipientDetail>();
                            }
                            emailInfo.EmailRecipients.Add(emailRecipient);
                        }
                    }
                    if (emailModel.Bcc != null)
                    {
                        foreach (string recipient in emailModel.Bcc.Split(','))
                        {
                            EmailRecipientDetail emailRecipient = new EmailRecipientDetail();
                            emailRecipient.Recipient = recipient;
                            emailRecipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.BCC;
                            emailRecipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                            if (emailInfo.EmailRecipients == null)
                            {
                                emailInfo.EmailRecipients = new List<EmailRecipientDetail>();
                            }
                            emailInfo.EmailRecipients.Add(emailRecipient);
                        }
                    }
                    emailInfoRepository.Create(emailInfo);
                    emailInfoRepository.Save();
                    DiscardChangeNotificationDetails(profileID);
                }                

                //if (emailSender.SendMail(emailModel))
                //{
                //    DiscardChangeNotificationDetails(profileID);
                //}
            }
            return true;
        }

        private EMailModel getEmailModel(List<ChangeNotificationDetail> changeDetails)
        {
            EMailModel emailModel = new EMailModel();
            emailModel.Subject = "Profile Update Notification";
            emailModel.To = "testingsingh285@gmail.com";
            emailModel.Cc = "testingsingh285@gmail.com";
            emailModel.Bcc = "testingsingh285@gmail.com";
            emailModel.From = "abcdd1536@gmail.com";
            //emailModel.To = changeDetails.First().ProviderEmailID;
            //emailModel.Cc ="cco@accesshealthcarellc.net";
            StringBuilder mailBody = new StringBuilder();
            mailBody.Append("<b>Dear ");
            mailBody.Append(changeDetails.First().ProviderFullName);
            mailBody.Append("</b></br></br>");
            mailBody.Append("<p>This is to inform you that the following section(s) of your profile have been updated:</p>");
            mailBody.Append("<br/>");
            mailBody.Append("<ol>");
            foreach (var cd in changeDetails)
            {
                mailBody.Append("<li>");
                mailBody.Append(cd.SectionName);
                mailBody.Append(" , ");
                mailBody.Append(cd.ActionPerformed);
                mailBody.Append(" by ");
                mailBody.Append(cd.ActionPerformedUser);
                mailBody.Append(" on ");
                mailBody.Append(cd.DateTime);
                mailBody.Append("</li>");
            }
            mailBody.Append("</ol>");
            mailBody.Append("<br/><br/>");
            mailBody.Append("Please contact us if you have any questions.");
            mailBody.Append("<br/><br/>");
            mailBody.Append("Thank You,<br/>");
            mailBody.Append("Credentialing Dept.<br/>");
            mailBody.Append("Access Healthcare Physicians, LLC<br/>");
            mailBody.Append("(352) 799-0046<br/>");
            mailBody.Append("credentialing@accesshealthcarellc.net");

            emailModel.Body = mailBody.ToString();
            return emailModel;
        }

        //public bool NotifyChanges()
        //{
        //    var changeDetails = (from cnd in GetChangeNotificationDetails()
        //                         orderby cnd.ProfileID
        //                         select cnd).ToList();

        //    if (changeDetails == null || changeDetails.Count() == 0)
        //        return false;

        //    var profileIds = (from cd in changeDetails
        //                      select cd.ProfileID).Distinct();

        //    foreach (var profileID in profileIds)
        //    {
        //        List<ChangeNotificationDetail> cDetails = changeDetails.Where(cd => cd.ProfileID == profileID).Select(cd => cd).ToList<ChangeNotificationDetail>();
        //        EMailModel emailModel = getEmailModel(cDetails, Entities.MasterData.Enums.EmailNotificationType.Alert, "Profile Management Updates");
        //        if (emailModel != null)
        //        {

        //            List<EmailInfo> emailInfoes = emailInfoRepository.GetAll("EmailRecipients.EmailTracker, EmailRecurrenceDetail").ToList();

        //            EmailInfo emailInfo = new EmailInfo();
        //            emailInfo.From = emailModel.From;
        //            emailInfo.Subject = emailModel.Subject;
        //            emailInfo.Body = emailModel.Body;
        //            emailInfo.EmailNotificationTypeCategory = Entities.MasterData.Enums.EmailNotificationType.Alert;

        //            emailInfo.IsRecurrenceEnabledYesNoOption = Entities.MasterData.Enums.YesNoOption.YES;
        //            emailInfo.EmailRecurrenceDetail = new EmailRecurrenceDetail();
        //            emailInfo.EmailRecurrenceDetail.RecurrenceIntervalTypeCategory = Entities.MasterData.Enums.RecurrenceIntervalType.Daily;
        //            emailInfo.EmailRecurrenceDetail.StatusType = Entities.MasterData.Enums.StatusType.Active;
        //            emailInfo.EmailRecurrenceDetail.IntervalFactor = 3;

        //            emailInfo.StatusType = Entities.MasterData.Enums.StatusType.Active;
        //            if (emailModel.To != null)
        //            {
        //                foreach (string recipient in emailModel.To.Split(','))
        //                {
        //                    EmailRecipientDetail emailRecipient = new EmailRecipientDetail();
        //                    emailRecipient.Recipient = recipient;
        //                    emailRecipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.To;
        //                    emailRecipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
        //                    if (emailInfo.EmailRecipients == null)
        //                    {
        //                        emailInfo.EmailRecipients = new List<EmailRecipientDetail>();
        //                    }
        //                    emailInfo.EmailRecipients.Add(emailRecipient);
        //                }
        //            }
        //            if (emailModel.Cc != null)
        //            {
        //                foreach (string recipient in emailModel.Cc.Split(','))
        //                {
        //                    EmailRecipientDetail emailRecipient = new EmailRecipientDetail();
        //                    emailRecipient.Recipient = recipient;
        //                    emailRecipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.CC;
        //                    emailRecipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
        //                    if (emailInfo.EmailRecipients == null)
        //                    {
        //                        emailInfo.EmailRecipients = new List<EmailRecipientDetail>();
        //                    }
        //                    emailInfo.EmailRecipients.Add(emailRecipient);
        //                }
        //            }
        //            if (emailModel.Bcc != null)
        //            {
        //                foreach (string recipient in emailModel.Bcc.Split(','))
        //                {
        //                    EmailRecipientDetail emailRecipient = new EmailRecipientDetail();
        //                    emailRecipient.Recipient = recipient;
        //                    emailRecipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.BCC;
        //                    emailRecipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
        //                    if (emailInfo.EmailRecipients == null)
        //                    {
        //                        emailInfo.EmailRecipients = new List<EmailRecipientDetail>();
        //                    }
        //                    emailInfo.EmailRecipients.Add(emailRecipient);
        //                }
        //            }
        //            emailInfoRepository.Create(emailInfo);
        //            emailInfoRepository.Save();
        //            DiscardChangeNotificationDetails(profileID);
        //        }
        //    }
        //    return true;
        //}

        //private EMailModel getEmailModel(List<ChangeNotificationDetail> changeDetails, Entities.MasterData.Enums.EmailNotificationType emailType, string action)
        //{
        //    EmailTemplate selectedEmailTemplate = emailTemplateRepository.Get(e => e.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString()).FirstOrDefault();
        //    EMailModel emailModel = null;
        //    if (selectedEmailTemplate != null)
        //    {
        //        emailModel = new EMailModel();
        //        emailModel.Subject = selectedEmailTemplate.Subject;
        //        emailModel.To = "testingsingh285@gmail.com";
        //        emailModel.From = changeDetails.First().ActionPerformedUser;
        //        //emailModel.To = changeDetails.First().ProviderEmailID;
        //        emailModel.Cc = "testingsingh285@gmail.com";
        //        emailModel.Bcc = "testingsingh285@gmail.com";
        //        emailModel.Body = selectedEmailTemplate.Body;
        //        //if (emailModel.Body.Contains("[Section Name]"))
        //        //{
        //        //    foreach (var cd in changeDetails)
        //        //    {
        //        //        emailModel.Body = emailModel.Body.Replace("[Section Name]", cd.SectionName);
        //        //        if (emailModel.Body.Contains("[Action Performed]"))
        //        //        {
        //        //            emailModel.Body = emailModel.Body.Replace("[Action Performed]", cd.ActionPerformed);
        //        //        }
        //        //        if (emailModel.Body.Contains("[Action Performed User]"))
        //        //        {
        //        //            emailModel.Body = emailModel.Body.Replace("[Action Performed User]", cd.ActionPerformedUser);
        //        //        }
        //        //        if (emailModel.Body.Contains("[Action Performed User]"))
        //        //        {
        //        //            emailModel.Body = emailModel.Body.Replace("[Action Performed User]", cd.ActionPerformedUser);
        //        //        }
        //        //        if (emailModel.Body.Contains("[Date]"))
        //        //        {
        //        //            emailModel.Body = emailModel.Body.Replace("[Date]", cd.DateTime.ToShortDateString());
        //        //        }
        //        //    }
        //        //}
        //        emailModel.Body = emailModel.Body.Replace("[Provider Name]", changeDetails.First().ProviderFullName);
        //    }
        //    return emailModel;
        //}
    }
}
