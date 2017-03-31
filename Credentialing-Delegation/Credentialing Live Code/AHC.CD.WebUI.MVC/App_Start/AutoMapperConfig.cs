﻿using AHC.CD.Entities.MasterData.Account.Accessibility;
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
using AHC.CD.WebUI.MVC.Areas.Initiation.Models;
using AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel;
using AHC.CD.Entities.Location;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.UserInfo;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Plan;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Initiation;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.DocumentRepository;
using AHC.CD.Entities.DocumentRepository;
using AHC.CD.Entities.Credentialing.AppointmentInformation;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.PSVInfo;
using AHC.CD.Entities.Credentialing.PSVInformation;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingCheckList;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Loading;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.DelegationProfileReport;
using AHC.CD.Entities.Credentialing.DelegationProfileReport;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.WebUI.MVC.Models.EmailTemplate;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.CustomField;
using AHC.CD.Entities.CustomField;
using AHC.CD.Entities.CustomField.CustomFieldTransaction;
using AHC.CD.WebUI.MVC.Models.EmailService;
using AHC.CD.Entities.MasterProfile.CredentialingRequest;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.CredentialingRequest;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Timeline;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingRequestTracker;
using AHC.CD.Entities.Credentialing.CredentialingRequestTracker;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfileReviewSection;
using AHC.CD.Entities.MasterProfile.ProfileReviewSection;
using AHC.CD.WebUI.MVC.Models.TaskTracker;
using AHC.CD.Entities.TaskTracker;
using AHC.CD.Business.BusinessModels.TaskTracker;
using AHC.CD.WebUI.MVC.Areas.PlanPDF.Models;
using AHC.CD.Entities.PackageGenerate;
using AHC.CD.WebUI.MVC.Areas.Plans.Models;

namespace AHC.CD.WebUI.MVC.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {


            #region Plans

            Mapper.CreateMap<PlanListViewModel, Plan>();
            Mapper.CreateMap<PlanListContactDetailViewModel, PlanContactDetail>();
            Mapper.CreateMap<PlanListAddressViewModel, PlanAddress>();
            Mapper.CreateMap<PlanListLOBViewModel, PlanLOB>();
            Mapper.CreateMap<PlanContractsViewModel, PlanContract>();
            Mapper.CreateMap<PlanLOBContactDetailsViewModel, LOBContactDetail>();
            Mapper.CreateMap<PlanLOBAddressDetailsViewModel, LOBAddressDetail>();
            Mapper.CreateMap<ContactDetailsViewModel, ContactDetail>();
            Mapper.CreateMap<PhoneDetailsViewModel, PhoneDetail>();
            Mapper.CreateMap<EmailDetailsViewModel, EmailDetail>();
            Mapper.CreateMap<PreferredWrittenContactsViewModel, PreferredWrittenContact>();
            Mapper.CreateMap<PreferredContactsViewModel, PreferredContact>();
            Mapper.CreateMap<SubPlansViewModel, SubPlan>();

            Mapper.CreateMap<Plan, Plan>().ForMember(dto => dto.PlanLOBs, opt => opt.Ignore());
            Mapper.CreateMap<PlanContactDetail, PlanContactDetail>();
            Mapper.CreateMap<PlanAddress, PlanAddress>();
            Mapper.CreateMap<PlanLOB, PlanLOB>();
            Mapper.CreateMap<PlanContract, PlanContract>();
            Mapper.CreateMap<LOBContactDetail, LOBContactDetail>();
            Mapper.CreateMap<LOBAddressDetail, LOBAddressDetail>();
            Mapper.CreateMap<ContactDetail, ContactDetail>();
            Mapper.CreateMap<PhoneDetail, PhoneDetail>();
            Mapper.CreateMap<EmailDetail, EmailDetail>();
            Mapper.CreateMap<PreferredWrittenContact, PreferredWrittenContact>();
            Mapper.CreateMap<PreferredContact, PreferredContact>();
            Mapper.CreateMap<SubPlan, SubPlan>();

            #endregion





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
            Mapper.CreateMap<HomeAddress, HomeAddressHistory>();

            Mapper.CreateMap<KnownLanguageViewModel, KnownLanguage>();
            Mapper.CreateMap<KnownLanguage, KnownLanguage>();

            Mapper.CreateMap<LanguageInfoViewModel, LanguageInfo>();
            Mapper.CreateMap<LanguageInfo, LanguageInfo>();

