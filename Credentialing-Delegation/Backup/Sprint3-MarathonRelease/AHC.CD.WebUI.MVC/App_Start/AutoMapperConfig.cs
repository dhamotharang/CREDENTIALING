using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
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
using AHC.CD.WebUI.MVC.Areas.Profile.Models.BoardSpecialty;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.DisclosureQuestions;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalAffiliation;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.WorkHistory;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            #region Profile

            Mapper.CreateMap<AHC.CD.Entities.MasterProfile.Profile, AHC.CD.Entities.MasterProfile.Profile>();
            Mapper.CreateMap<ProfileDocument, ProfileDocument>();

            #region Board Speciality

            Mapper.CreateMap<SpecialtyDetailViewModel, SpecialtyDetail>();
            Mapper.CreateMap<SpecialtyDetail, SpecialtyDetail>();

            Mapper.CreateMap<PracticeInterestViewModel, PracticeInterest>();
            Mapper.CreateMap<PracticeInterest, PracticeInterest>();

            Mapper.CreateMap<SpecialtyBoardCertifiedDetailViewModel, SpecialtyBoardCertifiedDetail>();
            Mapper.CreateMap<SpecialtyBoardCertifiedDetail, SpecialtyBoardCertifiedDetail>();

            Mapper.CreateMap<SpecialtyBoardNotCertifiedDetailViewModel, SpecialtyBoardNotCertifiedDetail>();
            Mapper.CreateMap<SpecialtyBoardNotCertifiedDetail, SpecialtyBoardNotCertifiedDetail>();

            Mapper.CreateMap<SpecialtyBoardCertifiedDetailHistory, SpecialtyBoardCertifiedDetailHistory>();

            #endregion

            #region Demographics

            Mapper.CreateMap<BirthInformationViewModel, BirthInformation>();
            Mapper.CreateMap<BirthInformation, BirthInformation>();

            Mapper.CreateMap<ContactDetailViewModel, ContactDetail>();
            Mapper.CreateMap<ContactDetail, ContactDetail>();

            Mapper.CreateMap<EmailDetailViewModel, EmailDetail>();
            Mapper.CreateMap<EmailDetail, EmailDetail>();

            Mapper.CreateMap<HomeAddressViewModel, HomeAddress>();
            Mapper.CreateMap<HomeAddress, HomeAddress>();

            Mapper.CreateMap<KnownLanguageViewModel, KnownLanguage>();
            Mapper.CreateMap<KnownLanguage, KnownLanguage>();

            Mapper.CreateMap<LanguageInfoViewModel, LanguageInfo>();
            Mapper.CreateMap<LanguageInfo, LanguageInfo>();

            Mapper.CreateMap<OtherLegalNameViewModel, OtherLegalName>();
            Mapper.CreateMap<OtherLegalName, OtherLegalName>();

            Mapper.CreateMap<PersonalDetailViewModel, PersonalDetail>();
            Mapper.CreateMap<ProviderTitleViewModel, ProviderTitle>();
            Mapper.CreateMap<ProviderTitle, ProviderTitle>();
            Mapper.CreateMap<PersonalDetail, PersonalDetail>().ForMember(dto => dto.ProviderTitles, opt => opt.Ignore()); ;

            Mapper.CreateMap<PersonalIdentificationViewModel, PersonalIdentification>();
            Mapper.CreateMap<PersonalIdentification, PersonalIdentification>();

            Mapper.CreateMap<PhoneDetailViewModel, PhoneDetail>();
            Mapper.CreateMap<PhoneDetail, PhoneDetail>();

            Mapper.CreateMap<PreferredContactViewModel, PreferredContact>();
            Mapper.CreateMap<PreferredContact, PreferredContact>();

            Mapper.CreateMap<PreferredWrittenContactViewModel, PreferredWrittenContact>();
            Mapper.CreateMap<PreferredWrittenContact, PreferredWrittenContact>();

            Mapper.CreateMap<VisaDetailViewModel, VisaDetail>();
            Mapper.CreateMap<VisaDetail, VisaDetail>().ForMember(dto => dto.VisaInfo, opt => opt.Ignore()); ;

            Mapper.CreateMap<VisaInfoViewModel, VisaInfo>();
            Mapper.CreateMap<VisaInfo, VisaInfo>();

            #endregion

            #region Discloure Question

            Mapper.CreateMap<ProviderDisclosureQuestionAnswerViewModel, ProfileDisclosure>();
            Mapper.CreateMap<ProfileDisclosure, ProfileDisclosure>();

            Mapper.CreateMap<ProfileDisclosureQuestionAnswer, ProfileDisclosureQuestionAnswer>();

            #endregion

            #region Eduction History

            Mapper.CreateMap<CMECertificationViewModel, CMECertification>();
            Mapper.CreateMap<CMECertification, CMECertification>();

            Mapper.CreateMap<ECFMGDetailViewModel, ECFMGDetail>();
            Mapper.CreateMap<ECFMGDetail, ECFMGDetail>();

            Mapper.CreateMap<EducationAddressViewModel, SchoolInformation>();
            Mapper.CreateMap<SchoolInformation, SchoolInformation>();

            Mapper.CreateMap<EducationDetailViewModel, EducationDetail>();
            Mapper.CreateMap<EducationDetail, EducationDetail>();            

            Mapper.CreateMap<ResidencyInternshipDetailViewModel, ResidencyInternshipDetail>();
            Mapper.CreateMap<ResidencyInternshipDetail, ResidencyInternshipDetail>();

            Mapper.CreateMap<TrainingDetailViewModel, TrainingDetail>();
            Mapper.CreateMap<TrainingDetail, TrainingDetail>();

            #endregion

            #region Hospital Privilege

            Mapper.CreateMap<HospitalPrivilegeInformationViewModel, HospitalPrivilegeInformation>();
            Mapper.CreateMap<HospitalPrivilegeInformation, HospitalPrivilegeInformation>();

            Mapper.CreateMap<HospitalPrivilegeDetailViewModel, HospitalPrivilegeDetail>();
            Mapper.CreateMap<HospitalPrivilegeDetail, HospitalPrivilegeDetail>();
            Mapper.CreateMap<HospitalPrivilegeDetailHistory, HospitalPrivilegeDetailHistory>();

            #endregion

            #region Identification and License

            Mapper.CreateMap<CDSCInformationViewModel, CDSCInformation>();
            Mapper.CreateMap<CDSCInformation, CDSCInformation>();
            Mapper.CreateMap<CDSCInfoHistory, CDSCInfoHistory>();

            Mapper.CreateMap<DEAScheduleInfoViewModel, DEAScheduleInfo>();
            Mapper.CreateMap<DEAScheduleInfo, DEAScheduleInfo>();

            Mapper.CreateMap<FederalDEAInformationViewModel, FederalDEAInformation>();
            Mapper.CreateMap<FederalDEAInformation, FederalDEAInformation>();
            Mapper.CreateMap<FederalDEAInfoHistory, FederalDEAInfoHistory>();

            Mapper.CreateMap<MedicaidInformationViewModel, MedicaidInformation>();
            Mapper.CreateMap<MedicaidInformation, MedicaidInformation>();

            Mapper.CreateMap<MedicareInformationViewModel, MedicareInformation>();
            Mapper.CreateMap<MedicareInformation, MedicareInformation>();

            Mapper.CreateMap<OtherAuthenticationDetailViewModel, OtherAuthenticationDetail>();
            Mapper.CreateMap<OtherAuthenticationDetailViewModel, OtherAuthenticationDetail>();
            Mapper.CreateMap<OtherAuthenticationDetail, OtherAuthenticationDetail>();

            Mapper.CreateMap<OtherIdentificationNumberViewModel, OtherIdentificationNumber>();
            Mapper.CreateMap<OtherIdentificationNumber, OtherIdentificationNumber>();

            Mapper.CreateMap<StateLicenseViewModel, StateLicenseInformation>();
            Mapper.CreateMap<StateLicenseInformation, StateLicenseInformation>();
            Mapper.CreateMap<StateLicenseInfoHistory, StateLicenseInfoHistory>();

            #endregion

            #region Professional Affiliation

            Mapper.CreateMap<ProfessionalAffiliationDetailViewModel, ProfessionalAffiliationInfo>();
            Mapper.CreateMap<ProfessionalAffiliationInfo, ProfessionalAffiliationInfo>();

            #endregion

            #region Professional Liability

            Mapper.CreateMap<ProfessionalLiabilityInfoViewModel, ProfessionalLiabilityInfo>();
            Mapper.CreateMap<ProfessionalLiabilityInfo, ProfessionalLiabilityInfo>();
            Mapper.CreateMap<ProfessionalLiabilityInfoHistory, ProfessionalLiabilityInfoHistory>();

            #endregion

            #region Professional Reference

            Mapper.CreateMap<ProfessionalReferenceViewModel, ProfessionalReferenceInfo>();
            Mapper.CreateMap<ProfessionalReferenceInfo, ProfessionalReferenceInfo>();

            #endregion

            #region Work History

            Mapper.CreateMap<ProfessionalWorkExperienceViewModel, ProfessionalWorkExperience>();
            Mapper.CreateMap<ProfessionalWorkExperience, ProfessionalWorkExperience>();

            Mapper.CreateMap<MilitaryServiceInformationViewModel, MilitaryServiceInformation>();
            Mapper.CreateMap<MilitaryServiceInformation, MilitaryServiceInformation>();

            Mapper.CreateMap<PublicHealthServiceViewModel, PublicHealthService>();
            Mapper.CreateMap<PublicHealthService, PublicHealthService>();

            Mapper.CreateMap<WorkGapViewModel, WorkGap>();
            Mapper.CreateMap<WorkGap, WorkGap>();

            #endregion

            #endregion

            
        }
    }
}