
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProviderInfo
{
    public class IndividualProvider 
    {
        #region Master Profile


        public IndividualProvider()
        {
            LastUpdatedDateTime = DateTime.Now;
        }
        public int IndividualProviderID
        {
            get;
            set;
        }

        
        public string UniqueProviderCode
        {
            get;
            set;
        }

        [Column(TypeName = "datetime2")]
        public DateTime? LastUpdatedDateTime
        {
            get;
            set;
        }


        public virtual OrganizationDetail OrganizationDetail  { get; set; }

        public virtual AHC.CD.Entities.MasterProfile.Demographics.PersonalDetail PersonalDetail { get; set; }
        public virtual ICollection<AHC.CD.Entities.MasterProfile.Demographics.OtherLegalName> OtherLegalNames { get; set; }
        public virtual ICollection<AHC.CD.Entities.MasterProfile.Demographics.HomeAddress> HomeAddresses { get; set; }
        public virtual AHC.CD.Entities.MasterProfile.Demographics.ContactDetail ContactDetail { get; set; }
        public virtual AHC.CD.Entities.MasterProfile.Demographics.PersonalIdentification PersonalIdentification { get; set; }
        //public virtual AHC.CD.Entities.MasterProfile.Demographics.PrimaryCredentialingContact PrimaryCredentialingContact { get; set; }

        public virtual AHC.CD.Entities.MasterProfile.Demographics.BirthInformation BirthInformation { get; set; }
        public virtual AHC.CD.Entities.MasterProfile.Demographics.LanguageInfo LanguageInfo { get; set; }
        public virtual AHC.CD.Entities.MasterProfile.Demographics.VisaDetail VisaDetail { get; set; }
        
        public virtual ICollection<AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.StateLicenseInformation> StateLicenses { get; set; }
        public virtual ICollection<AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.FederalDEAInformation> FederalDEAInformations { get; set; }
        //public virtual AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.MedicareAndMedicaidInfo MedicareAndMedicaid { get; set; }
        public virtual AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.CDSCInformation CDSCInformation { get; set; }
        public virtual AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.OtherIdentificationNumber OtherIdentificationNumber { get; set; }
        
        public virtual AHC.CD.Entities.MasterProfile.EducationHistory.EducationDetail SchoolDetail { get; set; }
        //public virtual ICollection<AHC.CD.Entities.MasterProfile.EducationHistory.GraduationDetail> GraduationDetails { get; set; }
        public virtual ICollection<AHC.CD.Entities.MasterProfile.EducationHistory.ResidencyInternshipDetail> ResidencyInternships { get; set; }
        //public virtual ICollection<AHC.CD.Entities.MasterProfile.EducationHistory.SpecialityDetail> SpecialityDetails { get; set; }
        public virtual ICollection<AHC.CD.Entities.MasterProfile.EducationHistory.TrainingDetail> FifthPathwayDetails { get; set; }
        public virtual ICollection<AHC.CD.Entities.MasterProfile.EducationHistory.CMECertification> ProviderCertifications { get; set; }
        //public virtual ICollection<AHC.CD.Entities.MasterProfile.EducationHistory.OtherReleventEducation> OtherReleventEducationDetails { get; set; }

        
        //public virtual ICollection<AHC.CD.Entities.MasterProfile.PracticeLocation.PrimaryPracticeLocation> PrimaryPracticeLocations { get; set; }
        //public virtual ICollection<AHC.CD.Entities.MasterProfile.PracticeLocation.ProfessionalLiabilityInsurance> ProfessionalLiabilityInsurances { get; set; }
        
        public virtual ICollection<AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeInformation> HospitalPrivilegeInformations { get; set; }
        
        public virtual ICollection<AHC.CD.Entities.MasterProfile.ProfessionalAffiliation.ProfessionalAffiliationInfo> ProfessionalAffiliationInfos { get; set; }
        
        public virtual ICollection<AHC.CD.Entities.MasterProfile.WorkHistory.MilitaryServiceInformation> MilitaryServiceInformations{get;set;}
        public virtual ICollection<AHC.CD.Entities.MasterProfile.WorkHistory.PublicHealthService> PublicHealthServices { get; set; }
        
        public virtual ICollection<AHC.CD.Entities.MasterProfile.WorkHistory.ProfessionalWorkExperience> ProfessionalWorkExperiences { get; set; }
        public virtual ICollection<AHC.CD.Entities.MasterProfile.WorkHistory.WorkGap> WorkGaps { get; set; }
        
        public virtual ICollection<AHC.CD.Entities.MasterProfile.ProfessionalReference.ProfessionalReferenceInfo> ProfessionalRefereneInfos { get; set; }

        public virtual ICollection<AHC.CD.Entities.MasterProfile.DisclosureQuestions.ProviderDisclosureQuestionAnswer> ProviderDisclosureQuestionAnswer { get; set; }

       
        #endregion


    }
}