            Mapper.CreateMap<OtherLegalNameViewModel, OtherLegalName>();
            Mapper.CreateMap<OtherLegalName, OtherLegalName>();
            Mapper.CreateMap<OtherLegalName, OtherLegalNameHistory>();

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
            Mapper.CreateMap<ProfileDisclosure, ProfileDisclosure>().ForMember(dto => dto.ProfileDisclosureQuestionAnswers, opt => opt.Ignore());

            Mapper.CreateMap<ProfileDisclosureQuestionAnswerViewModel, ProfileDisclosureQuestionAnswer>();
            Mapper.CreateMap<ProfileDisclosureQuestionAnswer, ProfileDisclosureQuestionAnswer>();

            #endregion

            #region Eduction History

            Mapper.CreateMap<CMECertificationViewModel, CMECertification>();
            Mapper.CreateMap<CMECertification, CMECertification>();
            Mapper.CreateMap<CMECertification, CMECertificationHistory>();

            Mapper.CreateMap<ECFMGDetailViewModel, ECFMGDetail>();
            Mapper.CreateMap<ECFMGDetail, ECFMGDetail>();

            Mapper.CreateMap<EducationAddressViewModel, SchoolInformation>();
            Mapper.CreateMap<SchoolInformation, SchoolInformation>();

            Mapper.CreateMap<EducationDetailViewModel, EducationDetail>();
            Mapper.CreateMap<EducationDetail, EducationDetail>();
            Mapper.CreateMap<EducationDetail, EducationDetailHistory>();

            Mapper.CreateMap<ResidencyInternshipDetailViewModel, ResidencyInternshipDetail>();
            Mapper.CreateMap<ResidencyInternshipDetail, ResidencyInternshipDetail>();

            Mapper.CreateMap<TrainingDetailViewModel, TrainingDetail>();
            Mapper.CreateMap<TrainingDetail, TrainingDetail>();

            Mapper.CreateMap<ProgramDetailViewModel, ProgramDetail>();
            Mapper.CreateMap<ProgramDetail, ProgramDetail>();
            Mapper.CreateMap<ProgramDetail, ProgramDetailHistory>();

            #endregion

            #region Hospital Privilege

            Mapper.CreateMap<HospitalPrivilegeInformationViewModel, HospitalPrivilegeInformation>();
            Mapper.CreateMap<HospitalPrivilegeInformation, HospitalPrivilegeInformation>();

            Mapper.CreateMap<HospitalPrivilegeDetailViewModel, HospitalPrivilegeDetail>();
            Mapper.CreateMap<HospitalPrivilegeDetail, HospitalPrivilegeDetail>();
            Mapper.CreateMap<HospitalPrivilegeDetail, HospitalPrivilegeDetailHistory>();
            Mapper.CreateMap<HospitalPrivilegeDetailHistory, HospitalPrivilegeDetailHistory>();

            #endregion

            #region Identification and License

            Mapper.CreateMap<CDSCInformationViewModel, CDSCInformation>();
            Mapper.CreateMap<CDSCInformation, CDSCInformation>();
            Mapper.CreateMap<CDSCInfoHistory, CDSCInfoHistory>();
            Mapper.CreateMap<CDSCInformation, CDSCInfoHistory>();

            Mapper.CreateMap<DEAScheduleInfoViewModel, DEAScheduleInfo>();
            Mapper.CreateMap<DEAScheduleInfo, DEAScheduleInfo>();

            Mapper.CreateMap<FederalDEAInformationViewModel, FederalDEAInformation>();
            Mapper.CreateMap<FederalDEAInformation, FederalDEAInformation>();
            Mapper.CreateMap<FederalDEAInfoHistory, FederalDEAInfoHistory>();
            Mapper.CreateMap<FederalDEAInformation, FederalDEAInfoHistory>();

            Mapper.CreateMap<MedicaidInformationViewModel, MedicaidInformation>();
            Mapper.CreateMap<MedicaidInformation, MedicaidInformation>();
            Mapper.CreateMap<MedicaidInformation, MedicaidInformationHistory>();

            Mapper.CreateMap<MedicareInformationViewModel, MedicareInformation>();
            Mapper.CreateMap<MedicareInformation, MedicareInformation>();
            Mapper.CreateMap<MedicareInformation, MedicareInformationHistory>();

            Mapper.CreateMap<OtherAuthenticationDetailViewModel, OtherAuthenticationDetail>();
            Mapper.CreateMap<OtherAuthenticationDetailViewModel, OtherAuthenticationDetail>();
            Mapper.CreateMap<OtherAuthenticationDetail, OtherAuthenticationDetail>();

            Mapper.CreateMap<OtherIdentificationNumberViewModel, OtherIdentificationNumber>();
            Mapper.CreateMap<OtherIdentificationNumber, OtherIdentificationNumber>();

