using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.DisclosureQuestions;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.Entities.MasterProfile.ProfessionalLiability;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile
{
    public class Profile
    {
        public Profile()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int ProfileID { get; set; }

        //[Required]
        //[MaxLength(15)]
        //[Index(IsUnique = true)]
        [NotMapped]
        public string ProviderID { get; set; }

        public string ProfilePhotoPath { get; set; }

        #region Demographics

        public virtual PersonalDetail PersonalDetail { get; set; }
        public virtual ICollection<OtherLegalName> OtherLegalNames { get; set; }
        public virtual ICollection<HomeAddress> HomeAddresses { get; set; }
        public virtual ContactDetail ContactDetail { get; set; }
        public virtual PersonalIdentification PersonalIdentification { get; set; }
        public virtual BirthInformation BirthInformation { get; set; }
        public virtual VisaDetail VisaDetail { get; set; }
        public virtual LanguageInfo LanguageInfo { get; set; }

        #endregion

        #region Identification And License

        public virtual ICollection<StateLicenseInformation> StateLicenses { get; set; }
        public virtual ICollection<FederalDEAInformation> FederalDEAInformations { get; set; }
        public virtual ICollection<MedicareInformation> MedicareInformations { get; set; }
        public virtual ICollection<MedicaidInformation> MedicaidInformations { get; set; }
        public virtual ICollection<CDSCInformation> CDSCInformations { get; set; }
        public virtual OtherIdentificationNumber OtherIdentificationNumber { get; set; }

        #endregion

        #region Specialty/Board

        public virtual ICollection<SpecialtyDetail> SpecialtyDetails { get; set; }
        public virtual PracticeInterest PracticeInterest { get; set; }

        #endregion

        #region Hospital Privileges

        public virtual HospitalPrivilegeInformation HospitalPrivilegeInformation { get; set; }

        #endregion

        #region Professional Liability

        public virtual ICollection<ProfessionalLiabilityInfo> ProfessionalLiabilityInfoes { get; set; }

        #endregion

        #region Professional Reference

        public virtual ICollection<ProfessionalReferenceInfo> ProfessionalReferenceInfos { get; set; }

        #endregion        

        #region Education History

        public virtual ICollection<EducationDetail> EducationDetails { get; set; }
        public virtual ICollection<TrainingDetail> TrainingDetails { get; set; }
        public virtual ICollection<CMECertification> CMECertifications { get; set; }
        public virtual ECFMGDetail ECFMGDetail { get; set; }

        #endregion

        #region Work History

        public virtual ICollection<ProfessionalWorkExperience> ProfessionalWorkExperiences { get; set; }
        public virtual ICollection<MilitaryServiceInformation> MilitaryServiceInformations { get; set; }
        public virtual ICollection<PublicHealthService> PublicHealthServices { get; set; }
        public virtual ICollection<WorkGap> WorkGaps { get; set; }

        #endregion

        #region Documents

        public virtual ICollection<ProfileDocument> ProfileDocuments { get; set; }

        #endregion

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion        

        #region Professional Affiliation

        public virtual ICollection<ProfessionalAffiliationInfo> ProfessionalAffiliationInfos { get; set; }

        #endregion

        #region Practice Locations

        public virtual ICollection<PracticeLocation.PracticeLocationDetail> PracticeLocationDetails { get; set; }

        #endregion

        #region OrganizationDetail

        public virtual OrganizationDetail OrganizationDetail { get; set; }

        #endregion

        #region Disclosure Question

        public virtual ProfileDisclosure ProfileDisclosure { get; set; }

        #endregion

        #region Contract Info
        public virtual ICollection<AHC.CD.Entities.MasterProfile.Contract.ContractInfo> ContractInfoes { get; set;}  
        #endregion

        #region CV Information
        public virtual CVInformation CVInformation { get; set;}
        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        
    }
}
