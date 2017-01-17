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
            changeNotificationRepository.Delete(item);
            
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
                if(emailSender.SendMail(emailModel))
                {
                    //DiscardChangeNotificationDetails(profileID);
                }
            }
            return true;
        }

        private EMailModel getEmailModel(List<ChangeNotificationDetail> changeDetails)
        {
            EMailModel emailModel = new EMailModel();
            emailModel.Subject = "Profile Update Notification";
            emailModel.To = "bgiri@pratian.com";
            //emailModel.To = changeDetails.First().ProviderEmailID;
            //emailModel.Cc ="cco@accesshealthcarellc.net";
            StringBuilder mailBody = new StringBuilder();
            mailBody.Append("<b>Dear ");
            mailBody.Append(changeDetails.First().ProviderFullName);
            mailBody.Append("</b></br></br>");
            mailBody.Append("<p>This is to inform you that the following section(s) of your profile have been updated</p>");
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
            mailBody.Append("Thanking you,<br/>");
            mailBody.Append("Jeanine Martin<br/><br/>");
            mailBody.Append("Credentialing Coordinator<br/>");
            mailBody.Append("Access HealthCare LLC");


            emailModel.Body = mailBody.ToString();
            return emailModel;
        }
    }
}