            Mapper.CreateMap<StateLicenseViewModel, StateLicenseInformation>();
            Mapper.CreateMap<StateLicenseInformation, StateLicenseInformation>();
            Mapper.CreateMap<StateLicenseInfoHistory, StateLicenseInfoHistory>();
            Mapper.CreateMap<StateLicenseInformation, StateLicenseInfoHistory>();

            #endregion

            #region Professional Affiliation

            Mapper.CreateMap<ProfessionalAffiliationDetailViewModel, ProfessionalAffiliationInfo>();
            Mapper.CreateMap<ProfessionalAffiliationInfo, ProfessionalAffiliationInfo>();
            Mapper.CreateMap<ProfessionalAffiliationInfo, ProfessionalAffiliationInfoHistory>();

            #endregion

            #region Professional Liability

            Mapper.CreateMap<ProfessionalLiabilityInfoViewModel, ProfessionalLiabilityInfo>();
            Mapper.CreateMap<ProfessionalLiabilityInfo, ProfessionalLiabilityInfo>();
            Mapper.CreateMap<ProfessionalLiabilityInfo, ProfessionalLiabilityInfoHistory>();

            #endregion

            #region Professional Reference

            Mapper.CreateMap<ProfessionalReferenceViewModel, ProfessionalReferenceInfo>();
            Mapper.CreateMap<ProfessionalReferenceInfo, ProfessionalReferenceInfo>();
            Mapper.CreateMap<ProfessionalReferenceInfo, ProfessionalReferenceInfoHistory>();

            #endregion

            #region Work History

            Mapper.CreateMap<ProfessionalWorkExperienceViewModel, ProfessionalWorkExperience>();
            Mapper.CreateMap<ProfessionalWorkExperience, ProfessionalWorkExperience>();
            Mapper.CreateMap<ProfessionalWorkExperience, ProfessionalWorkExperienceHistory>();

            Mapper.CreateMap<MilitaryServiceInformationViewModel, MilitaryServiceInformation>();
            Mapper.CreateMap<MilitaryServiceInformation, MilitaryServiceInformation>();
            Mapper.CreateMap<MilitaryServiceInformation, MilitaryServiceInformationHistory>();

            Mapper.CreateMap<PublicHealthServiceViewModel, PublicHealthService>();
            Mapper.CreateMap<PublicHealthService, PublicHealthService>();
            Mapper.CreateMap<PublicHealthService, PublicHealthServiceHistory>();

            Mapper.CreateMap<WorkGapViewModel, WorkGap>();
            Mapper.CreateMap<WorkGap, WorkGap>();
            Mapper.CreateMap<WorkGap, WorkGapHistory>();

            #endregion

            #region Practice Location

            Mapper.CreateMap<PracticeLocationDetail, PracticeLocationDetail>().
                ForMember(dto => dto.BusinessOfficeManagerOrStaff, opt => opt.Ignore()).
                ForMember(dto => dto.PaymentAndRemittance, opt => opt.Ignore()).
                ForMember(dto => dto.OfficeHour, opt => opt.Ignore()).
                ForMember(dto => dto.OpenPracticeStatus, opt => opt.Ignore()).
                ForMember(dto => dto.BillingContactPerson, opt => opt.Ignore()).
                ForMember(dto => dto.PrimaryCredentialingContactPerson, opt => opt.Ignore()).
                ForMember(dto => dto.WorkersCompensationInformation, opt => opt.Ignore()).
                ForMember(dto => dto.Facility, opt => opt.Ignore()).
                ForMember(dto => dto.Organization, opt => opt.Ignore()).
                ForMember(dto => dto.Group, opt => opt.Ignore()).
                ForMember(dto => dto.PracticeProviders, opt => opt.Ignore());

            Mapper.CreateMap<PracticeLocationDetail, PracticeLocationDetailHistory>();

            Mapper.CreateMap<PracticeOfficeHour, PracticeOfficeHour>().ForMember(dto => dto.PracticeDays, opt => opt.Ignore());
            Mapper.CreateMap<PracticeOfficeHourViewModel, PracticeOfficeHour>();
            Mapper.CreateMap<PracticeDay, PracticeDay>().ForMember(dto => dto.DailyHours, opt => opt.Ignore());
            Mapper.CreateMap<PracticeDayViewModel, PracticeDay>();
            Mapper.CreateMap<OpenPracticeStatus, OpenPracticeStatus>();

