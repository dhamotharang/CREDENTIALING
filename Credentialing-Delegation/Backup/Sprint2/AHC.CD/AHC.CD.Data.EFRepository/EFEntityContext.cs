using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.ProfileDemographicInfo;
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
    public class EFEntityContext : DbContext
    {
        #region Provider Details
        public DbSet<ProviderCategory> ProviderCategories { get; set; }

        public DbSet<ProviderType> ProviderTypes { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<ContractInfo> ContractInfos { get; set; }

        public DbSet<ContractDetail> ContractDetails { get; set; }

        public DbSet<MailTemplate> MailTemplates { get; set; }

        public DbSet<MailCategory> MailCategories { get; set; }

        #endregion

        #region Individual Demographic Details
        public DbSet<PersonalInfo> PersonalInfos { get; set; }

        public DbSet<FamilyInfo> FamilyInfos { get; set; }

        public DbSet<PersonalIdentificationInfo> PersonalIdentificationInfos { get; set; }

        public DbSet<AddressInfo> AddresesInfos { get; set; }

        public DbSet<ContactInfo> ContactInfos { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<DocumentCategory> DocumentCategories { get; set; }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        public DbSet<ProfessionalLicenseInfo> ProfessionalLicenseInfos { get; set; }

        public DbSet<BirthInfo> BirthInfos { get; set; }

        public DbSet<LanguageInfo> LanguageInfos { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<CitizenshipInfo> CitizenshipInfos { get; set; }

        public DbSet<NonResidentInfo> NonResidentInfos { get; set; }

        public DbSet<VisaInformation> VisaInformations { get; set; }

        public DbSet<ProfessionalIdentificationInfo> ProfessionalIdentificationInfos { get; set; }

        public DbSet<LicenseType> LicenseTypes { get; set; }

        public DbSet<DEALicenseInfo> DEALicenseInfos { get; set; }

        public DbSet<DEASchedule> DEASchedules { get; set; }

        public DbSet<WorkHistoryInfo> WorkHistoryInfos { get; set; }

        public DbSet<WorkGap> WorkGaps { get; set; }

        public DbSet<WorkInfo> WorkInfos { get; set; }

        public DbSet<EmploymentInfo> EmployementInfos { get; set; }

        public DbSet<WorkInfoContact> WorkInfoContacts { get; set; }

        public DbSet<Employer> Employers { get; set; }



        #endregion

        #region Credentialing Details and Plans

        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<IndividualPlan> IndividualPlans { get; set; }
        public DbSet<CredentialingInfo> CredentialingInfos { get; set; }
        public DbSet<CredentialingLog> CredentialingLogs { get; set; }

        #endregion
       
    }


   
}
