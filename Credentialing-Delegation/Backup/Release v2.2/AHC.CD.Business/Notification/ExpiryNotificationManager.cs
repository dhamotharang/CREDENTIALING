using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.Notification;
using AHC.MailService;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Notification
{
    public class ExpiryNotificationManager : IExpiryNotificationManager
    {
        private IUserRepository UserRepository { get; set; }
        private IProfileRepository ProfileRepository { get; set; }
        private IExpiryNotificationRepository ExpiryNotificationRepository { get; set; }
        private IEmailSender EmailSender { get; set; }

        public ExpiryNotificationManager(IUnitOfWork uow, IEmailSender emailSender)
        {
            this.UserRepository = uow.GetUserRepository();
            this.ProfileRepository = uow.GetProfileRepository();
            this.ExpiryNotificationRepository = uow.GetExpiryNotificationRepository();
            this.EmailSender = emailSender;
        }

        public async Task SaveExpiryNotificationAsync()
        {
            //Get the latest expiries
            IEnumerable<ExpiryNotificationDetail> expiries = await GetAllExpiryDetail();

            //Save it on the database
            ExpiryNotificationRepository.UpdateExpiryDetails(expiries);
            await ExpiryNotificationRepository.SaveAsync();
        }

        public async Task<IEnumerable<ExpiryNotificationDetail>> GetExpiries(string userId = null)
        {
            IEnumerable<ExpiryNotificationDetail> expiries = null;

            if (userId != null)
            {
                //get the roles with respect to the logged in user
                var user = await UserRepository.FindAsync(c => c.AuthenicateUserId.Equals(userId), "CDRoles.CDRole");

                //get the Expiries with respect to the roles
                expiries = await GetExpiryDetailAccordingToRole(user);
            }
            else
            {
                //expiries = await GetAllExpiryDetail();
                expiries = await ExpiryNotificationRepository.GetAllAsync();
            }

            return expiries;
        }

        private async Task<IEnumerable<ExpiryNotificationDetail>> GetExpiryDetailAccordingToRole(CDUser user)
        {
            if (user == null)
                return null;

            if (user.CDRoles.Any(r => r.CDRole.Code.Equals("CCO")))
            {
                return await GetAllExpiryDetail();
                //return await ExpiryNotificationRepository.GetAllAsync();
            }
            else if (user.CDRoles.Any(r => r.CDRole.Code.Equals("PRO")))
            {
                return await GetExpiryDetailForProvider(user);
            }
            else
                return null;
        }

        private async Task<IEnumerable<ExpiryNotificationDetail>> GetExpiryDetailForProvider(CDUser user)
        {
            var profile = await ProfileRepository.FindAsync(p => p.ProfileID == user.ProfileId && p.Status.Equals(StatusType.Active.ToString()), String.Join(",", GetIncludeProperties()));
            var Expiries = new List<ExpiryNotificationDetail>();
            Expiries.Add(FillExpiryInformation(profile));
            return Expiries;

            //return await ExpiryNotificationRepository.GetAsync(e => e.ProfileId == user.ProfileId);
        }

        private async Task<IEnumerable<ExpiryNotificationDetail>> GetAllExpiryDetail()
        {
            var profileList = await ProfileRepository.GetAsync(p => p.Status.Equals(StatusType.Active.ToString()), String.Join(",", GetIncludeProperties()));
            var Expiries = new List<ExpiryNotificationDetail>();

            foreach (var profile in profileList)
            {
                Expiries.Add(FillExpiryInformation(profile));
            }

            return Expiries;
        }

        private ExpiryNotificationDetail FillExpiryInformation(Entities.MasterProfile.Profile profile)
        {
            try
            {
                int numberOfDays = 180;

                var expiry = new ExpiryNotificationDetail()
                {
                    ProfileId = profile.ProfileID,
                    NPINumber = profile.OtherIdentificationNumber.NPINumber,
                    FirstName = profile.PersonalDetail.FirstName,
                    MiddleName = profile.PersonalDetail.MiddleName,
                    LastName = profile.PersonalDetail.LastName,
                    EmailAddress = profile.ContactDetail.EmailIDs.FirstOrDefault() != null ? profile.ContactDetail.EmailIDs.FirstOrDefault().EmailAddress : null,
                    ProviderLevel = profile.PersonalDetail.ProviderLevel.Name,
                    ProviderTitles = profile.PersonalDetail.ProviderTitles.Select(s => s.ProviderType.Title).ToList()
                };

                if (profile.StateLicenses != null && profile.StateLicenses.Count != 0)
                {
                    foreach (var stateLicense in profile.StateLicenses.Where(s => s.ExpiryDate.HasValue && s.ExpiryDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                    {
                        var stateLicenseExpiry = Mapper.Map<StateLicenseInformation, StateLicenseExpiry>(stateLicense);
                        stateLicenseExpiry.NotificationDate = AssignNotificationDate(stateLicenseExpiry.ExpiryDate);

                        var sls = "";
                        var temp = true;

                        if (stateLicense.StateLicenseStatus != null)
                        {
                            sls = stateLicense.StateLicenseStatus.Title;
                        }
                        
                        if (sls == "Cancelled" || sls == "Denied" || sls == "Inactive" || sls == "Revoked" || sls == "Surrendered" || sls == "Terminated")
                        {
                            temp = false;
                        }

                        if (stateLicense.StatusType == StatusType.Active && temp == true)
                        {
                            expiry.StateLicenseExpiries.Add(stateLicenseExpiry);
                        }
                    }
                }

                if (profile.FederalDEAInformations != null && profile.FederalDEAInformations.Count != 0)
                {
                    foreach (var deaLicense in profile.FederalDEAInformations.Where(s => s.ExpiryDate.HasValue && s.ExpiryDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                    {
                        var deaLicenseExpiry = Mapper.Map<FederalDEAInformation, DEALicenseExpiry>(deaLicense);
                        deaLicenseExpiry.NotificationDate = AssignNotificationDate(deaLicenseExpiry.ExpiryDate);

                        if (deaLicense.StatusType == StatusType.Active)
                        {
                            expiry.DEALicenseExpiries.Add(deaLicenseExpiry);
                        }
                    }
                }

                if (profile.CDSCInformations != null && profile.CDSCInformations.Count != 0)
                {
                    foreach (var cdsc in profile.CDSCInformations.Where(s => s.ExpiryDate.HasValue && s.ExpiryDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                    {
                        var cdscExpiry = Mapper.Map<CDSCInformation, CDSCInfoExpiry>(cdsc);
                        cdscExpiry.NotificationDate = AssignNotificationDate(cdscExpiry.ExpiryDate);

                        if (cdsc.StatusType == StatusType.Active)
                        {
                            expiry.CDSCInfoExpiries.Add(cdscExpiry);
                        }
                    }
                }

                if (profile.SpecialtyDetails != null && profile.SpecialtyDetails.Count != 0)
                {
                    foreach (var speciality in profile.SpecialtyDetails.Where(s => s.SpecialtyBoardCertifiedDetail != null && s.SpecialtyBoardCertifiedDetail.ExpirationDate.HasValue && s.SpecialtyBoardCertifiedDetail.ExpirationDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                    {
                        if (speciality.BoardCertifiedYesNoOption == YesNoOption.YES && speciality.StatusType == StatusType.Active)
                        {
                            expiry.SpecialtyDetailExpiries.Add(new SpecialtyDetailExpiry()
                            {
                                SpecialtyDetailID = speciality.SpecialtyDetailID,
                                SpecialtyName = speciality.Specialty.Name,
                                SpecialtyBoardName = speciality.SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name,
                                ExpiryDate = speciality.SpecialtyBoardCertifiedDetail.ExpirationDate.Value,
                                CertificateNumber = speciality.SpecialtyBoardCertifiedDetail.CertificateNumber,
                                NotificationDate = AssignNotificationDate(speciality.SpecialtyBoardCertifiedDetail.ExpirationDate.Value)
                            });
                        }
                    }
                }

                List<string> staffCategories = new List<string>();
                staffCategories.Add("Applied for Privileges");
                staffCategories.Add("Expelled");
                staffCategories.Add("Inactive");
                staffCategories.Add("Pending");
                staffCategories.Add("Privileges Without Membership");
                staffCategories.Add("Resigned");
                staffCategories.Add("Temporary");
                staffCategories.Add("Unknown");
                staffCategories.Add("Suspended");

                

                if (profile.HospitalPrivilegeInformation != null)
                {
                    if (profile.HospitalPrivilegeInformation.HasHospitalPrivilege.Equals(YesNoOption.YES.ToString()) && profile.HospitalPrivilegeInformation.HasHospitalPrivilege != null && profile.HospitalPrivilegeInformation.HasHospitalPrivilege.Count() != 0)
                    {
                        foreach (var hospiatalDetail in profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Where(s => s.AffiliationEndDate.HasValue && s.AffiliationEndDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                        {
                            if (hospiatalDetail.StatusType == StatusType.Active && hospiatalDetail.StaffCategory != null && !(staffCategories.Any(c => c.Equals(hospiatalDetail.StaffCategory.Title))))
                            {
                                expiry.HospitalPrivilegeExpiries.Add(new HospitalPrivilegeExpiry()
                                {
                                    HospitalPrivilegeDetailID = hospiatalDetail.HospitalPrivilegeDetailID,
                                    HospitalName = hospiatalDetail.Hospital.HospitalName,
                                    AffilicationStartDate = hospiatalDetail.AffilicationStartDate,
                                    AffiliationEndDate = hospiatalDetail.AffiliationEndDate.Value,
                                    NotificationDate = AssignNotificationDate(hospiatalDetail.AffiliationEndDate.Value)
                                });
                            }
                        }
                    }
                }

                if (profile.ProfessionalLiabilityInfoes != null && profile.ProfessionalLiabilityInfoes.Count != 0)
                {
                    foreach (var profLiability in profile.ProfessionalLiabilityInfoes.Where(s => s.ExpirationDate.HasValue && s.ExpirationDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                    {
                        if (profLiability.StatusType == StatusType.Active)
                        {
                            expiry.ProfessionalLiabilityExpiries.Add(new ProfessionalLiabilityExpiry()
                            {
                                ProfessionalLiabilityInfoID = profLiability.ProfessionalLiabilityInfoID,
                                PolicyNumber = profLiability.PolicyNumber,
                                InsuranceCarrier = profLiability.InsuranceCarrier.Name,
                                EffectiveDate = profLiability.EffectiveDate,
                                ExpirationDate = profLiability.ExpirationDate.Value,
                                NotificationDate = AssignNotificationDate(profLiability.ExpirationDate.Value)
                            });
                        }
                    }
                }

                if (profile.PracticeLocationDetails != null && profile.PracticeLocationDetails.Count != 0)
                {
                    foreach (var pracLocation in profile.PracticeLocationDetails.Where(p => p.WorkersCompensationInformation != null && p.WorkersCompensationInformation.ExpirationDate.HasValue && p.WorkersCompensationInformation.ExpirationDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                    {
                        if (pracLocation.WorkersCompensationInformation.StatusType == StatusType.Active)
                        {
                            expiry.WorkerCompensationExpiries.Add(new WorkerCompensationExpiry()
                            {
                                WorkersCompensationInformationID = pracLocation.WorkersCompensationInformation.WorkersCompensationInformationID,
                                WorkersCompensationNumber = pracLocation.WorkersCompensationInformation.WorkersCompensationNumber,
                                IssueDate = pracLocation.WorkersCompensationInformation.IssueDate,
                                ExpirationDate = pracLocation.WorkersCompensationInformation.ExpirationDate.Value,
                                NotificationDate = AssignNotificationDate(pracLocation.WorkersCompensationInformation.ExpirationDate.Value)
                            });
                        }
                    }
                }

                return expiry;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private DateTime? AssignNotificationDate(DateTime? expiryDate)
        {
            if (!expiryDate.HasValue)
                return null;

            var totalDays = (expiryDate.Value - DateTime.Now).TotalDays;

            if (totalDays >= 180)
                return expiryDate.Value.AddDays(-180);
            else if (totalDays >= 90)
                return expiryDate.Value.AddDays(-90);
            else if (totalDays >= 45)
                return expiryDate.Value.AddDays(-45);
            else if (totalDays >= 30)
                return expiryDate.Value.AddDays(-30);
            else if (totalDays >= 15)
                return expiryDate.Value.AddDays(-15);
            else if (totalDays >= 0)
                return DateTime.Now;
            else
                return null;

        }

        private static string[] GetIncludeProperties()
        {
            return new string[]
                {
                    //Specialty
                    "SpecialtyDetails.Specialty",
                    "SpecialtyDetails.SpecialtyBoardCertifiedDetail.SpecialtyBoard",

                    //hospital Privilege
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.Hospital", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalContactInfo", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalContactPerson", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.AdmittingPrivilege", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.StaffCategory",

                   //Professional Liability
                    "ProfessionalLiabilityInfoes.InsuranceCarrier",
                    "ProfessionalLiabilityInfoes.InsuranceCarrierAddress",

                    //State License
                    "StateLicenses.ProviderType",
                    "StateLicenses.StateLicenseStatus",

                    //Personal Detail
                    "PersonalDetail.ProviderTitles.ProviderType",
                    "PersonalDetail.ProviderLevel"
                };
        }

        public void NotifyExpiries()
        {
            var expiries = ExpiryNotificationRepository.GetAll();
            
            if (expiries == null || expiries.Count() == 0)
                return ;

            foreach (var expiry in expiries)
            {
                foreach (var stateLicenseExpiry in expiry.StateLicenseExpiries.Where(s => s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("State License");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "State License", stateLicenseExpiry.ExpiryDate.Value, (stateLicenseExpiry.ExpiryDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var deaLicense in expiry.DEALicenseExpiries.Where(s => s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("DEA License");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "DEA License", deaLicense.ExpiryDate.Value, (deaLicense.ExpiryDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var cdsc in expiry.CDSCInfoExpiries.Where(s => s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("DEA License");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "DEA License", cdsc.ExpiryDate.Value, (cdsc.ExpiryDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var specialty in expiry.SpecialtyDetailExpiries.Where(s => s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("DEA License");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "DEA License", specialty.ExpiryDate.Value, (specialty.ExpiryDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var hospital in expiry.HospitalPrivilegeExpiries.Where(s => s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("DEA License");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "DEA License", hospital.AffiliationEndDate.Value, (hospital.AffiliationEndDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var profLiability in expiry.ProfessionalLiabilityExpiries.Where(s => s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("DEA License");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "DEA License", profLiability.ExpirationDate.Value, (profLiability.ExpirationDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var workerComp in expiry.WorkerCompensationExpiries.Where(s => s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("DEA License");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "DEA License", workerComp.ExpirationDate.Value, (workerComp.ExpirationDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }
            }
        }

        private string GetFullName(ExpiryNotificationDetail expiry)
        {
            return expiry.FirstName + " " + expiry.MiddleName + " " + expiry.LastName;
        }

        private string GetEmailBody(string fullName, string licenseType, DateTime expiryDate, double totalDays)
        {
            StringBuilder mailBody = new StringBuilder();
            
            mailBody.Append("<b>Dear ");
            mailBody.Append(fullName);
            mailBody.Append("</b></br></br>");
            mailBody.Append("<p>This is to inform you that your ");
            mailBody.Append(licenseType);
            mailBody.Append(" is due to expire on ");
            mailBody.Append(expiryDate.Date.ToShortDateString());
            mailBody.Append(". Please renew this in the next ");
            mailBody.Append(totalDays);
            mailBody.Append(" days, to avoid any gaps in credentialing.");
            mailBody.Append("</p><br/><br/>");
            mailBody.Append("<br/><br/>");
            mailBody.Append("Thanking you,<br/>");
            mailBody.Append("Jeanine Martin<br/><br/>");
            mailBody.Append("Credentialing Coordinator<br/>");
            mailBody.Append("Access HealthCare LLC");

            return mailBody.ToString();
        }

        private string GetEmailSubject(string expiryType)
        {
            return "Renewal Notice for the " + expiryType;
        }

        private EMailModel GetEmailModel(ExpiryNotificationDetail expiry)
        {
            EMailModel emailModel = new EMailModel();
            emailModel.To = "jmartin@accesshealthcarellc.net";
            //emailModel.To = expiry.EmailAddress;
            //emailModel.Cc = "cco@accesshealthcarellc.net";
            return emailModel;
        }
    }
}