            Mapper.CreateMap<PracticeProvider, PracticeProvider>().
                ForMember(dto => dto.PracticeProviderSpecialties, opt => opt.Ignore()).
                ForMember(dto => dto.PracticeProviderTypes, opt => opt.Ignore());
            Mapper.CreateMap<PracticeProviderViewModel, PracticeProvider>();
            Mapper.CreateMap<PracticeProviderTypeViewModel, PracticeProviderType>();
            Mapper.CreateMap<PracticeProviderSpecialtyViewModel, PracticeProviderSpecialty>();
            Mapper.CreateMap<PracticeProviderSpecialty, PracticeProviderSpecialty>().
                ForMember(dto => dto.Specialty, opt => opt.Ignore()).
                ForMember(dto => dto.Specialty, opt => opt.Ignore());
            Mapper.CreateMap<PracticeProviderType, PracticeProviderType>().
                ForMember(dto => dto.ProviderType, opt => opt.Ignore()).
                ForMember(dto => dto.ProviderType, opt => opt.Ignore());
            Mapper.CreateMap<Specialty, Specialty>();
            Mapper.CreateMap<ProviderType, ProviderType>();
            Mapper.CreateMap<WorkersCompensationInformation, WorkersCompensationInformation>();
            Mapper.CreateMap<PracticeLocationViewModel, Facility>();
            Mapper.CreateMap<FacilityDetailViewModel, FacilityDetail>();
            Mapper.CreateMap<FacilityLanguageViewModel, FacilityLanguage>();
            Mapper.CreateMap<NonEnglishLanguageViewModel, NonEnglishLanguage>();
            Mapper.CreateMap<FacilityAccessibilityViewModel, FacilityAccessibility>();
            Mapper.CreateMap<FacilityAccessibility, FacilityAccessibility>().ForMember(dto => dto.FacilityAccessibilityQuestionAnswers, opt => opt.Ignore());
            Mapper.CreateMap<FacilityAccessibilityQuestionAnswerViewModel, FacilityAccessibilityQuestionAnswer>();
            Mapper.CreateMap<FacilityServiceViewModel, FacilityService>();
            Mapper.CreateMap<FacilityService, FacilityService>().ForMember(dto => dto.FacilityServiceQuestionAnswers, opt => opt.Ignore()); ;
            Mapper.CreateMap<FacilityServiceQuestionAnswerViewModel, FacilityServiceQuestionAnswer>();
            Mapper.CreateMap<PracticeLocationDetailViewModel, PracticeLocationDetail>();

            Mapper.CreateMap<FacilityPracticeProviderViewModel, FacilityPracticeProvider>();
            Mapper.CreateMap<FacilityPracticeProviderTypeViewModel, FacilityPracticeProviderType>();
            Mapper.CreateMap<FacilityPracticeProviderSpecialityViewModel, FacilityPracticeProviderSpeciality>();

            Mapper.CreateMap<FacilityPracticeProvider, FacilityPracticeProvider>().ForMember(dto => dto.FacilityPracticeProviderTypes, opt => opt.Ignore()).ForMember(dto => dto.FacilityPracticeProviderSpecialties, opt => opt.Ignore());
            Mapper.CreateMap<FacilityPracticeProviderType, FacilityPracticeProviderType>();
            Mapper.CreateMap<FacilityPracticeProviderSpeciality, FacilityPracticeProviderSpeciality>();

            Mapper.CreateMap<WorkersCompensationInfoViewModel, WorkersCompensationInformation>();
            Mapper.CreateMap<WorkersCompensationInformation, WorkersCompensationInformation>();
            Mapper.CreateMap<PracticeOfficeHourViewModel, PracticeOfficeHour>();
            Mapper.CreateMap<PracticeDayViewModel, PracticeDay>();
            Mapper.CreateMap<PracticeDailyHourViewModel, PracticeDailyHour>();
            Mapper.CreateMap<PracticeOfficeHourViewModel, PracticeOfficeHour>();

            Mapper.CreateMap<Employee, Employee>().ForMember(dto => dto.Departments, opt => opt.Ignore()).ForMember(dto => dto.Designations, opt => opt.Ignore());
            Mapper.CreateMap<FacilityEmployeeViewModel, Employee>();
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

            Mapper.CreateMap<MidlevelEmployeeViewModel, Employee>();
            Mapper.CreateMap<PracticeProvider, PracticeProviderHistory>();

            Mapper.CreateMap<FacilityDetail, FacilityDetail>();
            Mapper.CreateMap<Facility, Facility>();
            #endregion

            #region Contract Information

