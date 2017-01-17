using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
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
        private IProfileRepository profileRepository;
        private IEmailSender emailSender;
        public ChangeNotificationManager(IUnitOfWork uow, IEmailSender emailSender)
        {
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
            changeNotificationRepository.Create(changeNotificationDetail);
            await changeNotificationRepository.SaveAsync();
        }


        public List<ChangeNotificationDetail> GetChangeNotificationDetails(string actionPerformedUser)
        {
            return changeNotificationRepository.GetAll().Where(c => c.ActionPerformedUser.Equals(actionPerformedUser)).ToList<ChangeNotificationDetail>();
        }

        private bool DiscardChangeNotificationDetails(string actionPerformedUser)
        {
            foreach (var item in GetChangeNotificationDetails(actionPerformedUser))
            {
                changeNotificationRepository.Delete(item);
            }
            return true;
        }

        public bool NotifyChanges(string actionPerformedUser)
        {
            var changeDetails = (from cnd in GetChangeNotificationDetails(actionPerformedUser)
                                orderby cnd.ProfileID
                                select cnd).ToList();

            if (changeDetails == null || changeDetails.Count() == 0)
                return false;    
            EMailModel emailModel = new EMailModel();
            emailModel.Subject = "Profile Update Notification";
            emailModel.To = "bgiri@pratian.com";
            string bodyContent = "";
            bodyContent="<h4>This is to inform you that the following sub-sections of your profile were updated by " + actionPerformedUser + " on " + DateTime.Now + "</h4>";
            bodyContent += "<ol>";
            int pid = changeDetails.First().ProfileID;
            foreach (var item in changeDetails)
            {
                
                if (item.ProfileID == pid)
                {
                    //emailModel.To = item.ProviderEmailID;
                    bodyContent += "<li>" + item.SectionName + " " + item.ActionPerformed + "</li>";
                }
                else
                {
                    bodyContent += "</ol>";
                    emailModel.Body = bodyContent;
                    emailSender.SendMail(emailModel);
                    
                    pid = item.ProfileID;
                    //emailModel.To = item.ProviderEmailID;

                    bodyContent = "<h4>This is to inform you that the following sub-sections of your profile were updated by " + actionPerformedUser + " on " + DateTime.Now + "</h4>";

                    bodyContent += "<li>" + item.SectionName + " " + item.ActionPerformed + "</li>";
                }

            }
            bodyContent += "</ol>";
            emailModel.Body = bodyContent;
            emailSender.SendMail(emailModel);
            
            foreach (var item in changeDetails)
            {
                changeNotificationRepository.Delete(item);
            }
            changeNotificationRepository.Save();

            return true;
        }
    }
}
