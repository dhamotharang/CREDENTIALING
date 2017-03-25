using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.Notification;
using AHC.CD.Entities.UserInfo;
using AHC.MailService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        private IGenericRepository<CredentialingInfo> credInfo;
        private IUnitOfWork uow;
        public ChangeNotificationManager(IUnitOfWork uow, IEmailSender emailSender)
        {
            emailInfoRepository = uow.GetGenericRepository<EmailInfo>();
            emailTemplateRepository = uow.GetGenericRepository<EmailTemplate>();
            cdUserRepo = uow.GetGenericRepository<CDUser>();
            changeNotificationRepository = uow.GetGenericRepository<ChangeNotificationDetail>();
            profileRepository = uow.GetProfileRepository();
            credInfo = uow.GetGenericRepository<CredentialingInfo>();
            this.uow = uow;
            this.emailSender = emailSender;
        }
        public async Task SaveNotificationDetailAsync(Entities.Notification.ChangeNotificationDetail changeNotificationDetail)
        {
            var profile = profileRepository.Get(p => p.ProfileID == changeNotificationDetail.ProfileID).FirstOrDefault();
            if (profile == null) return;
            string providerMidleName = "";
            if (profile.PersonalDetail != null)
            {
                string providerFirstName = (profile.PersonalDetail.FirstName.Contains("Not Available")) ? "" : profile.PersonalDetail.FirstName;
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
            if (profile.OtherIdentificationNumber != null)
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



        public async Task SaveNotificationDetailAsyncForAdd(Entities.Notification.ChangeNotificationDetail changeNotificationDetail, bool isCCO)
        {
            var profile = profileRepository.Get(p => p.ProfileID == changeNotificationDetail.ProfileID).FirstOrDefault();
            if (isCCO)
            {
                if (profile == null) return;
                string providerMidleName = "";
                if (profile.PersonalDetail != null)
                {
                    string providerFirstName = (profile.PersonalDetail.FirstName.Contains("Not Available")) ? "" : profile.PersonalDetail.FirstName;
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
                if (profile.OtherIdentificationNumber != null)
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
            else
            {
                //CDUser cdUser = cdUserRepo.Find(c => c.ProfileId == profile.ProfileID, "DashboardNotifications");
                List<CDUser> cdusers = await GetCCOUserID();
                foreach (CDUser cdUser in cdusers)
                {
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
                        RedirectURL = "/Profile/MasterProfile/ProviderProfile/" + profile.ProfileID
                    });
                    cdUserRepo.Update(cdUser);

                }
                await cdUserRepo.SaveAsync();
            }
        }

        private async Task<List<CDUser>> GetCCOUserID()
        {
            
            List<CDUser> cdusers = new List<CDUser>();
            //var userRepo = uow.GetGenericRepository<CDUser>();
            var ccoList = await cdUserRepo.GetAllAsync("CDRoles,CDRoles.CDRole,DashboardNotifications");
            foreach (CDUser TempccoList in ccoList)
            {
                foreach (var TempccoListForRoles in TempccoList.CDRoles)
                {
                    if (TempccoListForRoles.CDRole.Code == "CCO" || TempccoListForRoles.CDRole.Code == "CRA")
                    {
                        //CCMListIDS.Add(TempccmList.CDUserID);
                        cdusers.Add(TempccoList);
                        break;
                    }
                }
            }
            return cdusers;
        }


        public List<ChangeNotificationDetail> GetChangeNotificationDetails(string actionPerformedUser = null)
        {
            if (string.IsNullOrEmpty(actionPerformedUser))
                return changeNotificationRepository.GetAll().ToList<ChangeNotificationDetail>();
            return changeNotificationRepository.GetAll().Where(c => c.ActionPerformedUser.Equals(actionPerformedUser)).ToList<ChangeNotificationDetail>();
        }

        private bool DiscardChangeNotificationDetails(int? profileID)
        {
            if (profileID != null)
            {
                var item = changeNotificationRepository.Get(cd => cd.ProfileID == profileID && cd.ActionPerformed != "CCM Appointment" && cd.ActionPerformed != "Cancellation of Credentialing Committee Meeting" && cd.ActionPerformed != "CCM Approval Status");
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
            else
            {
                return false;
            }
        }


        private bool DiscardChangeNotificationDetailsForCCM(int changeNotificationDetailID)
        {
            ChangeNotificationDetail item = changeNotificationRepository.Find(cd => cd.ChangeNotificationDetailID == changeNotificationDetailID && (cd.SectionName == "CCM Appointment" || cd.SectionName == "Cancellation of Credentialing Committee Meeting"));
            if (item != null)
            {
                changeNotificationRepository.Delete(item);
                changeNotificationRepository.Save();
            }
            return true;
        }
        public bool NotifyChangesForCCM()
        {
            var t = GetChangeNotificationDetails();
            var changeDetails = t.Where(x => x.SectionName == "CCM Appointment" || x.SectionName == "Cancellation of Credentialing Committee Meeting").ToList(); ;

            if (changeDetails == null || changeDetails.Count() == 0)
                return false;



            var profileIds = (from cd in changeDetails
                              select cd.ProfileID).Distinct();

            foreach (var profileID in profileIds)
            {
                List<ChangeNotificationDetail> cDetails = changeDetails.Where(cd => cd.ProfileID == profileID).Select(cd => cd).ToList<ChangeNotificationDetail>();
                foreach (var tempData in cDetails)
                {
                    EMailModel emailModel = new EMailModel();
                    if (tempData.SectionName == "CCM Appointment")
                    {
                        emailModel = getEmailModelForCCM(tempData);
                    }
                    else
                    {
                        emailModel = getEmailModelForCCMCancelation(tempData);
                    }

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
                        DiscardChangeNotificationDetailsForCCM(tempData.ChangeNotificationDetailID);
                    }
                }

            }
            return true;
        }


        public bool NotifyChanges()
        {
            var changeDetails = (from cnd in GetChangeNotificationDetails()
                                 where cnd.ActionPerformed != "CCM Appointment"
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
            emailModel.To = changeDetails.First().ProviderEmailID;
            //emailModel.Cc ="cco@accesshealthcarellc.net";
            //emailModel.To = "aryadevi.k@pratian.com";
            emailModel.Cc = "";
            emailModel.Bcc = "";
            emailModel.From = System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"];

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
            mailBody.Append("Credaxis Healthcare Physicians, LLC<br/>");
            mailBody.Append("(352) 799-0046<br/>");
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"]);
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["DisclaimerMessage"]);
            //mailBody.Append("<br/><br/><b>This is a system generated email, please do not respond. Send all inquiries directly to credentialing@accesshealthcarellc.net</b>");
            //mailBody.Append("<br/><br/><b>This is a system generated email, please do not respond. Send all inquiries directly to credentialing@credaxis.com");
            //mailBody.Append(" and for call inquiries, our Credentialing Specialists are available to assist with all questions Monday- Friday 8a.m.-5PM and you may contact us via telephone at 352-796-9994.");
            mailBody.Append("</b>");
            emailModel.Body = mailBody.ToString();
            return emailModel;
        }
        private EMailModel getEmailModelForCCM(ChangeNotificationDetail changeDetails)
        {
            EMailModel emailModel = new EMailModel();
            emailModel.Subject = "CCM Appointment";
            emailModel.To = changeDetails.ProviderEmailID;
            //emailModel.Cc ="prakash@pratian.com";
            //emailModel.To = "aryadevi.k@pratian.com";
            emailModel.Cc = "";
            emailModel.Bcc = "";
            emailModel.From = System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"];
            StringBuilder mailBody = new StringBuilder();
            mailBody.Append("<b>");
            mailBody.Append(changeDetails.ProviderFullName + ",");
            mailBody.Append("</b></br></br>");
            mailBody.Append("<p>Greetings!  You have been invited to attend a Credentialing Committee meeting on " + DateTime.Parse(changeDetails.DateTime.ToString().Split(' ')[0]).ToString("MM/dd/yyyy") + ".  Please log into CredAxis to participate.  Directions are listed below.</p>");
            mailBody.Append("<br/>");
            mailBody.Append("<br/>");
            mailBody.Append("<p>Link:  https://ahc.credaxis.com</p>");
            mailBody.Append("<br/>");
            mailBody.Append("<ol>");
            mailBody.Append("<li>");
            mailBody.Append("Login with your credential");
            mailBody.Append("</li>");
            mailBody.Append("<li>");
            mailBody.Append("Go for Credential Action List(Cred/Decred)");
            mailBody.Append("</li>");
            mailBody.Append("<li>");
            mailBody.Append("Select a purticular Credentialing Profile for approval");
            mailBody.Append("</li>");
            mailBody.Append("</ol>");
            mailBody.Append("<br/><br/>");
            mailBody.Append("<p>If you have any questions, please don’t hesitate to contact us.  Your cooperation is greatly appreciated.</p>");
            mailBody.Append("<br/>");
            mailBody.Append("<br/>");
            mailBody.Append("Respectfully,<br/><br/>");
            mailBody.Append("Credentialing Dept.<br/>");
            mailBody.Append("Credaxis Management Co., LLC<br/>");
            mailBody.Append("14690 Spring Hill Dr. Suite 101<br/>");
            mailBody.Append("Spring Hill FL, 34609<br/>");
            mailBody.Append("352-799-0046<br/>");
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"]);
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["DisclaimerMessage"]);
            //mailBody.Append("<br/><br/><b>This is a system generated email, please do not respond. Send all inquiries directly to credentialing@accesshealthcarellc.net</b>");
            //mailBody.Append("<br/><br/><b>This is a system generated email, please do not respond. Send all inquiries directly to credentialing@credaxis.com");
            //mailBody.Append(" and for call inquiries, our Credentialing Specialists are available to assist with all questions Monday- Friday 8a.m.-5PM and you may contact us via telephone at 352-796-9994.</b>");
            mailBody.Append("<br/>");
            mailBody.Append("<br/>");
            emailModel.Body = mailBody.ToString();
            return emailModel;
        }
        private EMailModel getEmailModelForCCMCancelation(ChangeNotificationDetail changeDetails)
        {
            EMailModel emailModel = new EMailModel();
            emailModel.Subject = "Cancellation of Credentialing Committee Meeting";
            emailModel.To = changeDetails.ProviderEmailID;
            //emailModel.Cc ="cco@accesshealthcarellc.net";
            //emailModel.To = "aryadevi.k@pratian.com";
            emailModel.Cc = "";
            emailModel.Bcc = "";
            emailModel.From = System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"];
            StringBuilder mailBody = new StringBuilder();
            mailBody.Append("<b>");
            mailBody.Append(changeDetails.ProviderFullName + ",");
            mailBody.Append("</b></br></br>");
            mailBody.Append("<p>Please be advised the Credentialing Committee Meeting scheduled for " + DateTime.Parse(changeDetails.DateTime.ToString().Split(' ')[0]).ToString("MM/dd/yyyy") + " has been cancelled.</p>");
            mailBody.Append("<br/>");
            mailBody.Append("<br/>");
            mailBody.Append("<p>There is no action required by you at this time.  When the meeting has been rescheduled, you will be notified via email.</p>");
            mailBody.Append("<br/>");
            mailBody.Append("<br/>");
            mailBody.Append("<p>If you have any questions, please don’t hesitate to contact us.  We apologize for any inconvenience.</p>");
            mailBody.Append("<br/><br/>");
            mailBody.Append("Respectfully,<br/><br/>");
            mailBody.Append("Credentialing Dept.<br/>");
            mailBody.Append("Credaxis Management Co., LLC<br/>");
            mailBody.Append("14690 Spring Hill Dr. Suite 101<br/>");
            mailBody.Append("Spring Hill FL, 34609<br/>");
            mailBody.Append("352-799-0046<br/>");
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
        #region CCM APPointment Notification

        /// <summary>
        /// Author : Santosh Kumar Senapati
        /// </summary>
        /// <param name="profileIds"></param>
        /// <param name="CCMIds"></param>
        /// <param name="AppointmentDate"></param>
        /// <returns></returns>
        public async Task SaveNotificationDetailsAsyncForCCM(List<int?> profileIds, List<int> CCMIds, string AppointmentDate, string AppointmentDateTOBESAVED, string ActionPerformedBy)
        {
            try
            {
                int flag = 0;
                var AppointmentDates = GetAllAppointmentsDates();
                foreach (DateTime data in AppointmentDates)
                {
                    var res = data.ToString().Split('/');
                    var AppointmentDateString = AppointmentDate.Split('/');
                    // res[1] is month and res [0] is day
                    if (res[0] == AppointmentDateString[0] && res[1] == AppointmentDateString[1] && data.Year.ToString() == AppointmentDateString[2])
                    {
                        flag = 1;
                        break;
                    }
                }

                var ProviderList = await profileRepository.GetAllAsync("PersonalDetail,ContactDetail,OtherIdentificationNumber");
                var CDUserList = await cdUserRepo.GetAllAsync("DashboardNotifications");
                foreach (int profileId in profileIds)
                {
                    if (flag == 0)
                    {
                        flag++;
                        if (ProviderList.Any(x => x.ProfileID == profileId))
                        {
                            var profile = ProviderList.FirstOrDefault(x => x.ProfileID == profileId);
                            string providerFullName = "";
                            if (profile.PersonalDetail != null)
                            {
                                string providerFirstName = (profile.PersonalDetail.FirstName.Contains("Not Available")) ? "" : profile.PersonalDetail.FirstName;
                                string providerMidleName = "";
                                if (profile.PersonalDetail.MiddleName != null)
                                {
                                    providerMidleName = (profile.PersonalDetail.MiddleName.Contains("Not Available")) ? "" : profile.PersonalDetail.MiddleName;
                                }
                                string providerLastName = (profile.PersonalDetail.LastName.Contains("Not Available")) ? "" : profile.PersonalDetail.LastName;
                                string providerSolutation = (profile.PersonalDetail.Salutation.Contains("Not Available")) ? "" : profile.PersonalDetail.Salutation; ;
                                providerFullName = providerSolutation + " " + providerFirstName + " " + providerMidleName + " " + providerLastName;
                            }
                            foreach (int CCMId in CCMIds)
                            {
                                if (CDUserList.Any(x => x.CDUserID == CCMId))
                                {
                                    var CCM = CDUserList.FirstOrDefault(x => x.CDUserID == CCMId);
                                    if (CCM.ProfileId != null)
                                    {
                                        ChangeNotificationDetail changeNotificationDetail = new ChangeNotificationDetail((int)CCM.ProfileId, ActionPerformedBy, "CCM Appointment", "Scheduled");
                                        var CCMProfile = ProviderList.FirstOrDefault(x => x.ProfileID == (int)CCM.ProfileId);
                                        string CCMProviderFullName = "";
                                        if (CCMProfile.PersonalDetail != null)
                                        {
                                            string CCMProviderFirstName = (CCMProfile.PersonalDetail.FirstName.Contains("Not Available")) ? "" : CCMProfile.PersonalDetail.FirstName;
                                            string CCMProviderMidleName = "";
                                            if (CCMProfile.PersonalDetail.MiddleName != null)
                                            {
                                                CCMProviderMidleName = (CCMProfile.PersonalDetail.MiddleName.Contains("Not Available")) ? "" : CCMProfile.PersonalDetail.MiddleName;
                                            }
                                            string CCMProviderLastName = (CCMProfile.PersonalDetail.LastName.Contains("Not Available")) ? "" : CCMProfile.PersonalDetail.LastName;
                                            string CCMProviderSolutation = (CCMProfile.PersonalDetail.Salutation.Contains("Not Available")) ? "" : CCMProfile.PersonalDetail.Salutation; ;
                                            CCMProviderFullName = CCMProviderSolutation + " " + CCMProviderFirstName + " " + CCMProviderMidleName + " " + CCMProviderLastName;
                                        }

                                        if (CCMProfile.ContactDetail != null && CCMProfile.ContactDetail.EmailIDs != null)
                                        {
                                            changeNotificationDetail.ProviderEmailID = profileRepository.Find(changeNotificationDetail.ProfileID).ContactDetail.EmailIDs.First().EmailAddress;
                                        }
                                        if (profile.OtherIdentificationNumber != null)
                                        {
                                            changeNotificationDetail.NPINumber = CCMProfile.OtherIdentificationNumber.NPINumber;
                                        }
                                        changeNotificationDetail.ProviderFullName = CCMProviderFullName;
                                        changeNotificationDetail.DateTime = DateTime.ParseExact(AppointmentDateTOBESAVED, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                        changeNotificationRepository.Create(changeNotificationDetail);
                                        await changeNotificationRepository.SaveAsync();
                                    }
                                    else
                                    {
                                        ChangeNotificationDetail changeNotificationDetail = new ChangeNotificationDetail(CCM.ProfileId, ActionPerformedBy, "CCM Appointment", "Scheduled");
                                        var CCMProfile = uow.GetGenericRepository<ProfileUser>().Find(x => x.CDUserID == CCM.CDUserID);
                                        if (CCMProfile != null)
                                        {
                                            changeNotificationDetail.ProviderEmailID = CCMProfile.Email;
                                            changeNotificationDetail.NPINumber = null;
                                            changeNotificationDetail.ProviderFullName = CCMProfile.Name;
                                            changeNotificationDetail.DateTime = DateTime.ParseExact(AppointmentDateTOBESAVED, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                            changeNotificationRepository.Create(changeNotificationDetail);
                                            await changeNotificationRepository.SaveAsync();
                                        }

                                    }
                                    if (CCM.DashboardNotifications == null)
                                    {
                                        CCM.DashboardNotifications = new List<UserDashboardNotification>();
                                    }
                                    CCM.DashboardNotifications.Add(new UserDashboardNotification
                                    {
                                        StatusType = Entities.MasterData.Enums.StatusType.Active,
                                        AcknowledgementStatusType = Entities.MasterData.Enums.AcknowledgementStatusType.Unread,
                                        Action = "Appointment Schedule",
                                        ActionPerformed = "Appointment Scheduled" + " on " + AppointmentDate + " By",
                                        ActionPerformedByUser = ActionPerformedBy,
                                        RedirectURL = "/Credentialing/CCM/Index"
                                    });
                                    cdUserRepo.Update(CCM);
                                    await cdUserRepo.SaveAsync();
                                }

                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        public List<Object> GetAllAppointmentsDates()
        {
            try
            {
                var includeProperties = "CredentialingLogs,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians,CredentialingLogs.CredentialingActivityLogs,Profile.PersonalDetail,Profile.PersonalDetail.ProviderTitles.ProviderType,Profile.SpecialtyDetails.Specialty,Plan,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists";
                var ListOfCredentialingInfo = credInfo.GetAll(includeProperties);

                List<CredentialingInfo> credentialList = ListOfCredentialingInfo.ToList();
                var updatedOject = new List<Object>();
                foreach (CredentialingInfo CredData in credentialList)
                {

                    CredentialingLog credentialingLog = CredData.CredentialingLogs.OrderByDescending(c => c.LastModifiedDate).FirstOrDefault();
                    if (credentialingLog != null)
                    {
                        CredData.CredentialingLogs = new List<CredentialingLog>();
                        CredData.CredentialingLogs.Add(credentialingLog);
                        if (credentialingLog.CredentialingActivityLogs != null)
                        {
                            if (!credentialingLog.CredentialingActivityLogs.Any(a => a.ActivityType == CD.Entities.MasterData.Enums.ActivityType.CCMAppointment && a.ActivityStatusType == AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed))
                            {
                                if (credentialingLog.CredentialingActivityLogs.Any(a => a.ActivityType == CD.Entities.MasterData.Enums.ActivityType.CCMAppointment && a.ActivityStatusType == AHC.CD.Entities.MasterData.Enums.ActivityStatusType.InProgress))
                                {
                                    if (credentialingLog.CredentialingAppointmentDetail.CredentialingAppointmentSchedule != null)
                                    {
                                        if (credentialingLog.CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate != null)
                                        {
                                            updatedOject.Add(credentialingLog.CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                return updatedOject;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CancelMeetingNotificationAsyncForCCM(List<int> profileIds, List<int> CCMIds, string AppointmentDate, string AppointmentDateTOBEREMOVED, string ActionPerformedBy)
        {
            try
            {
                int flag = 0;
                var AppointmentDates = GetAllAppointmentsDates();
                var AppointmentDateString = AppointmentDate.Split('/');

                foreach (DateTime data in AppointmentDates)
                {
                    var res = data.ToString().Split('/');
                    if (res[0] == AppointmentDateString[0] && res[1] == AppointmentDateString[1] && data.Year.ToString() == AppointmentDateString[2])
                    {
                        flag = 1;
                        break;
                    }
                }

                var ProviderList = await profileRepository.GetAllAsync("PersonalDetail,ContactDetail,OtherIdentificationNumber");
                var CDUserList = await cdUserRepo.GetAllAsync("DashboardNotifications");
                foreach (int profileId in profileIds)
                {
                    if (flag == 0)
                    {
                        flag++;
                        if (ProviderList.Any(x => x.ProfileID == profileId))
                        {
                            var profile = ProviderList.FirstOrDefault(x => x.ProfileID == profileId);
                            string providerFullName = "";
                            if (profile.PersonalDetail != null)
                            {
                                string providerFirstName = (profile.PersonalDetail.FirstName.Contains("Not Available")) ? "" : profile.PersonalDetail.FirstName;
                                string providerMidleName = "";
                                if (profile.PersonalDetail.MiddleName != null)
                                {
                                    providerMidleName = (profile.PersonalDetail.MiddleName.Contains("Not Available")) ? "" : profile.PersonalDetail.MiddleName;
                                }
                                string providerLastName = (profile.PersonalDetail.LastName.Contains("Not Available")) ? "" : profile.PersonalDetail.LastName;
                                string providerSolutation = (profile.PersonalDetail.Salutation.Contains("Not Available")) ? "" : profile.PersonalDetail.Salutation; ;
                                providerFullName = providerSolutation + " " + providerFirstName + " " + providerMidleName + " " + providerLastName;
                            }
                            foreach (int CCMId in CCMIds)
                            {
                                if (CDUserList.Any(x => x.CDUserID == CCMId))
                                {
                                    var CCM = CDUserList.FirstOrDefault(x => x.CDUserID == CCMId);
                                    if (CCM.ProfileId != null)
                                    {
                                        ChangeNotificationDetail changeNotificationDetail = new ChangeNotificationDetail((int)CCM.ProfileId, ActionPerformedBy, "Cancellation of Credentialing Committee Meeting", "canceled");
                                        var CCMProfile = ProviderList.FirstOrDefault(x => x.ProfileID == (int)CCM.ProfileId);
                                        string CCMProviderFullName = "";
                                        if (CCMProfile.PersonalDetail != null)
                                        {
                                            string CCMProviderFirstName = (CCMProfile.PersonalDetail.FirstName.Contains("Not Available")) ? "" : CCMProfile.PersonalDetail.FirstName;
                                            string CCMProviderMidleName = "";
                                            if (CCMProfile.PersonalDetail.MiddleName != null)
                                            {
                                                CCMProviderMidleName = (CCMProfile.PersonalDetail.MiddleName.Contains("Not Available")) ? "" : CCMProfile.PersonalDetail.MiddleName;
                                            }
                                            string CCMProviderLastName = (CCMProfile.PersonalDetail.LastName.Contains("Not Available")) ? "" : CCMProfile.PersonalDetail.LastName;
                                            string CCMProviderSolutation = (CCMProfile.PersonalDetail.Salutation.Contains("Not Available")) ? "" : CCMProfile.PersonalDetail.Salutation; ;
                                            CCMProviderFullName = CCMProviderSolutation + " " + CCMProviderFirstName + " " + CCMProviderMidleName + " " + CCMProviderLastName;
                                        }

                                        if (CCMProfile.ContactDetail != null && CCMProfile.ContactDetail.EmailIDs != null)
                                        {
                                            changeNotificationDetail.ProviderEmailID = profileRepository.Find(changeNotificationDetail.ProfileID).ContactDetail.EmailIDs.First().EmailAddress;
                                        }
                                        if (profile.OtherIdentificationNumber != null)
                                        {
                                            changeNotificationDetail.NPINumber = CCMProfile.OtherIdentificationNumber.NPINumber;
                                        }
                                        changeNotificationDetail.ProviderFullName = CCMProviderFullName;
                                        changeNotificationRepository.Create(changeNotificationDetail);
                                        await changeNotificationRepository.SaveAsync();
                                    }
                                    else
                                    {
                                        ChangeNotificationDetail changeNotificationDetail = new ChangeNotificationDetail(CCM.ProfileId, ActionPerformedBy, "Cancellation of Credentialing Committee Meeting", "canceled");
                                        var CCMProfile = uow.GetGenericRepository<ProfileUser>().Find(x => x.CDUserID == CCM.CDUserID);
                                        if (CCMProfile != null)
                                        {
                                            changeNotificationDetail.ProviderEmailID = CCMProfile.Email;
                                            changeNotificationDetail.NPINumber = null;
                                            changeNotificationDetail.ProviderFullName = CCMProfile.Name;
                                            changeNotificationDetail.DateTime = DateTime.ParseExact(AppointmentDateTOBEREMOVED, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                            changeNotificationRepository.Create(changeNotificationDetail);
                                            await changeNotificationRepository.SaveAsync();
                                        }
                                    }
                                    if (CCM.DashboardNotifications == null)
                                    {
                                        CCM.DashboardNotifications = new List<UserDashboardNotification>();
                                    }
                                    CCM.DashboardNotifications.Add(new UserDashboardNotification
                                    {
                                        StatusType = Entities.MasterData.Enums.StatusType.Active,
                                        AcknowledgementStatusType = Entities.MasterData.Enums.AcknowledgementStatusType.Unread,
                                        Action = "Cancellation of Credentialing Committee Meeting",
                                        ActionPerformed = "Appointment is canceled on " + AppointmentDate + " By",
                                        ActionPerformedByUser = ActionPerformedBy,
                                        RedirectURL = "/Credentialing/CCM/Index"
                                    });
                                    cdUserRepo.Update(CCM);
                                    await cdUserRepo.SaveAsync();
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task SaveNotificationDetailAsyncForCCO(ChangeNotificationDetail changeNotificationDetail, string ApprovalStatus, int credentialingAppointmentDetailID)
        {
            int? applicationId = null;
            var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
            List<CredentialingInfo> credentialingInfoes = credentialingInfoRepo.GetAll("CredentialingLogs.CredentialingAppointmentDetail").ToList();
            foreach (var credInfo in credentialingInfoes)
            {
                if (credInfo.CredentialingLogs.Count > 0 && credInfo.CredentialingLogs.Last().CredentialingAppointmentDetail != null && credInfo.CredentialingLogs.Last().CredentialingAppointmentDetail.CredentialingAppointmentDetailID == credentialingAppointmentDetailID)
                {
                    applicationId = credInfo.CredentialingInfoID;
                }
            }

            var ccoList = await cdUserRepo.GetAllAsync("CDRoles,CDRoles.CDRole,DashboardNotifications");
            foreach (CDUser TempccoList in ccoList)
            {
                foreach (var TempccoListForRoles in TempccoList.CDRoles)
                {
                    if (TempccoListForRoles.CDRole.Code == "CCO" || TempccoListForRoles.CDRole.Code == "CRA")
                    {
                        if (TempccoList.DashboardNotifications == null)
                        {
                            TempccoList.DashboardNotifications = new List<UserDashboardNotification>();
                        }
                        if (ApprovalStatus == "Approved")
                        {
                            TempccoList.DashboardNotifications.Add(new UserDashboardNotification
                            {
                                StatusType = Entities.MasterData.Enums.StatusType.Active,
                                AcknowledgementStatusType = Entities.MasterData.Enums.AcknowledgementStatusType.Unread,
                                Action = "CCM Approval Status",
                                ActionPerformed = "CCM approved the provider credentialing on " + DateTime.Now.ToString("MM/dd/yyyy"),
                                ActionPerformedByUser = changeNotificationDetail.ActionPerformedUser,
                                //RedirectURL = "/Credentialing/Initiation/CredentialingList"
                                RedirectURL = "/Credentialing/CnD/Application/" + applicationId
                            });
                        }
                        else
                        {
                            TempccoList.DashboardNotifications.Add(new UserDashboardNotification
                            {
                                StatusType = Entities.MasterData.Enums.StatusType.Active,
                                AcknowledgementStatusType = Entities.MasterData.Enums.AcknowledgementStatusType.Unread,
                                Action = "CCM Approval Status",
                                ActionPerformed = "CCM rejected the provider credentialing on " + DateTime.Now.ToString("MM/dd/yyyy"),
                                ActionPerformedByUser = changeNotificationDetail.ActionPerformedUser,
                                //RedirectURL = "/Credentialing/Initiation/CredentialingList"
                                RedirectURL = "/Credentialing/CnD/Application/" + applicationId
                            });
                        }

                        cdUserRepo.Update(TempccoList);
                        break;
                    }
                }
            }
            await cdUserRepo.SaveAsync();

        }
    }
}
