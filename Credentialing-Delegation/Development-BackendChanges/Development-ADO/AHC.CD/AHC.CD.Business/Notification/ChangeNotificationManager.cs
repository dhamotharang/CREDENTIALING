using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.EmailNotifications;
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
        private IGenericRepository<EmailInfo> emailInfoRepository;
        private IGenericRepository<EmailTemplate> emailTemplateRepository;
        private IProfileRepository profileRepository;
        private IEmailSender emailSender;
        public ChangeNotificationManager(IUnitOfWork uow, IEmailSender emailSender)
        {
            emailInfoRepository = uow.GetGenericRepository<EmailInfo>();
            emailTemplateRepository = uow.GetGenericRepository<EmailTemplate>();
            changeNotificationRepository = uow.GetGenericRepository<ChangeNotificationDetail>();
            profileRepository = uow.GetProfileRepository();
            this.emailSender = emailSender;
        }
        public async Task SaveNotificationDetailAsync(Entities.Notification.ChangeNotificationDetail changeNotificationDetail)
        {
            var profile = profileRepository.Find(changeNotificationDetail.ProfileID);
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
        //        EMailModel emailModel = getEmailModel(cDetails);
        //        if (emailSender.SendMail(emailModel))
        //        {
        //            //DiscardChangeNotificationDetails(profileID);
        //        }
        //    }
        //    return true;
        //}

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
                EMailModel emailModel = getEmailModel(cDetails, Entities.MasterData.Enums.EmailNotificationType.Alert, "Profile Management Updates");
                if (emailModel != null)
                {

                    List<EmailInfo> emailInfoes = emailInfoRepository.GetAll("EmailRecipients.EmailTracker, EmailRecurrenceDetail").ToList();

                    EmailInfo emailInfo = new EmailInfo();
                    emailInfo.From = emailModel.From;
                    emailInfo.Subject = emailModel.Subject;
                    emailInfo.Body = emailModel.Body;
                    emailInfo.EmailNotificationTypeCategory = Entities.MasterData.Enums.EmailNotificationType.Alert;
                    emailInfo.IsRecurrenceEnabledYesNoOption = Entities.MasterData.Enums.YesNoOption.NO;
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
            }
            return true;
        }

        private EMailModel getEmailModel(List<ChangeNotificationDetail> changeDetails, Entities.MasterData.Enums.EmailNotificationType emailType, string action)
        {
            EmailTemplate selectedEmailTemplate = emailTemplateRepository.Get(e => e.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString() && e.EmailNotificationType == emailType.ToString() && e.Action == action).FirstOrDefault();
            EMailModel emailModel = null;
            if (selectedEmailTemplate != null)
            {
                emailModel = new EMailModel();
                emailModel.Subject = selectedEmailTemplate.Subject;
                emailModel.To = "testingsingh285@gmail.com";
                emailModel.From = changeDetails.First().ActionPerformedUser;
                //emailModel.To = changeDetails.First().ProviderEmailID;
                emailModel.Cc = "testingsingh285@gmail.com";
                emailModel.Bcc = "testingsingh285@gmail.com";
                emailModel.Body = selectedEmailTemplate.Body;
                //if (emailModel.Body.Contains("[Section Name]"))
                //{
                //    foreach (var cd in changeDetails)
                //    {
                //        emailModel.Body = emailModel.Body.Replace("[Section Name]", cd.SectionName);
                //        if (emailModel.Body.Contains("[Action Performed]"))
                //        {
                //            emailModel.Body = emailModel.Body.Replace("[Action Performed]", cd.ActionPerformed);
                //        }
                //        if (emailModel.Body.Contains("[Action Performed User]"))
                //        {
                //            emailModel.Body = emailModel.Body.Replace("[Action Performed User]", cd.ActionPerformedUser);
                //        }
                //        if (emailModel.Body.Contains("[Action Performed User]"))
                //        {
                //            emailModel.Body = emailModel.Body.Replace("[Action Performed User]", cd.ActionPerformedUser);
                //        }
                //        if (emailModel.Body.Contains("[Date]"))
                //        {
                //            emailModel.Body = emailModel.Body.Replace("[Date]", cd.DateTime.ToShortDateString());
                //        }
                //    }
                //}
                emailModel.Body = emailModel.Body.Replace("[Provider Name]", changeDetails.First().ProviderFullName);
            }
            return emailModel;
        }
    }
}