            Mapper.CreateMap<ContractInfoViewModel, ContractInfo>();
            Mapper.CreateMap<ContractInfo, ContractInfo>().ForMember(dto => dto.ContractGroupInfoes, opt => opt.Ignore());
            Mapper.CreateMap<ContractGroupInfoViewModel, ContractGroupInfo>();
            Mapper.CreateMap<ContractGroupInfo, ContractGroupInfo>();
            Mapper.CreateMap<ContractGroupInfo, ContractGroupInfoHistory>();
            Mapper.CreateMap<PracticePaymentAndRemittance, PracticePaymentAndRemittance>();
            Mapper.CreateMap<PracticePaymentAndRemittance, PracticePaymentAndRemittance>().ForMember(dto => dto.PaymentAndRemittancePerson, opt => opt.Ignore());
            Mapper.CreateMap<PaymentRemittanceViewModel, PracticePaymentAndRemittance>();

            #endregion

            #region CV Information
            Mapper.CreateMap<CVInformationViewModel, CVInformation>();
            #endregion

            #region Document Repository

            Mapper.CreateMap<OtherDocumentViewModel, OtherDocument>();
            Mapper.CreateMap<OtherDocument, OtherDocument>();
            //Mapper.CreateMap<OtherDocument, OtherLegalNameHistory>();

            #endregion

            #region


            #endregion

            #region Provider Initiation

            Mapper.CreateMap<OtherIdentificationViewModel, OtherIdentificationNumber>();

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
                .ForMember(dto => dto.CDSCInfoExpiries, opt => opt.Ignore())
                .ForMember(dto => dto.ExpiryNotificationDetailID, opt => opt.Ignore());

            Mapper.CreateMap<AHC.CD.Entities.Notification.StateLicenseExpiry, AHC.CD.Entities.Notification.StateLicenseExpiry>()
                .ForMember(dto => dto.StateLicenseExpiryID, opt => opt.Ignore());
            Mapper.CreateMap<AHC.CD.Entities.Notification.DEALicenseExpiry, AHC.CD.Entities.Notification.DEALicenseExpiry>()
                .ForMember(dto => dto.DEALicenseExpiryID, opt => opt.Ignore());
            Mapper.CreateMap<AHC.CD.Entities.Notification.HospitalPrivilegeExpiry, AHC.CD.Entities.Notification.HospitalPrivilegeExpiry>()
                .ForMember(dto => dto.HospitalPrivilegeExpiryID, opt => opt.Ignore());
            Mapper.CreateMap<AHC.CD.Entities.Notification.ProfessionalLiabilityExpiry, AHC.CD.Entities.Notification.ProfessionalLiabilityExpiry>()
                .ForMember(dto => dto.ProfessionalLiabilityExpiryID, opt => opt.Ignore());
            Mapper.CreateMap<AHC.CD.Entities.Notification.SpecialtyDetailExpiry, AHC.CD.Entities.Notification.SpecialtyDetailExpiry>()
                .ForMember(dto => dto.SpecialtyDetailExpiryID, opt => opt.Ignore());
            Mapper.CreateMap<AHC.CD.Entities.Notification.WorkerCompensationExpiry, AHC.CD.Entities.Notification.WorkerCompensationExpiry>()
                .ForMember(dto => dto.WorkerCompensationExpiryID, opt => opt.Ignore());
            Mapper.CreateMap<AHC.CD.Entities.Notification.CDSCInfoExpiry, AHC.CD.Entities.Notification.CDSCInfoExpiry>()
                .ForMember(dto => dto.CDSCInfoExpiryID, opt => opt.Ignore());

            #endregion

            #region Master Data

            Mapper.CreateMap<FacilityEmployeeMasterDataViewModel, MasterEmployee>().ForMember(dto => dto.Departments, opt => opt.Ignore()).ForMember(dto => dto.Designations, opt => opt.Ignore());
            Mapper.CreateMap<PracticePaymentAndRemittanceMasterDataViewModel, MasterPracticePaymentRemittancePerson>();
            Mapper.CreateMap<MasterPracticePaymentRemittancePerson, MasterPracticePaymentRemittancePerson>();
            Mapper.CreateMap<MasterEmployee, MasterEmployee>();

            Mapper.CreateMap<EmailTemplateViewModel, EmailTemplate>();
            Mapper.CreateMap<EmailTemplate, EmailTemplate>();

            Mapper.CreateMap<AdmittingPrivilegeViewModel, AdmittingPrivilege>();
            Mapper.CreateMap<AdmittingPrivilege, AdmittingPrivilege>();

            Mapper.CreateMap<CertificationViewModel, Certification>();
            Mapper.CreateMap<Certification, Certification>();

