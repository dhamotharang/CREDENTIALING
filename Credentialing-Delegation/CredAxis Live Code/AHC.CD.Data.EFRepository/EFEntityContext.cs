using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Credentialing.CredentialingRequestTracker;
using AHC.CD.Entities.Credentialing.PSVInformation;
using AHC.CD.Entities.Forms;
using AHC.CD.Entities.Location;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Accessibility;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Service;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile.CredentialingRequest;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.MasterProfile.ProfileReviewSection;
using AHC.CD.Entities.Notification;
using AHC.CD.Entities.PackageGenerate;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository
{
    /// <summary>
    /// Author: Venkat
    /// Date:   20/10/2014
    /// 
    /// EF code first context
    /// Creates database tables for each DbSet property
    /// </summary>
    /// 
  [DbConfigurationType(typeof(EFCacheConfiguration))] // For EF 2nd Level Cache
    public class EFEntityContext : DbContext
    {
        //public EFEntityContext()
        //{
            
        //}

        //public EFEntityContext(bool status)
        //{
        //    Configuration.LazyLoadingEnabled = status;
        //    Configuration.ProxyCreationEnabled = status;
        //}

        public EFEntityContext()
        {
            var adapter = (IObjectContextAdapter)this;
            var objectContext = adapter.ObjectContext;
            objectContext.CommandTimeout = 1 * 60; // value in seconds
        }

        #region Credentialing Details and Plans

        //public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        //public DbSet<Plan> Plans { get; set; }
        //public DbSet<IndividualPlan> IndividualPlans { get; set; }
        //public DbSet<CredentialingInfo> CredentialingInfos { get; set; }
        //public DbSet<CredentialingLog> CredentialingLogs { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<PlanLOB> PlanLOB { get; set; }

        public DbSet<LOB> LineOfBusinesses { get; set; }

        public DbSet<LOBDetail> LOBDetail { get; set;}

        public DbSet<SubPlan> SubPlans { get; set; }

        public DbSet<AHC.CD.Entities.Credentialing.Loading.CredentialingInfo> CredentialingInfoes { get; set; }

        public DbSet<PlanContactDetail> ContactDetail { get; set;}

        public DbSet<PlanAddress> Location { get; set;}

        public DbSet<PlanBE> PlanBE { get; set;}

        public DbSet<LobBE> LobBE { get; set;}

        public DbSet<PlanContract> PlanContract { get; set;}

        public DbSet<PlanContractDetail> PlanContractDetail { get; set;}

        //public DbSet<PlanLOBAddress> LOBAddressDetail { get; set; }

        //public DbSet<PlanLOBContact> LOBContactDetail { get; set;}

        public DbSet<LOBAddressDetail> LOBAddressDetail { get; set; }   

        public DbSet<LOBContactDetail> LOBContactDetail { get; set; }

        public DbSet<ProfileVerificationInfo> ProfileVerificationInfo { get; set; }

        

        #endregion

        #region Credentialing Request

        public DbSet<CredentialingRequest> CredentialingRequests { get; set; }

        public DbSet<CredentialingRequestTracker> CredentialingRequestTrackers { get; set; }    

        #endregion

        #region MasterData
        public DbSet<ParticipatingStatusType> ParticipatingStatusTypes { get; set; }
        public DbSet<MasterEmployee> MasterEmployees { get; set; }
        public DbSet<MasterPracticePaymentRemittancePerson> MasterPracticePaymentRemittancePersons { get; set; }
        public DbSet<StateLicenseStatus> StateLicenseStatuses { get; set; }
        public DbSet<StaffCategory> StaffCategories { get; set; }
        public DbSet<SpecialtyBoard> SpecialtyBoards { get; set; }
        public DbSet<Specialty> Specialities { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<QualificationDegree> QualificationDegrees { get; set; }
        public DbSet<ProviderType> ProviderTypes { get; set; }
        public DbSet<ProfileDisclosureQuestion> ProfileDisclosureQuestions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<PracticeServiceQuestion> PracticeServiceQuestions { get; set; }
        public DbSet<PracticeOpenStatusQuestion> PracticeOpenStatusQuestions { get; set; }
        public DbSet<PracticeAccessibilityQuestion> PracticeAccessibilityQuestions { get; set; }
        public DbSet<MilitaryRank> MilitaryRanks { get; set; }
        public DbSet<MilitaryPresentDuty> MilitaryPresentDuties { get; set; }
        public DbSet<MilitaryDischarge> MilitaryDischarges { get; set; }
        public DbSet<MilitaryBranch> MilitaryBranches { get; set; }
        public DbSet<InsuranceCarrier> InsuranceCarriers { get; set; }
        public DbSet<InsuranceCarrierAddress> InsuranceCarrierAddresses { get; set; }
        public DbSet<HospitalContactPerson> HospitalContactPersons { get; set; }
        public DbSet<HospitalContactInfo> HospitalContactInfoes { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<DEASchedule> DEASchedules { get; set; } 
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<AdmittingPrivilege> AdmittingPrivileges { get; set; }
        public DbSet<VisaStatus> VisaStatuses { get; set; }
        public DbSet<VisaType> VisaTypes { get; set; }
        public DbSet<LocationDetail> Locations { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProfileVerificationParameter> ProfileVerificationParameters { get; set; }
        public DbSet<VerificationLink> VerificationLinks { get; set; }
        public DbSet<PracticeOpenStatusQuestionAnswer> PracticeOpenStatusQuestionAnswers { get; set; }
        public DbSet<InsuaranceCompanyName> InsuaranceCompanyNames { get; set; }
        public DbSet<ProfileSubSection> ProfileSubSections { get; set; }
        public DbSet<DecredentialingReason> DecredentialingReasons { get; set; } 
       public DbSet<NotesTemplate> NotesTemplates { get; set; }
        #endregion

        #region Profile

        public DbSet<AHC.CD.Entities.MasterProfile.Profile> Profiles { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.PracticeLocation.PracticeLocationDetail> PracticeLocationDetails { get; set; }

        #endregion

        public DbSet<OrganizationGroup> OrganizationGroups { get; set; }

        public DbSet<CDUser> Users { get; set; }
        public DbSet<OtherUser> OtherUsers { get; set; }
        public DbSet<CDUserRoleRelation> UserRoleRelations { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<ProviderLevel> ProviderLevels { get; set; }

        public DbSet<ProfileReviewSection> ProfileReviewSections { get; set; }

        public DbSet<ProfileSection> ProfileSections { get; set; }

        public DbSet<TaskTrackerExpiry> TaskTrackerExpiries { get; set; }

        #region Plan Forms

        public DbSet<PlanFormPayer> PlanFormPayers { get; set; }
        public DbSet<PlanFormRegion> PlanFormRegions { get; set; }
        public DbSet<PlanFormDetail> PlanFormDetails { get; set; }

        #endregion


        #region profile Users

        public DbSet<AHC.CD.Entities.UserInfo.ProfileUser> ProfileUsers { get; set; }

        #endregion

        #region Profile Update/Renewals Tracker

        public DbSet<AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal.ProfileUpdatesTracker> ProfileUpdatesTrackers { get; set; }

        #endregion


        #region Account

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        public DbSet<FacilityAccessibilityQuestion> FacilityAccessibilityQuestions { get; set; }
        public DbSet<FacilityServiceQuestion> FacilityServiceQuestions { get; set; }
        public DbSet<FacilityPracticeType> FacilityPracticeTypes { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }

        #endregion

        #region Notification

        public DbSet<AHC.CD.Entities.Notification.ChangeNotificationDetail> ChangeNotificationDetails { get; set; }
        public DbSet<AHC.CD.Entities.Notification.ExpiryNotificationDetail> ExpiryDetails { get; set; }
        public DbSet<AHC.CD.Entities.EmailNotifications.EmailInfo> EmailInfoes { get; set; }
        public DbSet<AHC.CD.Entities.EmailNotifications.EmailTemplate> EmailTemplates { get; set; }
        public DbSet<AHC.CD.Entities.EmailNotifications.EmailGroup> EmailGroups { get; set; }
        public DbSet<AHC.CD.Entities.EmailNotifications.CDUser_GroupEmail> CDUser_GroupEmails { get; set; }
        public DbSet<AHC.CD.Entities.Credentialing.LoadingInformation.ContractGrid> ContractGrids { get; set; }

        #endregion

        #region Pacakage Generation Tracker

        public DbSet<AuditingPackageGenerationTracker> AuditingPackageGenerationTrackers { get; set; }

        #endregion

        #region Task Tracker

        public DbSet<AHC.CD.Entities.TaskTracker.TaskTracker> TaskTrackers { get; set; }

        #endregion

        #region Plan Form

        public DbSet<PlanForm> PlanForms { get; set; }

        #endregion
    }

     ///<summary>
     ///Author: Venkat
     ///Date: 07/02/2015
     ///Entity Framework 2nd Level Cache - To Improve the DB Access Performance 
     ///</summary>

  public class EFCacheConfiguration : DbConfiguration
  {
      internal static readonly EFCache.InMemoryCache Cache = new EFCache.InMemoryCache();
      public EFCacheConfiguration()
      {
          var transactionHandler = new EFCache.CacheTransactionHandler(Cache);

          AddInterceptor(transactionHandler);

          Loaded +=
            (sender, args) => args.ReplaceService<System.Data.Entity.Core.Common.DbProviderServices>(
              (s, _) => new EFCache.CachingProviderServices(s, transactionHandler));
      }
  }
}
