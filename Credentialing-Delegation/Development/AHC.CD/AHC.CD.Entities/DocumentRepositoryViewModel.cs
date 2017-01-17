using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.Credentialing.PSVInformation;
using AHC.CD.Entities.DocumentRepository;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.ProfessionalLiability;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities
{
    public class DocumentRepositoryViewModel
    {
        public PersonalIdentification PersonalIdentification { get; set; }

        public BirthInformation BirthInformation { get; set; }

        public List<OtherLegalName> OtherLegalNames { get; set; }

        public CVInformation CVInformation { get; set; }

        public VisaDetail VisaDetail { get; set; }

        public List<StateLicenseInformation> StateLicenses { get; set; }

        public List<FederalDEAInformation> FederalDEAInformations { get; set; }

        public List<MedicareInformation> MedicareInformations { get; set; }

        public List<MedicaidInformation> MedicaidInformations { get; set; }

        public List<CDSCInformation> CDSCInformations { get; set; }

        public List<EducationDetail> EducationDetails { get; set; }

        public ECFMGDetail ECFMGDetail { get; set; }

        public List<ProgramDetail> ProgramDetails { get; set; }

        public List<CMECertification> CMECertifications { get; set; }

        public List<SpecialtyDetail> SpecialtyDetails { get; set; }

        public HospitalPrivilegeInformation HospitalPrivilegeInformation { get; set; }

        public List<ProfessionalLiabilityInfo> ProfessionalLiabilityInfoes { get; set; }

        public List<ProfessionalWorkExperience> ProfessionalWorkExperiences { get; set; }

        public List<ContractInfo> ContractInfoes { get; set; }

        public List<OtherDocument> OtherDocuments { get; set; }

        public List<ProfileVerificationInfo> ProfileVerificationInfo { get; set; }

        public ICollection<CredentialingLog> credentialLog { get; set; }

    }
}