            Mapper.CreateMap<InsuaranceCompanyNameViewModel, InsuaranceCompanyName>();
            Mapper.CreateMap<InsuaranceCompanyName, InsuaranceCompanyName>();

            Mapper.CreateMap<MilitaryDischargeViewModel, MilitaryDischarge>();
            Mapper.CreateMap<MilitaryDischarge, MilitaryDischarge>();

            Mapper.CreateMap<MilitaryPresentDutyViewModel, MilitaryPresentDuty>();
            Mapper.CreateMap<MilitaryPresentDuty, MilitaryPresentDuty>();

            Mapper.CreateMap<QualificationDegreeViewModel, QualificationDegree>();
            Mapper.CreateMap<QualificationDegree, QualificationDegree>();

            Mapper.CreateMap<SchoolViewModel, School>();
            Mapper.CreateMap<School, School>();

            Mapper.CreateMap<SpecialtyViewModel, Specialty>();
            Mapper.CreateMap<Specialty, Specialty>();

            Mapper.CreateMap<SpecialtyBoardViewModel, SpecialtyBoard>();
            Mapper.CreateMap<SpecialtyBoard, SpecialtyBoard>();

            Mapper.CreateMap<StaffCategoryViewModel, StaffCategory>();
            Mapper.CreateMap<StaffCategory, StaffCategory>();

            Mapper.CreateMap<StateLicenseStatusViewModel, StateLicenseStatus>();
            Mapper.CreateMap<StateLicenseStatus, StateLicenseStatus>();

            Mapper.CreateMap<VisaStatusViewModel, VisaStatus>();
            Mapper.CreateMap<VisaStatus, VisaStatus>();

            Mapper.CreateMap<VisaTypeViewModel, VisaType>();
            Mapper.CreateMap<VisaType, VisaType>();

            Mapper.CreateMap<PracticeAccessibilityQuestionViewModel, FacilityAccessibilityQuestion>();
            Mapper.CreateMap<FacilityAccessibilityQuestion, FacilityAccessibilityQuestion>();

            Mapper.CreateMap<PracticeOpenStatusQuestionViewModel, AHC.CD.Entities.MasterData.Account.PracticeOpenStatusQuestion>();
            Mapper.CreateMap<AHC.CD.Entities.MasterData.Account.PracticeOpenStatusQuestion, AHC.CD.Entities.MasterData.Account.PracticeOpenStatusQuestion>();

            Mapper.CreateMap<PracticeServiceQuestionViewModel, FacilityServiceQuestion>();
            Mapper.CreateMap<FacilityServiceQuestion, FacilityServiceQuestion>();

            Mapper.CreateMap<HospitalContactInfoViewModel, HospitalContactInfo>().ForMember(d => d.HospitalContactPersons, opt => opt.MapFrom(s => new List<HospitalContactPersonViewModel> { s.HospitalContactPersonViewModel }));
            Mapper.CreateMap<HospitalContactPersonViewModel, HospitalContactPerson>();
            Mapper.CreateMap<HospitalOnlyViewModel, Hospital>().ForMember(x => x.HospitalContactInfoes, opt => opt.Ignore());

            Mapper.CreateMap<HospitalViewModel, Hospital>().
                ForMember(d => d.HospitalContactInfoes, opt => opt.ResolveUsing(s =>
                    {
                        var infos = new List<HospitalContactInfo>();
                        var contactinfo = Mapper.Map<HospitalContactInfoViewModel, HospitalContactInfo>(s.HospitalContactInfoViewModel);
                        contactinfo.HospitalContactPersons = new List<HospitalContactPerson>() { Mapper.Map<HospitalContactPersonViewModel, HospitalContactPerson>(s.HospitalContactPersonViewModel) };
                        infos.Add(contactinfo);
                        return infos;

                    }));
            Mapper.CreateMap<SchoolContactInfoViewModel, SchoolContactInfo>();
            Mapper.CreateMap<SchoolViewModel, School>().
            ForMember(d => d.SchoolContactInfoes, opt => opt.ResolveUsing(a =>
                {
                    var infos = new List<SchoolContactInfo>();
                    var contactinfo = Mapper.Map<SchoolContactInfoViewModel, SchoolContactInfo>(a.SchoolContactInfoViewModel);
                    infos.Add(contactinfo);
                    return infos;
                }));
            //new List<HospitalContactInfoViewModel> { opt.MapFrom(t => t. s.HospitalContactInfoViewModel })).
            //    ForMember(d => d.HospitalContactInfoes, opt => opt.MapFrom(s => new List<HospitalContactInfoViewModel> { s.HospitalContactInfoViewModel }));
            //ForMember(d => d.HospitalContactInfoes.Select(x=>x.HospitalContactPersons),opt => opt.MapFrom(s => new List<HospitalContactPersonViewModel> { s.HospitalContactPersonViewModel }) );

