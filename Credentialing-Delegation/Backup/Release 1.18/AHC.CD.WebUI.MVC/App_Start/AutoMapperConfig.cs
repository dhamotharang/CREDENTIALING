using AHC.CD.Entities.MasterData.Account.Accessibility;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Service;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.DisclosureQuestions;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.Entities.MasterProfile.ProfessionalLiability;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ContractInformation;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.WebUI.MVC.Areas.Profile.Models;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.BoardSpecialty;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.DisclosureQuestions;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalAffiliation;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.WorkHistory;
using AHC.CD.Entities.MasterData.Account.Language;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHC.CD.Entities.MasterData.Account.Staff;

namespace AHC.CD.WebUI.MVC.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            #region Profile

            

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
            Mapper.CreateMap<ProviderLevel, ProviderLevel>();

            Mapper.CreateMap<ProviderTitleViewModel, ProviderTitle>();
            Mapper.CreateMap<ProviderTitle, ProviderTitle>();
            Mapper.CreateMap<PersonalDetail, PersonalDetail>().ForMember(dto => dto.ProviderTitles, opt => opt.Ignore());

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

            #region Disclosure Question

            Mapper.CreateMap<ProfileDisclosureViewModel, ProfileDisclosure>();
            Mapper.CreateMap<ProfileDisclosure, ProfileDisclosure>().ForMember(dto => dto.ProfileDisclosureQuestionAnswers, opt => opt.Ignore()); ;

            Mapper.CreateMap<ProfileDisclosureQuestionAnswerViewModel, ProfileDisclosureQuestionAnswer>();
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

            #region Practice Location

            Mapper.CreateMap<PracticeLocationDetail, PracticeLocationDetail>().
                ForMember(dto => dto.BusinessOfficeManagerOrStaff, opt => opt.Ignore()).
                ForMember(dto => dto.PaymentAndRemittance, opt => opt.Ignore()).
                ForMember(dto => dto.OfficeHour, opt => opt.Ignore()).
                ForMember(dto => dto.OpenPracticeStatus, opt => opt.Ignore()).
                ForMember(dto => dto.BillingContactPerson, opt => opt.Ignore()).
                ForMember(dto => dto.PracticeColleagues, opt => opt.Ignore()).
                ForMember(dto => dto.MidlevelPractioners, opt => opt.Ignore()).
                ForMember(dto => dto.PrimaryCredentialingContactPerson, opt => opt.Ignore()).
                ForMember(dto => dto.WorkersCompensationInformation, opt => opt.Ignore()).
                ForMember(dto => dto.Facility, opt => opt.Ignore()).
                ForMember(dto => dto.Organization, opt => opt.Ignore()).
                ForMember(dto => dto.Group, opt => opt.Ignore()).
                ForMember(dto => dto.PracticeProviders, opt => opt.Ignore());
            Mapper.CreateMap<PracticeOfficeHour, PracticeOfficeHour>().ForMember(dto => dto.PracticeDays, opt => opt.Ignore());
            Mapper.CreateMap<PracticeOfficeHourViewModel, PracticeOfficeHour>();
            Mapper.CreateMap<PracticeDay, PracticeDay>().ForMember(dto => dto.DailyHours, opt => opt.Ignore());
            Mapper.CreateMap<PracticeDayViewModel, PracticeDay>();
            Mapper.CreateMap<OpenPracticeStatus, OpenPracticeStatus>();

            Mapper.CreateMap<PracticeColleague, PracticeColleague>();
            Mapper.CreateMap<PracticeColleagueViewModel, PracticeColleague>();
            Mapper.CreateMap<PracticeProvider, PracticeProvider>().
                ForMember(dto => dto.PracticeProviderSpecialties, opt => opt.Ignore()).
                ForMember(dto => dto.PracticeProviderTypes, opt => opt.Ignore());
            Mapper.CreateMap<PracticeProviderViewModel, PracticeProvider>();
            Mapper.CreateMap<PracticeProviderTypeViewModel, PracticeProviderType>();
            Mapper.CreateMap<PracticeProviderSpecialtyViewModel, PracticeProviderSpecialty>();
            Mapper.CreateMap<MidLevelPractitioner, MidLevelPractitioner>();
            Mapper.CreateMap<WorkersCompensationInformation, WorkersCompensationInformation>();
            Mapper.CreateMap<PracticeLocationViewModel, Facility>();
            Mapper.CreateMap<FacilityDetailViewModel, FacilityDetail>();
            Mapper.CreateMap<FacilityLanguageViewModel, FacilityLanguage>();
            Mapper.CreateMap<NonEnglishLanguageViewModel, NonEnglishLanguage>();
            Mapper.CreateMap<FacilityAccessibilityViewModel, FacilityAccessibility>();
            Mapper.CreateMap<FacilityAccessibility, FacilityAccessibility>().ForMember(dto => dto.FacilityAccessibilityQuestionAnswers, opt => opt.Ignore()); ;
            Mapper.CreateMap<FacilityAccessibilityQuestionAnswerViewModel, FacilityAccessibilityQuestionAnswer>();
            Mapper.CreateMap<FacilityServiceViewModel, FacilityService>();
            Mapper.CreateMap<FacilityService, FacilityService>().ForMember(dto => dto.FacilityServiceQuestionAnswers, opt => opt.Ignore()); ;
            Mapper.CreateMap<FacilityServiceQuestionAnswerViewModel, FacilityServiceQuestionAnswer>();
            Mapper.CreateMap<PracticeLocationDetailViewModel, PracticeLocationDetail>();
            
            Mapper.CreateMap<WorkersCompensationInfoViewModel, WorkersCompensationInformation>();
            Mapper.CreateMap<WorkersCompensationInformation, WorkersCompensationInformation>();
            Mapper.CreateMap<PracticeOfficeHourViewModel, PracticeOfficeHour>();
            Mapper.CreateMap<PracticeDayViewModel, PracticeDay>();
            Mapper.CreateMap<PracticeDailyHourViewModel, PracticeDailyHour>();
            Mapper.CreateMap<PracticeOfficeHourViewModel, PracticeOfficeHour>();

            Mapper.CreateMap<Employee, Employee>().ForMember(dto => dto.Departments, opt => opt.Ignore()).ForMember(dto => dto.Designations, opt => opt.Ignore()); 
            Mapper.CreateMap<FacilityEmployeeViewModel , Employee>();
            Mapper.CreateMap<FacilityEmployeeViewModel, FacilityEmployeeViewModel>().ForMember(dto => dto.Departments, opt => opt.Ignore());
            Mapper.CreateMap<FacilityEmployeeViewModel, FacilityEmployeeViewModel>().ForMember(dto => dto.Designations, opt => opt.Ignore());
            Mapper.CreateMap<ProviderPracticeOfficeHourViewModel, ProviderPracticeOfficeHour>();
            Mapper.CreateMap<ProviderPracticeOfficeHour, ProviderPracticeOfficeHour>();

            Mapper.CreateMap<OpenPracticeStatusViewModel, OpenPracticeStatus>();
            Mapper.CreateMap<PracticeOpenStatusQuestionAnswerViewModel, PracticeOpenStatusQuestionAnswer>();
            Mapper.CreateMap<PracticeOpenStatusQuestionAnswer, PracticeOpenStatusQuestionAnswer>();
            Mapper.CreateMap<OpenPracticeStatus, OpenPracticeStatus>().ForMember(dto => dto.PracticeQuestionAnswers, opt => opt.Ignore()); ;

            //Mapper.CreateMap<BusinessOfficeContactPersonViewModel, BusinessOfficeContactPerson>();
            //Mapper.CreateMap<PracticePaymentAndRemittance, PracticePaymentAndRemittance>();
            //Mapper.CreateMap<PracticePaymentAndRemittance, PracticePaymentAndRemittance>().ForMember(dto => dto.PaymentAndRemittancePerson, opt => opt.Ignore());
            Mapper.CreateMap<PracticePaymentAndRemittanceViewModel, PracticePaymentAndRemittance>();

            Mapper.CreateMap<MidLevelPractitionerViewModel, MidLevelPractitioner>();

            Mapper.CreateMap<SupervisingProviderViewModel, SupervisingProvider>();

            Mapper.CreateMap<SupervisingProvider, SupervisingProvider>();

            Mapper.CreateMap<MidlevelEmployeeViewModel, Employee>();

       
            #endregion

            #region Contract Information
            Mapper.CreateMap<ContractInfoViewModel, ContractInfo>();
            Mapper.CreateMap<ContractInfo, ContractInfo>().ForMember(dto => dto.ContractGroupInfoes, opt => opt.Ignore());


            Mapper.CreateMap<ContractGroupInfoViewModel, ContractGroupInfo>();
            Mapper.CreateMap<ContractGroupInfo, ContractGroupInfo>();
            Mapper.CreateMap<PracticePaymentAndRemittance, PracticePaymentAndRemittance>();
            Mapper.CreateMap<PracticePaymentAndRemittance, PracticePaymentAndRemittance>().ForMember(dto => dto.PaymentAndRemittancePerson, opt => opt.Ignore());
            Mapper.CreateMap<PaymentRemittanceViewModel, PracticePaymentAndRemittance>();
            #endregion

            #region CV Information
            Mapper.CreateMap<CVInformationViewModel, CVInformation>();
            #endregion

            Mapper.CreateMap<ProfileViewModel, AHC.CD.Entities.MasterProfile.Profile>();
            Mapper.CreateMap<AHC.CD.Entities.MasterProfile.Profile, AHC.CD.Entities.MasterProfile.Profile>();
            Mapper.CreateMap<ProfileDocument, ProfileDocument>();

            #endregion

            #region Expiries

            Mapper.CreateMap<StateLicenseInformation, AHC.CD.Entities.Notification.StateLicenseExpiry>();
            Mapper.CreateMap<FederalDEAInformation, AHC.CD.Entities.Notification.DEALicenseExpiry>();
            Mapper.CreateMap<CDSCInformation, AHC.CD.Entities.Notification.CDSCInfoExpiry>();


            Mapper.CreateMap<AHC.CD.Entities.Notification.ExpiryNotificationDetail, AHC.CD.Entities.Notification.ExpiryNotificationDetail>()
                .ForMember(dto => dto.StateLicenseExpiries, opt => opt.Ignore())
                .ForMember(dto => dto.DEALicenseExpiries, opt => opt.Ignore())
                .ForMember(dto => dto.HospitalPrivilegeExpiries, opt => opt.Ignore())
                .ForMember(dto => dto.ProfessionalLiabilityExpiries, opt => opt.Ignore())
                .ForMember(dto => dto.SpecialtyDetailExpiries, opt => opt.Ignore())
                .ForMember(dto => dto.WorkerCompensationExpiries, opt => opt.Ignore())
                .ForMember(dto => dto.CDSCInfoExpiries, opt => opt.Ignore());

            Mapper.CreateMap<AHC.CD.Entities.Notification.StateLicenseExpiry, AHC.CD.Entities.Notification.StateLicenseExpiry>();
            Mapper.CreateMap<AHC.CD.Entities.Notification.DEALicenseExpiry, AHC.CD.Entities.Notification.DEALicenseExpiry>();
            Mapper.CreateMap<AHC.CD.Entities.Notification.HospitalPrivilegeExpiry, AHC.CD.Entities.Notification.HospitalPrivilegeExpiry>();
            Mapper.CreateMap<AHC.CD.Entities.Notification.ProfessionalLiabilityExpiry, AHC.CD.Entities.Notification.ProfessionalLiabilityExpiry>();
            Mapper.CreateMap<AHC.CD.Entities.Notification.SpecialtyDetailExpiry, AHC.CD.Entities.Notification.SpecialtyDetailExpiry>();
            Mapper.CreateMap<AHC.CD.Entities.Notification.WorkerCompensationExpiry, AHC.CD.Entities.Notification.WorkerCompensationExpiry>();
            Mapper.CreateMap<AHC.CD.Entities.Notification.CDSCInfoExpiry, AHC.CD.Entities.Notification.CDSCInfoExpiry>();

            #endregion
        }
    }
}