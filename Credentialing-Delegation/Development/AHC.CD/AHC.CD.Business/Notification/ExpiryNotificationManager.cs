using AHC.CD.Business.Users;
using AHC.CD.Data.ADO.AspnetUser;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Data.Repository.TaskTracker;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.Notification;
using AHC.MailService;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace AHC.CD.Business.Notification
{
    public class ExpiryNotificationManager : IExpiryNotificationManager
    {
        private IUserRepository UserRepository { get; set; }
        private IProfileRepository ProfileRepository { get; set; }
        private IExpiryNotificationRepository ExpiryNotificationRepository { get; set; }
        private IEmailSender EmailSender { get; set; }
        private ITaskTrackerRepository TaskTrackerRepository { get; set; }
        private ITaskTrackerExpiryRepository TaskTrackerExpiryRepository { get; set; }
        private IUserRepository userRepository = null;
        private IUserDetails aspnetusersRepository = null;
        private IUnitOfWork uow = null;
        List<dynamic> ccos;
        static int taskcount;
        public ExpiryNotificationManager(IUnitOfWork uow, IEmailSender emailSender)
        {
            this.UserRepository = uow.GetUserRepository();
            this.ProfileRepository = uow.GetProfileRepository();
            this.ExpiryNotificationRepository = uow.GetExpiryNotificationRepository();
            this.EmailSender = emailSender;
            this.TaskTrackerRepository = uow.GetTaskTrackerRepository();
            this.TaskTrackerExpiryRepository = uow.GetTaskTrackerExpiryRepository();
            this.userRepository = uow.GetUserRepository();
            this.aspnetusersRepository = new UserDetails();
            this.uow = uow;

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

            if ((user.CDRoles.Any(r => r.CDRole.Code.Equals("CCO"))) || (user.CDRoles.Any(r => r.CDRole.Code.Equals("CRA"))))
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
            var ExpiryInformation = FillExpiryInformation(profile);
            if (ExpiryInformation != null)
                Expiries.Add(ExpiryInformation);
            var UpcomingRecredentialRepository = uow.GetGenericRepository<CredentialingInfo>();

            var credentialingInfo = await UpcomingRecredentialRepository.FindAsync(c => c.ProfileID == user.ProfileId, "Profile");
            if (credentialingInfo != null)
            {
                var UpComingRecredentialInformation = FillUpComingRecredentialInformation(credentialingInfo);
                if (UpComingRecredentialInformation != null)
                    Expiries.Add(UpComingRecredentialInformation);
            }
            return Expiries;

            //return await ExpiryNotificationRepository.GetAsync(e => e.ProfileId == user.ProfileId);
        }

        private async Task<IEnumerable<ExpiryNotificationDetail>> GetAllExpiryDetail()
        {
            var profileList = await ProfileRepository.GetAsync(p => p.Status.Equals(StatusType.Active.ToString()), String.Join(",", GetIncludeProperties()));
            var UpcomingRecredentialRepository = uow.GetGenericRepository<CredentialingInfo>();
            var credentialingInfoList = await UpcomingRecredentialRepository.GetAsync(p => p.Profile.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), "CredentialingContractRequests,CredentialingContractRequests.ContractGrid,CredentialingContractRequests.ContractGrid.BusinessEntity,CredentialingContractRequests.ContractGrid.Report,Profile,Profile.PersonalDetail,Profile.SpecialtyDetails,Profile.SpecialtyDetails.Specialty,Profile.PersonalDetail.ProviderLevel,Profile.PersonalDetail.ProviderTitles,Profile.PersonalDetail.ProviderTitles.ProviderType");
            var Expiries = new List<ExpiryNotificationDetail>();

            foreach (var profile in profileList)
            {
                Expiries.Add(FillExpiryInformation(profile));

            }
            foreach (var cred in credentialingInfoList)
            {
                var res = FillUpComingRecredentialInformation(cred);
                if (res != null)
                {
                if (res.UpComingRecredentials != null && res.UpComingRecredentials.Count != 0)
                {
                    Expiries.Add(res);
                }
            }
            }

            return Expiries;
        }

        private ExpiryNotificationDetail FillExpiryInformation(Entities.MasterProfile.Profile profile)
        {
            try
            {
                int numberOfDays = 90;
                int numberOfDaysForHospitalPrivileges = 180;
                string providerrelationship = "";
                if (profile.ContractInfoes != null && profile.ContractInfoes.Count != 0)
                {
                    var contractinfo = profile.ContractInfoes.Where(s => s.ContractStatus == "Accepted").FirstOrDefault();
                    if (contractinfo != null)
                    {
                        providerrelationship = contractinfo.ProviderRelationship;
                    }
                }
                var expiry = new ExpiryNotificationDetail()
                {
                    ProfileId = profile.ProfileID,
                    ProfileStatus = profile.Status,
                    NPINumber = profile.OtherIdentificationNumber.NPINumber,
                    ProviderRelationship = providerrelationship,
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
                        if (stateLicense.Status == "Active")
                        {
                            var stateLicenseExpiry = Mapper.Map<StateLicenseInformation, StateLicenseExpiry>(stateLicense);
                            stateLicenseExpiry.NotificationDate = AssignNotificationDate(stateLicenseExpiry.ExpiryDate);

                            var sls = "";
                            var temp = true;

                            if (stateLicense.StateLicenseStatus != null)
                            {
                                sls = stateLicense.StateLicenseStatus.Title;
                            }

                            if (sls == "Cancelled" || sls == "Denied" || sls == "Expired" || sls == "Inactive" || sls == "Lapsed" || sls == "Pending" || sls == "Revoked" || sls == "Suspended" || sls == "Surrendered" || sls == "Terminated" || sls == "Not Available")
                            {
                                temp = false;
                            }

                            if (temp == true)
                            {
                                expiry.StateLicenseExpiries.Add(stateLicenseExpiry);
                            }
                        }
                    }
                }

                if (profile.FederalDEAInformations != null && profile.FederalDEAInformations.Count != 0)
                {
                    foreach (var deaLicense in profile.FederalDEAInformations.Where(s => s.ExpiryDate.HasValue && s.ExpiryDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                    {
                        var deaLicenseExpiry = Mapper.Map<FederalDEAInformation, DEALicenseExpiry>(deaLicense);
                        deaLicenseExpiry.NotificationDate = AssignNotificationDate(deaLicenseExpiry.ExpiryDate);

                        if (deaLicense.StatusType == StatusType.Active && deaLicense.IsInGoodStanding == "YES")
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
                                SpecialtyName = speciality.Specialty == null ? "" : speciality.Specialty.Name,
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
                        foreach (var hospiatalDetail in profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Where(s => s.AffiliationEndDate.HasValue && s.AffiliationEndDate.Value.AddDays(-numberOfDaysForHospitalPrivileges) <= DateTime.Now))
                        {
                            if (hospiatalDetail.StatusType == StatusType.Active && hospiatalDetail.StaffCategory != null && !(staffCategories.Any(c => c.Equals(hospiatalDetail.StaffCategory.Title))))
                            {
                                if (hospiatalDetail.StaffCategory.Title != null)
                                {
                                    if ((hospiatalDetail.StaffCategory.Title != "Inactive") && (hospiatalDetail.StaffCategory.Title != "Expelled") && (hospiatalDetail.StaffCategory.Title != "Pending") && (hospiatalDetail.StaffCategory.Title != "Resigned"))
                                    {
                                        if (System.Configuration.ConfigurationManager.AppSettings["DashBoardStatus"] == "true")
                                        {
                                            if ((hospiatalDetail.StaffCategory.Title != "Suspended") && (hospiatalDetail.StaffCategory.Title != "Applied for Privileges") && (hospiatalDetail.StaffCategory.Title != "None") && (hospiatalDetail.StaffCategory.Title != "Unknown"))
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

                                        else if (hospiatalDetail.AdmittingPrivilege != null && hospiatalDetail.AdmittingPrivilege.Title != "None")
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

                if (profile.MedicareInformations != null && profile.MedicareInformations.Count != 0)
                {
                    foreach (var MedicareInfo in profile.MedicareInformations.Where(p => p != null && p.ExpirationDate.HasValue && p.ExpirationDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                    {
                        if (MedicareInfo.StatusType == StatusType.Active)
                        {
                            expiry.MedicareExpiries.Add(new MedicareExpiry()
                            {
                                MedicareInformationID = MedicareInfo.MedicareInformationID,
                                LicenseNumber = MedicareInfo.LicenseNumber,
                                IssueDate = MedicareInfo.IssueDate,
                                ExpirationDate = MedicareInfo.ExpirationDate.Value,
                                NotificationDate = AssignNotificationDate(MedicareInfo.ExpirationDate.Value)
                            });
                        }
                    }
                }

                if (profile.MedicaidInformations != null && profile.MedicaidInformations.Count != 0)
                {
                    foreach (var MedicaidInfo in profile.MedicaidInformations.Where(p => p != null && p.ExpirationDate.HasValue && p.ExpirationDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                    {
                        if (MedicaidInfo.StatusType == StatusType.Active)
                        {
                            expiry.MedicaidExpiries.Add(new MedicaidExpiry()
                            {
                                MedicaidInformationID = MedicaidInfo.MedicaidInformationID,
                                LicenseNumber = MedicaidInfo.LicenseNumber,
                                IssueDate = MedicaidInfo.IssueDate,
                                ExpirationDate = MedicaidInfo.ExpirationDate.Value,
                                NotificationDate = AssignNotificationDate(MedicaidInfo.ExpirationDate.Value)
                            });
                        }
                    }
                }

                if (profile.OtherIdentificationNumber != null)
                {
                    if ((profile.OtherIdentificationNumber.NextAttestationDate != null) && (profile.OtherIdentificationNumber.LastCAQHAttestationDate != null) && (profile.OtherIdentificationNumber.NextAttestationDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                    {

                        expiry.CAQHExpiries.Add(new CAQHExpiry()
                           {
                               CAQHNumber = profile.OtherIdentificationNumber.CAQHNumber,
                               NextAttestationDate = profile.OtherIdentificationNumber.NextAttestationDate
                           });
                    }
                }


                return expiry;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private ExpiryNotificationDetail FillUpComingRecredentialInformation(Entities.Credentialing.Loading.CredentialingInfo credentialingInfo)
        {
            try
            {
                int numberOfDays = 90;
                var PlanRepository = uow.GetGenericRepository<Plan>();
                var PlansList = PlanRepository.GetAll();
                var LOBRepository = uow.GetGenericRepository<LOB>();
                var LOBList = LOBRepository.GetAll();
                string PName = null;
                string LobName = null;
                string ProviderLevel = null;
                string NPI = null;
                List<string> ProviderTitles = null;
                List<string> Speciality = null;
                string IPA = null;

                if (credentialingInfo != null && credentialingInfo.Profile != null && credentialingInfo.Profile.PersonalDetail != null)
                {
                    if (credentialingInfo.Profile.PersonalDetail.ProviderLevel != null)
                    {
                        ProviderLevel = credentialingInfo.Profile.PersonalDetail.ProviderLevel.Name;
                    }
                    if (credentialingInfo.Profile.PersonalDetail.ProviderTitles != null)
                    {
                        for (int i = 0; i < credentialingInfo.Profile.PersonalDetail.ProviderTitles.Count; i++)
                        {
                            if (credentialingInfo.Profile.PersonalDetail.ProviderTitles.ElementAt(i).ProviderType != null)
                            {
                                ProviderTitles = credentialingInfo.Profile.PersonalDetail.ProviderTitles.Select(s => s.ProviderType.Title).ToList();
                            }
                        }
                    }
                    if (credentialingInfo.Profile.OtherIdentificationNumber != null)
                    {
                        NPI = credentialingInfo.Profile.OtherIdentificationNumber.NPINumber;
                    }
                }


                var expiry = new ExpiryNotificationDetail()
                {
                    ProfileId = (int)credentialingInfo.ProfileID,
                    NPINumber = NPI,
                    FirstName = credentialingInfo.Profile.PersonalDetail.FirstName,
                    MiddleName = credentialingInfo.Profile.PersonalDetail.MiddleName,
                    LastName = credentialingInfo.Profile.PersonalDetail.LastName,
                    EmailAddress = credentialingInfo.Profile.ContactDetail.EmailIDs.FirstOrDefault() != null ? credentialingInfo.Profile.ContactDetail.EmailIDs.FirstOrDefault().EmailAddress : null,
                    ProviderLevel = ProviderLevel,
                    ProviderTitles = ProviderTitles,
                };
                if (credentialingInfo.CredentialingContractRequests != null && credentialingInfo.CredentialingContractRequests.Count != 0)
                {

                    foreach (var conreq in credentialingInfo.CredentialingContractRequests)
                    {
                        //foreach (var contractgrid in conreq.ContractGrid.Where(p => p != null && p.Report.ReCredentialingDate.HasValue && p.Report.ReCredentialingDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                        foreach (var contractgrid in conreq.ContractGrid)
                        {

                            if (contractgrid.Report != null)
                            {
                                foreach (var plan in PlansList)
                                {
                                    if (credentialingInfo.PlanID == plan.PlanID)
                                    {
                                        PName = plan.PlanName;
                                    }
                                }
                            }
                            if (contractgrid.BusinessEntity != null)
                            {
                                IPA = contractgrid.BusinessEntity.GroupName;
                            }
                            foreach (var lob in LOBList)
                            {
                                if (contractgrid.LOBID == lob.LOBID)
                                {
                                    LobName = lob.LOBName;
                                }
                            }
                            for (int i = 0; i < credentialingInfo.Profile.SpecialtyDetails.Count; i++)
                            {
                                if (credentialingInfo.Profile.SpecialtyDetails != null)
                                {
                                    Speciality = credentialingInfo.Profile.SpecialtyDetails.Select(s => s.Specialty.Name).ToList();
                                }
                            }


                            //if (credentialingInfo.Profile.Status == "Active")
                            //{
                           
                            if (conreq.ContractRequestStatus != null && contractgrid.Report.ParticipatingStatus != null)
                            {
                                contractgrid.Report.ParticipatingStatus = contractgrid.Report.ParticipatingStatus.Trim();
                                if ((conreq.ContractRequestStatus == "Active") && (contractgrid.ContractGridStatus == "Active") && (contractgrid.Status == "Active") && (conreq.Status == "Active") && (contractgrid.Report.ReCredentialingDate.HasValue) && (contractgrid.Report.ReCredentialingDate.Value.AddDays(-numberOfDays) <= DateTime.Now))
                                {
                                    if (System.Configuration.ConfigurationManager.AppSettings["PrintProfileAsPDF"] == "true")
                                    {
                                        
                                        if ((contractgrid.Report.ParticipatingStatus == "PROVIDER LINKED") || (contractgrid.Report.ParticipatingStatus == "PANEL CLOSE NOT ALLOWED FOR MID LEVELS") || (contractgrid.Report.ParticipatingStatus == "PAR") || (contractgrid.Report.ParticipatingStatus == "PAR - PENDING PROVIDER ID") || (contractgrid.Report.ParticipatingStatus == "PANEL CLOSED") || (contractgrid.Report.ParticipatingStatus == "TERM PROCESSING") || (contractgrid.Report.ParticipatingStatus == "PAR VIA TIN") || (contractgrid.Report.ParticipatingStatus == "Panel Re-Opening in process") || (contractgrid.Report.ParticipatingStatus == "PANEL CLOSE IN PROCESS") || (contractgrid.Report.ParticipatingStatus == "RECREDENTIALING IN PROCESS"))
                                        {
                                            expiry.UpComingRecredentials.Add(new UpcomingRecredential()
                                            {
                                                //PlanID = credentialingInfo.PlanID,
                                                //LOBID = contractgrid.LOBID,
                                                PlanName = PName,
                                                PlanID = credentialingInfo.PlanID,
                                                LOBName = LobName,
                                                //LOBID=contractgrid.LOBID,
                                                ParticipatingStatus = contractgrid.Report.ParticipatingStatus,
                                                GroupID = contractgrid.Report.GroupID,
                                                ProviderID = contractgrid.Report.ProviderID,
                                                InitialCredentialingDate = contractgrid.InitialCredentialingDate,
                                                InitiatedDate = contractgrid.Report.InitiatedDate,
                                                PanelStatus = contractgrid.Report.PanelStatus,
                                                TerminationDate = contractgrid.Report.TerminationDate,
                                                ReCredentialingDate = contractgrid.Report.ReCredentialingDate,
                                                CredentialingInfoID = credentialingInfo.CredentialingInfoID,
                                                CredentialingContractRequestID = conreq.CredentialingContractRequestID,
                                                SpecialityName = Speciality,
                                                IPA = IPA,
                                                ContractGridID = contractgrid.ContractGridID
                                            });
                                        }
                                    }

                                    else if ((contractgrid.Report.ParticipatingStatus == "APPLIED") || (contractgrid.Report.ParticipatingStatus == "IN PROCESS PER PAYER") || (contractgrid.Report.ParticipatingStatus == "PAR") || (contractgrid.Report.ParticipatingStatus == "ON HOLD") || (contractgrid.Report.ParticipatingStatus == "RECREDENTIALING IN PROCESS") || (contractgrid.Report.ParticipatingStatus == "TERM PROCESSING"))
                                    {
                                        expiry.UpComingRecredentials.Add(new UpcomingRecredential()
                                       {
                                           //PlanID = credentialingInfo.PlanID,
                                           //LOBID = contractgrid.LOBID,
                                           PlanName = PName,
                                           PlanID = credentialingInfo.PlanID,
                                           LOBName = LobName,
                                           //LOBID=contractgrid.LOBID,
                                           ParticipatingStatus = contractgrid.Report.ParticipatingStatus,
                                           GroupID = contractgrid.Report.GroupID,
                                           ProviderID = contractgrid.Report.ProviderID,
                                           InitialCredentialingDate = contractgrid.InitialCredentialingDate,
                                           InitiatedDate = contractgrid.Report.InitiatedDate,
                                           PanelStatus = contractgrid.Report.PanelStatus,
                                           TerminationDate = contractgrid.Report.TerminationDate,
                                           ReCredentialingDate = contractgrid.Report.ReCredentialingDate,
                                           CredentialingInfoID = credentialingInfo.CredentialingInfoID,
                                           CredentialingContractRequestID = conreq.CredentialingContractRequestID,
                                           SpecialityName = Speciality,
                                           IPA = IPA,
                                           ContractGridID = contractgrid.ContractGridID
                                       });
                                    }
                                }
                            }

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

            var totalDays = (expiryDate.Value - DateTime.Now).Days + 1;

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

                    //ContractInfo
                    "ContractInfoes",


                    //Personal Detail
                    "PersonalDetail.ProviderTitles.ProviderType",
                    "PersonalDetail.ProviderLevel"
                };
        }

        public void NotifyExpiries()
        {
            var expiries = ExpiryNotificationRepository.GetAll();

            if (expiries == null || expiries.Count() == 0)
                return;
            ccos = aspnetusersRepository.GetCCOAndCRADetails();

            string HospitalPriviligesBody = "";
            taskcount = 1;
            foreach (var expiry in expiries)
            {
                foreach (var stateLicenseExpiry in expiry.StateLicenseExpiries.Where(s => s.NotificationDate != null && s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("State License");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "State License", stateLicenseExpiry.ExpiryDate.Value, (stateLicenseExpiry.ExpiryDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var deaLicense in expiry.DEALicenseExpiries.Where(s => s.NotificationDate != null && s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("Federal DEA");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "Federal DEA", deaLicense.ExpiryDate.Value, (deaLicense.ExpiryDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var cdsc in expiry.CDSCInfoExpiries.Where(s => s.NotificationDate != null && s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("CDS Information");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "CDS Information", cdsc.ExpiryDate.Value, (cdsc.ExpiryDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var specialty in expiry.SpecialtyDetailExpiries.Where(s => s.NotificationDate != null && s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("Specialty/Board");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "Specialty/Board", specialty.ExpiryDate.Value, (specialty.ExpiryDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                if (expiry.HospitalPrivilegeExpiries.Where(s => s.NotificationDate != null && s.NotificationDate.Value.Date == DateTime.Now.Date).Count() > 0)
                {

                    HospitalPriviligesBody += GetHospitalPriviligesEmailBody(expiry.FirstName + ' ' + expiry.LastName, expiry.NPINumber, expiry.ProviderLevel, expiry.HospitalPrivilegeExpiries.ToList());


                }

                foreach (var profLiability in expiry.ProfessionalLiabilityExpiries.Where(s => s.NotificationDate != null && s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("Professional Liability");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "Professional Liability", profLiability.ExpirationDate.Value, (profLiability.ExpirationDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var workerComp in expiry.WorkerCompensationExpiries.Where(s => s.NotificationDate != null && s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("Worker Compensation");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "Worker Compensation", workerComp.ExpirationDate.Value, (workerComp.ExpirationDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var Medicare in expiry.MedicareExpiries.Where(s => s.NotificationDate != null && s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("Medicare Information");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "Medicare Information", Medicare.ExpirationDate.Value, (Medicare.ExpirationDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var Medicaid in expiry.MedicaidExpiries.Where(s => s.NotificationDate != null && s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("Medicaid Information");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "Medicaid Information", Medicaid.ExpirationDate.Value, (Medicaid.ExpirationDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }
                foreach (var CAQH in expiry.CAQHExpiries.Where(s => s.NotificationDate != null && s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("CAQH Expiry");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "CAQH Expiry", CAQH.NextAttestationDate.Value, (CAQH.NextAttestationDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }

                foreach (var credinfo in expiry.UpComingRecredentials.Where(s => s.NotificationDate != null && s.NotificationDate.Value.Date == DateTime.Now.Date))
                {
                    EMailModel emailModel = GetEmailModel(expiry);
                    emailModel.Subject = GetEmailSubject("UpComing Recredentials");
                    emailModel.Body = GetEmailBody(GetFullName(expiry), "UpComing Recredentials", credinfo.ReCredentialingDate.Value, (credinfo.ReCredentialingDate.Value - DateTime.Now).TotalDays);
                    EmailSender.SendMail(emailModel);
                }


            }
            if (HospitalPriviligesBody != "")
            {
                EMailModel hospitalpriviligesemailModel = GetHospitalPriviligesEmailModel();
                hospitalpriviligesemailModel.Subject = "Hospital Privilege Expiry Details";
                StringBuilder mailBody = new StringBuilder();
                mailBody.Append("<b>Dear All ");
                mailBody.Append("</b></br></br>");
                mailBody.Append("<p>This is to inform you that the following Hospital Privilege Licenses are going to expire within 90 days, ");
                mailBody.Append("<br/><br/></p><pre>");
                mailBody.Append(HospitalPriviligesBody);
                mailBody.Append("</pre><br/><br/>");
                mailBody.Append("Thank You,<br/>");
                mailBody.Append("Credentialing Dept.<br/>");
                mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["CompanyName"] + "<br/>");
                mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["CompanyContactNumber"] + "<br/>");
                mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"]);
                mailBody.Append("<br/><br/><br/><b>");
                //mailBody.Append("<br/><br/><b>This is a system generated email, please do not respond. Send all inquiries directly to credentialing@accesshealthcarellc.net</b>");
                //mailBody.Append("<br/><br/><b>This is a system generated email, please do not respond. Send all inquiries directly to credentialing@credaxis.com");
                //mailBody.Append(" and for call inquiries, our Credentialing Specialists are available to assist with all questions Monday- Friday 8a.m.-5PM and you may contact us via telephone at 352-796-9994.");
                mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["DisclaimerMessage"]);
                mailBody.Append("</b>");
                hospitalpriviligesemailModel.Body = mailBody.ToString();
                EmailSender.SendMail(hospitalpriviligesemailModel);
            }
        }
        public async Task GetTaskExpiriesForCCO()
        {
            var ccoList = await userRepository.GetAllAsync();
            var ListofCCO = ccoList.Where(f => f.StatusType == StatusType.Active).ToList();
            foreach (var cco in ListofCCO)
            {
                var TaskTrackerDetails = await TaskTrackerRepository.GetAllTasksWithUserId(cco.AuthenicateUserId);

                var AllTasks = TaskTrackerDetails.ToList();
                List<TaskTrackerExpiry> expiredTasks = new List<TaskTrackerExpiry>();
                if (AllTasks != null && AllTasks.Count != 0)
                {

                    foreach (var Task in AllTasks.Where(p => p != null && p.NextFollowUpDate < DateTime.Now))
                    {
                        if (Task.StatusType == TaskTrackerStatusType.OPEN || Task.StatusType == TaskTrackerStatusType.REOPEN)
                        {
                            if (TaskTrackerExpiryRepository.Find(x => x.TaskTrackerId == Task.TaskTrackerId) == null)
                            {
                                expiredTasks.Add(new TaskTrackerExpiry()
                                {
                                    TaskTrackerId = Task.TaskTrackerId,
                                    SubSectionName = Task.SubSectionName,
                                    Subject = Task.Subject,
                                    NextFollowUpDate = Task.NextFollowUpDate,
                                    AssignedById = Task.AssignedById,
                                    AssignedToId = Task.AssignedToId,
                                    HospitalID = Task.HospitalID,
                                    InsuaranceCompanyNameID = Task.InsuaranceCompanyNameID,
                                    LastUpdatedDate = Task.LastModifiedDate,
                                    ModeOfFollowUp = Task.ModeOfFollowUp,
                                    ProfileID = Task.ProfileID,
                                    DaysLeft = (Task.NextFollowUpDate - DateTime.Now).Days

                                });
                            }
                        }
                    }

                }
                if (expiredTasks.Count > 0)
                    TaskTrackerExpiryRepository.SaveAllExpiries(expiredTasks);
            }
        }
        public void NotifyExpiriesForCCO()
        {
            var TaskTrackerExpiries = TaskTrackerExpiryRepository.GetAll("AssignedTo,AssignedBy,Hospital,InsuranceCompanyName,Provider").ToList();
            var ExpiredTasksDetails = (from task in TaskTrackerExpiries
                                       group new { task.TaskTrackerExpiryID, task.TaskTrackerId, task.SubSectionName, task.Subject, task.Hospital, task.InsuranceCompanyName, task.ModeOfFollowUp, task.NextFollowUpDate, task.LastUpdatedDate, task.Provider, task.DaysLeft } by new { task.AssignedToId, task.AssignedTo } into CCOWiseTaskData
                                       select new { AssignedToID = CCOWiseTaskData.Key.AssignedToId, AssignedCCO = CCOWiseTaskData.Key.AssignedTo, ExpiredTasks = CCOWiseTaskData.ToList() }).ToList();

            foreach (var ccoTasks in ExpiredTasksDetails)
            {
                EMailModel emailModel = GetEmailModelForTaskExpiries(aspnetusersRepository.GetUserDetailsByUserID(ccoTasks.AssignedCCO.AuthenicateUserId).Email);
                emailModel.Subject = GetEmailSubjectForTaskExpiries();
                emailModel.Body = GetEmailBodyForTaskExpiries(aspnetusersRepository.GetUserDetailsByUserID(ccoTasks.AssignedCCO.AuthenicateUserId).FullName, ccoTasks.ExpiredTasks);
                EmailSender.SendMail(emailModel);
                CDUser cdUser = UserRepository.Find(c => c.CDUserID == ccoTasks.AssignedToID, "DashboardNotifications");
                cdUser.DashboardNotifications.Add(new UserDashboardNotification
                {
                    StatusType = Entities.MasterData.Enums.StatusType.Active,
                    AcknowledgementStatusType = Entities.MasterData.Enums.AcknowledgementStatusType.Unread,
                    Action = "Task Expired",
                    ActionPerformed = ccoTasks.ExpiredTasks.Count + " of your tasks due for follow-up today.",
                    //ActionPerformedByUser = ,
                    RedirectURL = "/TaskTracker/Index"
                });
                UserRepository.Update(cdUser);
                UserRepository.Save();
            }


            if (TaskTrackerExpiries.Count != 0)
                TaskTrackerExpiryRepository.DeleteAllExpiries(TaskTrackerExpiries);
        }

        private string GetFullName(ExpiryNotificationDetail expiry)
        {
            return expiry.FirstName + " " + expiry.MiddleName + " " + expiry.LastName;
        }
        private EMailModel GetEmailModelForTaskExpiries(string EmailAddress)
        {
            EMailModel emailModel = new EMailModel();
            //emailModel.To = "testingsingh285@gmail.com";
            //emailModel.To = expiry.EmailAddress;
            //emailModel.Cc = "cco@accesshealthcarellc.net";

            emailModel.To = EmailAddress;
            emailModel.From = System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"];
            return emailModel;
        }
        private string GetEmailSubjectForTaskExpiries()
        {
            StringBuilder mailBody = new StringBuilder();
            mailBody.Append("Reminder For Expired Tasks");
            return mailBody.ToString();
        }
        private string GetEmailBodyForTaskExpiries(string FullName, dynamic expiries)
        {
            StringBuilder mailBody = new StringBuilder();

            mailBody.Append("<b>Dear ");
            mailBody.Append(FullName);
            mailBody.Append("</b></br></br>");
            mailBody.Append("<p>This is to inform you that the following Task(s) ");
            mailBody.Append("of yours are due for follow-up today: ");
            mailBody.Append("<br/><br/></p><pre>");
            var taskcount = 1;
            foreach (var task in expiries)
            {
                mailBody.Append(taskcount++);
                mailBody.Append(".  Provider Name     : " + task.Provider.PersonalDetail.FirstName + " " + task.Provider.PersonalDetail.MiddleName + " " + task.Provider.PersonalDetail.LastName + " -" + task.Provider.OtherIdentificationNumber.NPINumber + "<br/>");
                mailBody.Append("    Subject           : " + task.Subject + "<br/>");
                mailBody.Append("    Sub Section Name  : " + task.SubSectionName + "<br/>");
                if (task.InsuranceCompanyName != null)
                    mailBody.Append("    Insurance Company : " + task.InsuranceCompanyName.CompanyName + "<br/>");
                else
                    mailBody.Append("    Insurance Company : " + "Not Available " + "<br/>");
                if (task.Hospital != null)
                    mailBody.Append("    Hospital          : " + task.Hospital.HospitalName + "<br/>");
                else
                    mailBody.Append("    Hospital          : " + "Not Available " + "<br/>");

                mailBody.Append("    Next Follow-up    : " + task.NextFollowUpDate.ToString("MM/dd/yyyy") + "<br/>");

                var modeoffollowup = new JavaScriptSerializer().Deserialize<dynamic>(task.ModeOfFollowUp);
                string mode = "";
                foreach (var item in modeoffollowup)
                {
                    mode += item["Name"];

                    if (Array.IndexOf(modeoffollowup, item) != modeoffollowup.Length - 1)
                    {
                        mode += ", ";
                    }
                }
                mailBody.Append("    Mode of Follow-up : " + mode + "<br/>");
                mailBody.Append("    Days Left         : " + task.DaysLeft + "<br/>");
                mailBody.Append("    Last Updated      : " + task.LastUpdatedDate.ToString("MM/dd/yyyy") + "<br/>");
                mailBody.Append("<br/><br/>");
            }


            mailBody.Append("</pre><br/><br/>");
            mailBody.Append("Thank You,<br/>");
            mailBody.Append("Credentialing Dept.<br/>");
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["CompanyName"] + "<br/>");
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["CompanyContactNumber"] + "<br/>");
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"]);
            mailBody.Append("<br/><br/><br/><b>");
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["DisclaimerMessage"]);
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["DisclaimerEmailID"]);
            mailBody.Append("</b>");
            return mailBody.ToString();
        }

        private string GetHospitalPriviligesEmailBody(string FullName, string NPI, string ProviderLevel, List<HospitalPrivilegeExpiry> expiries)
        {
            StringBuilder mailBody = new StringBuilder();



            foreach (var expiry in expiries)
            {
                mailBody.Append(taskcount++);
                mailBody.Append(".  Provider Name     : " + FullName + "<br/>");
                mailBody.Append("    NPI Number        : " + NPI + "<br/>");
                mailBody.Append("    Provider Level    : " + ProviderLevel + "<br/>");
                mailBody.Append("    Hospital Name     : " + expiry.HospitalName + "<br/>");
                mailBody.Append("    Expiry Date       : " + expiry.AffiliationEndDate.Value.ToString("MM/dd/yyyy") + "<br/>");
                mailBody.Append("    Days Left         : " + Math.Floor((expiry.AffiliationEndDate.Value - DateTime.Now).TotalDays) + "<br/>");
                mailBody.Append("<br/><br/>");
            }



            return mailBody.ToString();
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
            mailBody.Append(expiryDate.ToString("MM/dd/yyyy"));
            mailBody.Append(". Please renew this as soon as possible to avoid issues with your credentialing.");
            mailBody.Append("<br/><br/>");
            mailBody.Append("Please contact us if you require any assistance.");
            mailBody.Append("</p><br/><br/>");
            mailBody.Append("<br/><br/>");
            mailBody.Append("Thank You,<br/>");
            mailBody.Append("Credentialing Dept.<br/>");
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["CompanyName"] + "<br/>");
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["CompanyContactNumber"] + "<br/>");
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"]);
            mailBody.Append("<br/><br/><br/><b>");
            //mailBody.Append("<br/><br/><b>This is a system generated email, please do not respond. Send all inquiries directly to credentialing@credaxis.com and  for call inquiries, our Credentialing Specialists are available to assist with all questions Monday- Friday 8a.m.-5PM and you may contact us via telephone at 352-796-9994.");
            //mailBody.Append("<br/><br/><b>This is a system generated email, please do not respond. Send all inquiries directly to credentialing@credaxis.com");
            //mailBody.Append(" and for call inquiries, our Credentialing Specialists are available to assist with all questions Monday- Friday 8a.m.-5PM and you may contact us via telephone at 352-796-9994.");
            mailBody.Append(System.Configuration.ConfigurationManager.AppSettings["DisclaimerMessage"]);
            mailBody.Append("</b>");
            return mailBody.ToString();
        }

        private string GetEmailSubject(string expiryType)
        {
            return "Renewal Notice for the " + expiryType;
        }
        private EMailModel GetHospitalPriviligesEmailModel()
        {
            EMailModel emailModel = new EMailModel();

            emailModel.To = "";
            foreach (var cco in ccos)
            {
                emailModel.To += cco.Email;
                if (ccos.LastOrDefault() != cco)
                {
                    emailModel.To += ',';
                }

            }
            emailModel.From = System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"];
            return emailModel;
        }
        private EMailModel GetEmailModel(ExpiryNotificationDetail expiry)
        {
            EMailModel emailModel = new EMailModel();
            //emailModel.To = "testingsingh285@gmail.com";
            //emailModel.To = expiry.EmailAddress;
            //emailModel.Cc = "cco@accesshealthcarellc.net";
            emailModel.To = expiry.EmailAddress;
            emailModel.From = System.Configuration.ConfigurationManager.AppSettings["ProductEmailID"];
            return emailModel;
        }

        public async Task<ExpiryNotificationDetail> GetAllExpiryForAProvider(int profileID)
        {
            var profile = await ProfileRepository.FindAsync(p => p.ProfileID == profileID && p.Status.Equals(StatusType.Active.ToString()), String.Join(",", GetIncludeProperties()));
            var Expiries = new ExpiryNotificationDetail();
            Expiries = FillExpiryInformation(profile);
            var UpcomingRecredentialRepository = uow.GetGenericRepository<CredentialingInfo>();
            var contractinfo = await UpcomingRecredentialRepository.FindAsync(p => p.ProfileID == profileID);
            Expiries.UpComingRecredentials = FillUpComingRecredentialInformation(contractinfo).UpComingRecredentials;
            return Expiries;
        }
    }
}
