using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing;
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
    public class EFEntityContext : DbContext
    {
        #region Provider Details
     
        public DbSet<IndividualProvider> Providers { get; set; }

        #endregion

        #region Individual Master Profile

        public DbSet<AHC.CD.Entities.MasterProfile.Demographics.PersonalDetail> PersonalDetails { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.Demographics.OtherLegalName> OtherLegalNames { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.Demographics.HomeAddress> HomeAddresses { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.Demographics.ContactDetail> ContactDetails { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.Demographics.PersonalIdentification> PersonalIdentifications { get; set; }

        public DbSet<AHC.CD.Entities.MasterProfile.Demographics.BirthInformation> BirthInformations { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.Demographics.VisaDetail> VisaDetails { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.Demographics.LanguageInfo> LanguageInfos { get; set; }

        public DbSet<AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.StateLicenseInformation> StateLicenses { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.FederalDEAInformation> FederalDEAInformations { get; set; }
        //public DbSet<AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.MedicareAndMedicaidInfo> MedicareAndMedicaids { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.CDSCInformation> CDSCInformations { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.OtherIdentificationNumber> OtherIdentificationNumbers { get; set; }

        public DbSet<AHC.CD.Entities.MasterProfile.EducationHistory.EducationDetail> SchoolDetails { get; set; }
        //public DbSet<AHC.CD.Entities.MasterProfile.EducationHistory.GraduationDetail> GraduationDetails { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.EducationHistory.ResidencyInternshipDetail> ResidencyInternships { get; set; }
        //public DbSet<AHC.CD.Entities.MasterProfile.EducationHistory.SpecialityDetail> SpecialityDetails { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.EducationHistory.TrainingDetail> FifthPathwayDetails { get; set; }
               

        //public DbSet<AHC.CD.Entities.MasterProfile.PracticeLocation.PrimaryPracticeLocation> PrimaryPracticeLocations { get; set; }
               
        public DbSet<AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeInformation> HospitalPrivilegeInformations { get; set; }
               
        public DbSet<AHC.CD.Entities.MasterProfile.ProfessionalAffiliation.ProfessionalAffiliationInfo> ProfessionalAffiliationInfos { get; set; }
               
        //public DbSet<AHC.CD.Entities.MasterProfile.ServiceInformation.MilitaryServiceInformation> MilitaryServiceInformations { get; set; }
        //public DbSet<AHC.CD.Entities.MasterProfile.ServiceInformation.PublicHealthService> PublicHealthServices { get; set; }
               
        public DbSet<AHC.CD.Entities.MasterProfile.WorkHistory.ProfessionalWorkExperience> ProfessionalWorkExperiences { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.WorkHistory.WorkGap> WorkGaps { get; set; }
               
        public DbSet<AHC.CD.Entities.MasterProfile.ProfessionalReference.ProfessionalReferenceInfo> ProfessionalRefereneInfos { get; set; }
        public DbSet<AHC.CD.Entities.MasterProfile.DisclosureQuestions.ProviderDisclosureQuestionAnswer> ProviderDisclosureQuestionAnswers { get; set; }
        #endregion

        #region Credentialing Details and Plans

        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<IndividualPlan> IndividualPlans { get; set; }
        public DbSet<CredentialingInfo> CredentialingInfos { get; set; }
        public DbSet<CredentialingLog> CredentialingLogs { get; set; }

        #endregion


        #region MasterData

        public DbSet<StateLicenseStatus> StateLicenseStatuses { get; set; }

        public DbSet<StaffCategory> StaffCategories { get; set; }

        public DbSet<SpecialityBoard> SpecialityBoards { get; set; }

        public DbSet<Speciality> Specialities { get; set; }

        public DbSet<School> Schools { get; set; }

        public DbSet<QualificationDegree> QualificationDegrees { get; set; }

        public DbSet<ProviderType> ProviderTypes { get; set; }

        public DbSet<ProfileDisclosureQuestion> ProfileDisclosureQuestions { get; set; }

        public DbSet<PracticeType> PracticeTypes { get; set; }

        public DbSet<PracticeServiceQuestion> PracticeServiceQuestions { get; set; }

        public DbSet<PracticeOpenStatusQuestion> PracticeOpenStatusQuestions { get; set; }

        public DbSet<PracticeAccessibilityQuestion> PracticeAccessibilityQuestions { get; set; }

        public DbSet<MilitaryRank> MilitaryRanks { get; set; }

        public DbSet<MilitaryPresentDuty> MilitaryPresentDuties { get; set; }

        public DbSet<MilitaryDischarge> MilitaryDischarges { get; set; }

        public DbSet<MilitaryBranch> MilitaryBranches { get; set; }

        public DbSet<InsuranceCarrier> InsuranceCarriers { get; set; }

        public DbSet<HospitalContactPerson> HospitalContactPersons { get; set; }

        public DbSet<HospitalContactInfo> HospitalContactInfoes { get; set; }

        public DbSet<Hospital> Hospitals { get; set; }

        public DbSet<DEASchedule> DEASchedules { get; set; }

        public DbSet<DEAScheduleType> DEAScheduleTypes { get; set; }

        public DbSet<Certification> Certifications { get; set; }

        public DbSet<AdmittingPrivilege> AdmittingPrivileges { get; set; }

        #endregion
    }


   
}