            Mapper.CreateMap<InsuranceCarrierViewModel, InsuranceCarrier>().ForMember(d => d.InsuranceCarrierAddresses, opt => opt.MapFrom(s => new List<InsuranceCarrierAddressViewModel> { s.InsuranceCarrierAddress }));
            Mapper.CreateMap<InsuranceCarrier, InsuranceCarrier>();
            Mapper.CreateMap<InsuranceCarrierAddressViewModel, InsuranceCarrierAddress>();
            Mapper.CreateMap<InsuranceCarrierAddress, InsuranceCarrierAddress>();

            Mapper.CreateMap<ProviderLevelViewModel, ProviderLevel>();
            Mapper.CreateMap<ProviderLevel, ProviderLevel>();

            Mapper.CreateMap<ProviderTypeViewModel, ProviderType>();
            Mapper.CreateMap<ProviderType, ProviderType>();

            Mapper.CreateMap<OrganizationGroupViewModel, Group>();
            Mapper.CreateMap<OrganizationGroup, OrganizationGroup>();

            Mapper.CreateMap<MasterDataOrganizationGroupViewModel, OrganizationGroup>();
            Mapper.CreateMap<OrganizationGroup, OrganizationGroup>();

            Mapper.CreateMap<DEAScheduleViewModel, DEASchedule>();
            Mapper.CreateMap<DEASchedule, DEASchedule>();

            Mapper.CreateMap<QuestionViewModel, Question>();
            Mapper.CreateMap<Question, Question>();

            Mapper.CreateMap<QuestionCategoryViewModel, QuestionCategory>();
            Mapper.CreateMap<QuestionCategory, QuestionCategory>();


            Mapper.CreateMap<MilitaryRankViewModel, MilitaryRank>().ForMember(d => d.MilitaryBranches, opt => opt.MapFrom(s => new List<MilitaryBranchViewModel> { s.MilitaryBranch }));
            Mapper.CreateMap<MilitaryRank, MilitaryRank>();

            Mapper.CreateMap<MilitaryBranchViewModel, MilitaryBranch>();
            Mapper.CreateMap<MilitaryBranch, MilitaryBranch>();
            Mapper.CreateMap<CitiesViewModel, City>();
            Mapper.CreateMap<ProfileSubSectionViewModel, ProfileSubSection>();
            #endregion

            #region User

            Mapper.CreateMap<UserViewModel, ProfileUser>();
            Mapper.CreateMap<ProfileUser, ProfileUser>().ForMember(dto => dto.ProvidersUser, opt => opt.Ignore());

            Mapper.CreateMap<ProviderUser, ProviderUser>();

            #endregion

            #region Credentialing

            Mapper.CreateMap<PlanViewModel, Plan>().ForMember(dto => dto.PlanLOBs, opt => opt.Ignore());
            Mapper.CreateMap<PlanAddressViewModel, PlanAddress>();
            Mapper.CreateMap<PlanContactDetailViewModel, PlanContactDetail>();
            Mapper.CreateMap<PlanContactDetail, PlanContactDetail>();
            Mapper.CreateMap<LOBViewModel, LOB>();
            Mapper.CreateMap<SubPlanViewModel, SubPlan>();
            Mapper.CreateMap<CredentialingInitiationInfoViewModel, CredentialingInfo>();
            Mapper.CreateMap<SubPlanViewModel, SubPlan>();
            Mapper.CreateMap<PlanContractViewModel, PlanContract>();
            Mapper.CreateMap<PlanContract, PlanContract>().ForMember(x => x.PlanLOB, opt => opt.Ignore()).ForMember(p => p.BusinessEntity, opt => opt.Ignore());
            Mapper.CreateMap<PlanContractDetailViewModel, PlanContractDetail>();
            Mapper.CreateMap<LOBAddressDetailViewModel, PlanLOBAddress>();
            Mapper.CreateMap<LOBContactDetailViewModel, PlanLOBContact>();
            Mapper.CreateMap<CredentialingAppointmentDetail, CredentialingAppointmentDetail>();
            Mapper.CreateMap<CredentialingAppointmentDetailViewModel, CredentialingAppointmentDetail>();
            Mapper.CreateMap<CredentialingAppointmentResultViewModel, CredentialingAppointmentResult>();
            
            Mapper.CreateMap<ProfileVerificationDetailViewModel, ProfileVerificationDetail>();
            Mapper.CreateMap<ProfileVerificationDetail, ProfileVerificationDetail>();

