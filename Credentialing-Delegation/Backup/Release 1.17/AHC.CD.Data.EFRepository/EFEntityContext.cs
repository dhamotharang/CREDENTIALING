using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Location;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Accessibility;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Service;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        #region Credentialing Details and Plans

        //public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        //public DbSet<Plan> Plans { get; set; }
        //public DbSet<IndividualPlan> IndividualPlans { get; set; }
        //public DbSet<CredentialingInfo> CredentialingInfos { get; set; }
        //public DbSet<CredentialingLog> CredentialingLogs { get; set; }

        #endregion

        #region MasterData

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
        
        #endregion

        #region Profile

        public DbSet<AHC.CD.Entities.MasterProfile.Profile> Profiles { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.PracticeLocation.PracticeLocationDetail> PracticeLocationDetails { get; set; }

        #endregion

        public DbSet<OrganizationGroup> OrganizationGroups { get; set; }

        public DbSet<CDUser> Users { get; set; }
        public DbSet<CDUserRoleRelation> UserRoleRelations { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<ProviderLevel> ProviderLevels { get; set; }    


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

        #region Change Notification
        public DbSet<AHC.CD.Entities.Notification.ChangeNotificationDetail> ChangeNotificationDetails { get; set; }
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
