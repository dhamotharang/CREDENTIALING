using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile.BoardSpeciality;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.ProfessionalLiability;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile
{
    public class Profile
    {
        public int ProfileID { get; set; }

        #region Demographics

        public PersonalDetail PersonalDetail { get; set; }
        public ICollection<OtherLegalName> OtherLegalNames { get; set; }
        public ICollection<HomeAddress> HomeAddresses { get; set; }
        public ContactDetail ContactDetail { get; set; }
        public PersonalIdentification PersonalIdentification { get; set; }
        public BirthInformation BirthInformation { get; set; }
        public VisaDetail VisaDetail { get; set; }
        public LanguageInfo LanguageInfo { get; set; }

        #endregion

        #region Specialty/Board

        public ICollection<SpecialityDetail> SpecialityDetails { get; set; }

        #endregion

        #region Hospital Privileges

        public ICollection<HospitalPrivilegeInformation> HospitalPrivilegeInformations { get; set; }

        #endregion

        #region Professional Liability

        public ICollection<ProfessionalLiabilityInsurance> ProfessionalLiabilityInsurances { get; set; }

        #endregion

        #region Professional Reference

        public ICollection<ProfessionalReferenceInfo> ProfessionalReferenceInfos { get; set; }

        #endregion

        #region Identification And License

        public ICollection<StateLicenseInformation> StateLicenses { get; set; }
        public ICollection<FederalDEAInformation> FederalDEAInformations { get; set; }
        public ICollection<MedicareInformation> MedicareInformations { get; set; }
        public ICollection<MedicaidInformation> MedicaidInformations { get; set; }
        public ICollection<CDSCInformation> CDSCInformations { get; set; }
        public OtherIdentificationNumber OtherIdentificationNumber { get; set; }

        #endregion

        #region Education History

        public ICollection<EducationDetail> EducationDetails { get; set; }
        public ICollection<TrainingDetail> TrainingDetails { get; set; }
        public ICollection<CMECertification> CMECertifications { get; set; }

        #endregion

        #region Work History

        public ICollection<ProfessionalWorkExperience> ProfessionalWorkExperiences { get; set; }
        public ICollection<MilitaryServiceInformation> MilitaryServiceInformations { get; set; }
        public ICollection<PublicHealthService> PublicHealthServices { get; set; }
        public ICollection<WorkGap> WorkGaps { get; set; }

        #endregion
    }
}