            Mapper.CreateMap<VerificationResultViewModel, VerificationResult>();
            Mapper.CreateMap<VerificationResult, VerificationResult>();
            
            Mapper.CreateMap<LoadedContractViewModel, LoadedContract>();
            Mapper.CreateMap<CredentialingVerificationInfo, CredentialingVerificationInfo>();
            Mapper.CreateMap<CredentialingLogViewModel, CredentialingLog>();
            Mapper.CreateMap<CredentialingCoveringPhysicianViewModel, CredentialingCoveringPhysician>();
            Mapper.CreateMap<CredentialingSpecialityListViewModel, CredentialingSpecialityList>();
            Mapper.CreateMap<CredentialingAppointmentScheduleViewModel, CredentialingAppointmentSchedule>();


            Mapper.CreateMap<LOBContactDetailViewModel, LOBContactDetail>();
            Mapper.CreateMap<LOBContactDetail, LOBContactDetail>();
            Mapper.CreateMap<LOBAddressDetailViewModel, LOBAddressDetail>();

            Mapper.CreateMap<ProfileVerificationInfo, CredentialingVerificationInfo>();
            Mapper.CreateMap<ProfileVerificationDetail, CredentialingProfileVerificationDetail>().ForMember(x => x.ProfileVerificationParameter, opt => opt.Ignore());
            Mapper.CreateMap<VerificationResult, CredentialingVerificationResult>();
            Mapper.CreateMap<CredentialingContractRequestViewModel, CredentialingContractRequest>().ForMember(x => x.ContractGrid, opt => opt.Ignore());

            Mapper.CreateMap<ProfileReportViewModel, ProfileReport>();
            Mapper.CreateMap<ProfileReport, ProfileReport>();

            Mapper.CreateMap<ProfileReport, ProfileReportViewModel>();

            #endregion

            #region CustomField

            Mapper.CreateMap<CustomFieldViewModel, CustomField>();
            Mapper.CreateMap<CustomFieldTransaction, CustomFieldTransaction>();
            Mapper.CreateMap<CustomFieldTransactionViewModel, CustomFieldTransaction>();
            Mapper.CreateMap<CustomFieldTransactionDataViewModel, CustomFieldTransactionData>();

            #endregion



            Mapper.CreateMap<ReCredenntialingInitiationViewModel, CredentialingContractRequest>();
            Mapper.CreateMap<ContractGridViewModel, ContractGrid>();
            Mapper.CreateMap<ContractSpecialtyViewModel, ContractSpecialty>();
            Mapper.CreateMap<ContractPracticeLocationViewModel, ContractPracticeLocation>();
            Mapper.CreateMap<ContractLOBViewModel, ContractLOB>();
            Mapper.CreateMap<CredentialingContractInfoFromPlanViewModel, CredentialingContractInfoFromPlan>();

            Mapper.CreateMap<EmailServiceViewModel, EmailInfo>();
            Mapper.CreateMap<EmailServiceViewModel, EmailRecurrenceDetail>();
            Mapper.CreateMap<EmailAttachment, EmailAttachment>();
            Mapper.CreateMap<EmailAttachmentViewModel, EmailAttachment>();
            Mapper.CreateMap<EmailGroupViewModel, EmailGroup>();
            Mapper.CreateMap<EmailGroup, EmailGroup>();


            Mapper.CreateMap<TimelineActivityViewModel, TimelineActivity>();

            Mapper.CreateMap<ProfileReviewSectionViewModel, ProfileReviewSection>();
            Mapper.CreateMap<ProfileReviewSection, ProfileReviewSectionViewModel>();

            Mapper.CreateMap<VerificationLinkViewModel, VerificationLink>();

            Mapper.CreateMap<DecredemtialingReasonViewModel, DecredentialingReason>();

            #region Credentialing Request

            Mapper.CreateMap<CredentialingRequestViewModel, CredentialingRequest>();
            Mapper.CreateMap<CredentialingRequest, CredentialingRequestViewModel>();


            Mapper.CreateMap<CredentialingRequestTrackerViewModel, CredentialingRequestTracker>();
            Mapper.CreateMap<CredentialingRequestTracker, CredentialingRequestTrackerViewModel>();

            #endregion

            #region Task Tracker

            Mapper.CreateMap<TaskTrackerViewModel, TaskTrackerBusinessModel>();
            Mapper.CreateMap<TaskTracker, TaskTracker>();

            #endregion

            #region Plan Form

            Mapper.CreateMap<PlanFormViewModel, PlanForm>();

            #endregion

        }
    }
}