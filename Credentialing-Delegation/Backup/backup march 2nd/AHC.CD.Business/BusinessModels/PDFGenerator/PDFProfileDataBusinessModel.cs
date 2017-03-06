using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.PDFGenerator
{
    public class PDFProfileDataBusinessModel
    {
        #region Personal Information and Professional IDs

        public string PersonalInfoProviderType { get; set; }
        public string PersonalInfoInpatientSetting { get; set; }

        #region Name

        public string PersonalInfoFirstName { get; set; }

        public string PersonalInfoMiddleName { get; set; }

        public string PersonalInfoLastName { get; set; }

        public string PersonalInfoAnotherNameExist { get; set; }

        public string PersonalInfoOtherFirstName { get; set; }

        public string PersonalInfoOtherMiddleName { get; set; }

        public string PersonalInfoOtherLastName { get; set; }

        public string PersonalInfoSuffix { get; set; }
       
        public string PersonalInfoOtherSuffix { get; set; }

        public string PersonalInfoDateStartOfOtherName { get; set; }
               
        public string PersonalInfoDateEndOfOtherName { get; set; }

        #endregion

        #region General Information

        public string PersonalInfoGender { get; set; }

        public string PersonalInfoDateOfBirth { get; set; }

        public string PersonalInfoCityOfBirth { get; set; }        

        public string PersonalInfoStateOfBirth { get; set; }

        public string PersonalInfoCountryOfBirth { get; set; }

        public string PersonalInfoSSN { get; set; }

        public string PersonalInfoFNIN { get; set; }

        public string PersonalInfoFNINCountryOfIssue { get; set; }

        //public string[] PersonalInfoNonEnglishLanguage { get; set; }

        public string PersonalInfoNonEnglishLanguage1 { get; set; }
        public string PersonalInfoNonEnglishLanguage2 { get; set; }
        public string PersonalInfoNonEnglishLanguage3 { get; set; }
        public string PersonalInfoNonEnglishLanguage4 { get; set; }
        public string PersonalInfoNonEnglishLanguage5 { get; set; }

        #endregion

        #region Home Address

        public string PersonalInfoNumber { get; set; }

        public string PersonalInfoStreet { get; set; }

        public string PersonalInfoAptNumber { get; set; }

        public string PersonalInfoCity { get; set; }

        public string PersonalInfoState { get; set; }

        public string PersonalInfoZipCode { get; set; }

        public string PersonalInfoTelephone { get; set; }

        public string PersonalInfoEmail { get; set; }

        public string PersonalInfoFax { get; set; }

        public string PersonalInfoPreferredMethodOfContact { get; set; }

        #endregion

        # region Professional IDs

        #region DEA
        public string ProfessionalIDsFederalDEANumber { get; set; }
        public string ProfessionalIDsFederalDEAIssueDate { get; set; }
        public string ProfessionalIDsFederalDEAStateOfReg { get; set; }
        public string ProfessionalIDsFederalDEAExpirationDate { get; set; }       
        #endregion

        #region CDS
        public string ProfessionalIDsCDSCertificateNumber { get; set; }
        public string ProfessionalIDsCDSIssueDate { get; set; }
        public string ProfessionalIDsCDSStateOfReg { get; set; }
        public string ProfessionalIDsCDSExpirationDate { get; set; }

        public string ProfessionalIDsCDSCertificateNumber2 { get; set; }
        public string ProfessionalIDsCDSIssueDate2 { get; set; }
        public string ProfessionalIDsCDSStateOfReg2 { get; set; }
        public string ProfessionalIDsCDSExpirationDate2 { get; set; }

        public string ProfessionalIDsCDSCertificateNumber3 { get; set; }
        public string ProfessionalIDsCDSIssueDate3 { get; set; }
        public string ProfessionalIDsCDSStateOfReg3 { get; set; }
        public string ProfessionalIDsCDSExpirationDate3 { get; set; }


        #endregion

        #region State License1
        public string ProfessionalIDsStateLicenseNumber1 { get; set; }
        public string ProfessionalIDsStateLicenseIssuingState1 { get; set; }
        public string ProfessionalIDsStateLicenseIssueDate1 { get; set; }
        public string ProfessionalIDsStateLicenseAreYouPractisingInThisState1 { get; set; }
        public string ProfessionalIDsStateLicenseExpirationDate1 { get; set; }
        public string ProfessionalIDsStateLicenseStatusCode1 { get; set; }
        public string ProfessionalIDsStateLicenseType1 { get; set; }
        #endregion

        #region State License2
        public string ProfessionalIDsStateLicenseNumber2 { get; set; }
        public string ProfessionalIDsStateLicenseIssuingState2 { get; set; }
        public string ProfessionalIDsStateLicenseIssueDate2 { get; set; }
        public string ProfessionalIDsStateLicenseAreYouPractisingInThisState2 { get; set; }
        public string ProfessionalIDsStateLicenseExpirationDate2 { get; set; }
        public string ProfessionalIDsStateLicenseStatusCode2 { get; set; }
        public string ProfessionalIDsStateLicenseType2 { get; set; }
        #endregion       

        #endregion

        # region Other ID Numbers
        public string OtherIDNumbersMedicareApproved { get; set; }
        public string OtherIDNumbersMedicareNumber { get; set; }
        public string OtherIDNumbersUPIN { get; set; }
        public string OtherIDNumbersMedicaidApproved { get; set; }
        public string OtherIDNumbersMedicaidNumber { get; set; }
        public string OtherIDNumbersMedicaidState { get; set; }
        public string OtherIDNumbersNPINumber { get; set; }
        public string OtherIDNumbersUSMLENumber { get; set; }
        public string OtherIDNumbersWorkersCompensationNumber { get; set; }
        public string OtherIDNumbersECFMGNumber { get; set; }
        public string OtherIDNumbersECFMGIssueDate { get; set; }
        #endregion

        #endregion

        #region Education & Training

        #region UnderGraduate School
        public string UnderGraduateSchoolOfficialName { get; set; }
        public string UnderGraduateSchoolAddress { get; set; }
        public string UnderGraduateSchoolCity { get; set; }
        public string UnderGraduateSchoolState { get; set; }
        public string UnderGraduateSchoolZip { get; set; }
        public string UnderGraduateSchoolCountryCode { get; set; }
        public string UnderGraduateSchoolTelephone { get; set; }
        public string UnderGraduateSchoolFax { get; set; }
        public string UnderGraduateSchoolStartDate { get; set; }
        public string UnderGraduateSchoolEndDate { get; set; }
        public string UnderGraduateSchoolDegreeAwarded { get; set; }
        public string UnderGraduateSchoolCompleteUnderGraduation { get; set; }
        #endregion

        #region Professional School
        public string ProfessionalSchoolGraduateType { get; set; }

        #region US or Canadian School
        public string USorCanadianSchoolCode { get; set; }
        public string USorCanadianSchoolName { get; set; }
        public DateTime? USorCanadianSchoolStartDate { get; set; }
        public DateTime? USorCanadianSchoolEndDate { get; set; }
        public string USorCanadianSchoolDegreeAwarded { get; set; }
        public string USorCanadianSchoolCompleteGraduation { get; set; }
        #endregion


        #region Non US or Canadian School
        public string NonUSorCanadianSchoolOfficialName { get; set; }
        public string NonUSorCanadianSchoolAddress { get; set; }
        public string NonUSorCanadianSchoolCity { get; set; }
        public string NonUSorCanadianSchoolCountryCode { get; set; }
        public string NonUSorCanadianSchoolPostalCode { get; set; }
        public string NonUSorCanadianSchoolStartDate { get; set; }
        public string NonUSorCanadianSchoolEndDate { get; set; }
        public string NonUSorCanadianSchoolDegreeAwarded { get; set; }
        public string NonUSorCanadianSchoolCompleteUnderGraduation { get; set; }
        #endregion        

        #endregion

        #region Training
        public string TrainingInstitutionOrHospitalName { get; set; }
        public string TrainingSchoolCode { get; set; }
        public string TrainingNumber { get; set; }
        public string TrainingStreet { get; set; }
        public string TrainingSuiteBuilding { get; set; }
        public string TrainingCity { get; set; }
        public string TrainingState { get; set; }
        public string TrainingZip { get; set; }
        public string TrainingCountryCode { get; set; }
        public string TrainingTelephone { get; set; }
        public string TrainingFax { get; set; }
        public string TrainingCompleteInSchool { get; set; }
        public string TrainingIfNotCompleteExplain { get; set; }

        #region Internship/Residency1
        public string Type1 { get; set; }
        public string StartDate1 { get; set; }
        public string EndDate1 { get; set; }
        public string DepartmentSpecialty1 { get; set; }
        public string NameOfDirector1 { get; set; }
        #endregion

        #region Internship/Residency2
        public string Type2 { get; set; }
        public string StartDate2 { get; set; }
        public string EndDate2 { get; set; }
        public string DepartmentSpecialty2 { get; set; }
        public string NameOfDirector2 { get; set; }
        #endregion

        #region Internship/Residency3
        public string Type3 { get; set; }
        public string StartDate3 { get; set; }
        public string EndDate3 { get; set; }
        public string DepartmentSpecialty3 { get; set; }
        public string NameOfDirector3 { get; set; }
        #endregion

        #endregion       


        #endregion

        #region Professional / Medical Specialty Information
        #region Primary Specialty
        public string PrimarySpecialtyCode { get; set; }
        public string PrimarySpecialtyInitialCertificationDate { get; set; }
        public string PrimarySpecialtyReCertificationDate { get; set; }
        public string PrimarySpecialtyExpirationDate { get; set; }
        public string PrimarySpecialtyBoardCertified{ get; set; }
        public string PrimarySpecialtyCertifyingBoardCode{ get; set; }
        public string PrimarySpecialtyHMO{ get; set; }
        public string PrimarySpecialtyPPO{ get; set; }
        public string PrimarySpecialtyPOS{ get; set; }

        #region If not Board Certified
        public string PrimarySpecialtyExamStatus { get; set; }
        //public string PrimarySpecialtyIntendToSitForExam { get; set; }
        //public string PrimarySpecialtyDontIntendToTakeExam { get; set; }
        //public string PrimarySpecialtyCertifyingBoardCode { get; set; }
        public DateTime? PrimarySpecialtyDate { get; set; }       //No label given for this
        public string PrimarySpecialtyReasonForNotTakingExam { get; set; }
        #endregion

        #endregion

        #region Secondary Specialty
        public string SecondarySpecialtyCode { get; set; }
        public string SecondarySpecialtyInitialCertificationDate { get; set; }
        public string SecondarySpecialtyReCertificationDate { get; set; }
        public string SecondarySpecialtyExpirationDate { get; set; }
        public string SecondarySpecialtyBoardCertified { get; set; }
        public string SecondarySpecialtyCertifyingBoardCode { get; set; }
        public string SecondarySpecialtyHMO { get; set; }
        public string SecondarySpecialtyPPO { get; set; }
        public string SecondarySpecialtyPOS { get; set; }

        #region If not Board Certified
        public string SecondarySpecialtyExamStatus { get; set; }
        //public string SecondarySpecialtyIntendToSitForExam { get; set; }
        //public string SecondarySpecialtyDontIntendToTakeExam { get; set; }
        //public string SecondarySpecialtyCertifyingBoardCode { get; set; }
        public DateTime? SecondarySpecialtyDate { get; set; }       //No label given for this
        public string SecondarySpecialtyReasonForNotTakingExam { get; set; }
        #endregion

        #endregion

        #region Certifications
        public string BASICLIFESUPPORT { get; set; }
        public DateTime? BASICLIFESUPPORTExpirationDate { get; set; }
        public string CPR { get; set; }
        public DateTime? CPRExpirationDate { get; set; }
        public string ADVCARDIACLIFESPT { get; set; }
        public DateTime? ADVCARDIACLIFESPTExpirationDate { get; set; }
        public string NEONATALADVANCEDLIFESPT { get; set; }
        public DateTime? NEONATALADVANCEDLIFESPTExpirationDate { get; set; }
        public string PEDIATRICADVANCEDLIFESPT { get; set; }
        public DateTime? PEDIATRICADVANCEDLIFESPTExpirationDate { get; set; }
        public string ADVTRAUMALIFESUPPORT { get; set; }
        public DateTime? ADVTRAUMALIFESUPPORTExpirationDate { get; set; }
        public string ADVLIFESUPPORTINOB { get; set; }
        public DateTime? ADVLIFESUPPORTINOBExpirationDate { get; set; }
        #endregion

        #region PractiseInterest
        public string PractiseInterest { get; set; }
        #endregion

        #region Primary Credentialing Contact

        public string PrimaryCredentialingContactLastName { get; set; }
        public string PrimaryCredentialingContactFirstName { get; set; }
        public string PrimaryCredentialingContactMI { get; set; }
        public string PrimaryCredentialingContactNumber { get; set; }
        public string PrimaryCredentialingContactStreet { get; set; }
        public string PrimaryCredentialingContactSuiteBuilding { get; set; }
        public string PrimaryCredentialingContactCity { get; set; }
        public string PrimaryCredentialingContactState { get; set; }
        public string PrimaryCredentialingContactZip { get; set; }
        public string PrimaryCredentialingContactTelephone { get; set; }
        public string PrimaryCredentialingContactFax { get; set; }
        public string PrimaryCredentialingContactEmail { get; set; }
        #endregion

        #endregion

        #region Practise Location Information
        #region Primary Practise Location
        public string CurrentlyPractisingAtThisAddress { get; set; }
        public string PrimaryPractiseLocationStartDate { get; set; }
        public string PrimaryPractiseLocationPhysicianGroup { get; set; }
        public string PrimaryPractiseLocationCorporateName { get; set; }
        public string PrimaryPractiseLocationNumber { get; set; }
        public string PrimaryPractiseLocationStreet { get; set; }
        public string PrimaryPractiseLocationSuiteBuilding { get; set; }
        public string PrimaryPractiseLocationCity { get; set; }
        public string PrimaryPractiseLocationState { get; set; }
        public string PrimaryPractiseLocationZip { get; set; }
        public string PrimaryPractiseLocationSendGeneralCorrespondance { get; set; }
        public string PrimaryPractiseLocationTelephone { get; set; }
        public string PrimaryPractiseLocationFax { get; set; }
        public string PrimaryPractiseLocationOfficialEmail { get; set; }
        public string PrimaryPractiseLocationIndividualTaxId { get; set; }
        public int? PrimaryPractiseLocationGroupTaxId { get; set; }
        public string PrimaryPractiseLocationPrimaryTaxId { get; set; }
        #endregion

        #region Office Manager
        public string OfficeManagerLastName { get; set; }
        public string OfficeManagerFirstName { get; set; }
        public string OfficeManagerMI { get; set; }
        public string OfficeManagerTelephone { get; set; }
        public string OfficeManagerFax { get; set; }
        public string OfficeManagerEmail { get; set; }
        #endregion

        #region Billing Contact
        public string BillingContactLastName { get; set; }
        public string BillingContactFirstName { get; set; }
        public string BillingContactMI { get; set; }
        public string BillingContactNumber { get; set; }
        public string BillingContactStreet { get; set; }
        public string BillingContactSuiteBuilding { get; set; }
        public string BillingContactCity { get; set; }
        public string BillingContactState{ get; set; }
        public string BillingContactZip { get; set; }
        public string BillingContactTelephone { get; set; }
        public string BillingContactFax { get; set; }
        public string BillingContactEmail { get; set; }
        #endregion

        #region Payment Remittance
        public string PaymentRemittanceElectronicBillingCapabilities { get; set; }
        public string PaymentRemittancBillingDepartment { get; set; }
        public string PaymentRemittanceCheckPayableTo { get; set; }
        public string PaymentRemittanceLastName { get; set; }
        public string PaymentRemittanceFirstName { get; set; }
        public string PaymentRemittanceMI { get; set; }
        public string PaymentRemittanceNumber { get; set; }
        public string PaymentRemittanceStreet { get; set; }
        public string PaymentRemittanceSuiteBuilding { get; set; }
        public string PaymentRemittanceCity { get; set; }
        public string PaymentRemittanceState { get; set; }
        public string PaymentRemittanceZip { get; set; }
        public string PaymentRemittanceTelephone { get; set; }
        public string PaymentRemittanceFax { get; set; }
        public string PaymentRemittanceEmail { get; set; }
        #endregion

        #region OfficeHours
        public string StartMonday { get; set; }
        public string StartTuesday { get; set; }
        public string StartWednesday { get; set; }
        public string StartThursday { get; set; }
        public string StartFriday { get; set; }
        public string StartSaturday { get; set; }
        public string StartSunday { get; set; }

        public string EndMonday { get; set; }
        public string EndTuesday { get; set; }
        public string EndWednesday { get; set; }
        public string EndThursday { get; set; }
        public string EndFriday { get; set; }
        public string EndSaturday { get; set; }
        public string EndSunday { get; set; }

        public string PhoneCoverage { get; set; }
        public string TypeOfAnsweringService { get; set; }
        public string AfterOfficeHoursOfficeTelephone { get; set; }

        #endregion

        #region OpenPractiseStatus
        public string OpenPractiseStatusACCEPTNEWPATIENTSINTOTHISPRACTICE { get; set; }
        public string OpenPractiseStatusACCEPTEXISTINGPATIENTSWITHCHANGEOFPAYOR { get; set; }
        public string OpenPractiseStatusACCEPTNEWPATIENTSWITHPHYSICIANREFERRAL { get; set; }
        public string OpenPractiseStatusACCEPTALLNEWPATIENTS { get; set; }
        public string OpenPractiseStatusACCEPTNEWMEDICAREPATIENTS { get; set; }
        public string OpenPractiseStatusACCEPTNEWMEDICAIDPATIENTS { get; set; }
        public string OpenPractiseStatusExplain { get; set; }
        public string OpenPractiseStatusPRACTICELIMITATIONS { get; set; }
        public string OpenPractiseStatusGENDERLIMITATIONS { get; set; }
        public int? OpenPractiseStatusMINIMUMAGELIMITATIONS  { get; set; }
        public int? OpenPractiseStatusMAXIMUMAGELIMITATIONS { get; set; }
        public string OpenPractiseStatusOtherLIMITATIONS { get; set; }
        #endregion

        #region Mid-Level Practitioner
        
        public string CareForPatients { get; set; }
        #region Mid-Level Practitioner1
        public string MidLevelPractitionerLastName1 { get; set; }
        public string MidLevelPractitionerFirstName1 { get; set; }
        public string MidLevelPractitionerMI1 { get; set; }
        public string MidLevelPractitionerType1 { get; set; }
        public string MidLevelPractitionerLicenseNumber1 { get; set; }
        public string MidLevelPractitionerState1 { get; set; }
        #endregion

        #region Mid-Level Practitioner2
        public string MidLevelPractitionerLastName2 { get; set; }
        public string MidLevelPractitionerFirstName2 { get; set; }
        public string MidLevelPractitionerMI2 { get; set; }
        public string MidLevelPractitionerType2 { get; set; }
        public string MidLevelPractitionerLicenseNumber2 { get; set; }
        public string MidLevelPractitionerState2 { get; set; }
        #endregion

        #region Mid-Level Practitioner3
        public string MidLevelPractitionerLastName3 { get; set; }
        public string MidLevelPractitionerFirstName3 { get; set; }
        public string MidLevelPractitionerMI3 { get; set; }
        public string MidLevelPractitionerType3 { get; set; }
        public string MidLevelPractitionerLicenseNumber3 { get; set; }
        public string MidLevelPractitionerState3 { get; set; }
        #endregion

        #region Mid-Level Practitioner4
        public string MidLevelPractitionerLastName4 { get; set; }
        public string MidLevelPractitionerFirstName4 { get; set; }
        public string MidLevelPractitionerMI4 { get; set; }
        public string MidLevelPractitionerType4 { get; set; }
        public string MidLevelPractitionerLicenseNumber4 { get; set; }
        public string MidLevelPractitionerState4 { get; set; }
        #endregion

        #region Mid-Level Practitioner5
        public string MidLevelPractitionerLastName5 { get; set; }
        public string MidLevelPractitionerFirstName5 { get; set; }
        public string MidLevelPractitionerMI5 { get; set; }
        public string MidLevelPractitionerType5 { get; set; }
        public string MidLevelPractitionerLicenseNumber5 { get; set; }
        public string MidLevelPractitionerState5 { get; set; }
        #endregion
        #endregion

        #region Languages
        //public string[] NonEnglishLanguages { get; set; }
        public string NonEnglishLanguage1 { get; set; }
        public string NonEnglishLanguage2 { get; set; }
        public string NonEnglishLanguage3 { get; set; }
        public string NonEnglishLanguage4 { get; set; }
        public string NonEnglishLanguage5 { get; set; }

        public string InterpreterAvailable { get; set; }

       // public string[] LanguagesInterpreted { get; set; }
        public string LanguagesInterpreted1 { get; set; }
        public string LanguagesInterpreted2 { get; set; }
        public string LanguagesInterpreted3 { get; set; }
        public string LanguagesInterpreted4 { get; set; }

        #endregion

        #region Accessibilities
        public string  ADAREQUIREMENTS  { get; set; }
        public string  HANDICAPPEDACCESSForBuilding { get; set; }
        public string  HANDICAPPEDACCESSForRESTROOM { get; set; }
        public string  HANDICAPPEDACCESSForPARKING { get; set; }
        public string  OTHERSERVICESForDISABLED{ get; set; }
        public string  TextTELEPHONY { get; set; }
        public string  AMERICANSIGNLANGUAGE { get; set; }
        public string  MENTALPhysicalIMPAIRMENTSERVICES { get; set; }
        public string  AccessibleByPublicTransport { get; set; }
        public string  Bus { get; set; }
        public string  Subway { get; set; }
        public string  RegionalTrain { get; set; }
        public string  OtherHANDICAPPEDAccess { get; set; }
        public string  OtherDisabilityServices{ get; set; }
        public string  OtherTransportationAccess{ get; set; }
        #endregion

        #region Services
        public string LaboratoryServices { get; set; }
        public string LaboratoryServicesAccreditingProgram { get; set; }
        public string RadiologyServices { get; set; }
        public string RadiologyServicesXrayCertificationType { get; set; }
        public string EKGS { get; set; }
        public string AllergyInjections { get; set; }
        public string AllergySkinTesting { get; set; }
        public string RoutineOfficeGynecology { get; set; }
        public string DrawingBlood { get; set; }
        public string AgeImmunizations { get; set; }
        public string FlexibleSIGMOIDOSCOPY { get; set; }
        public string TYMPANOMETRYAUDIOMETRYSCREENING { get; set; }
        public string ASTHMATreatment { get; set; }
        public string OSTEOPATHICManipulation { get; set; }
        public string IVHYDRATIONTREATMENT { get; set; }
        public string CARDIACSTRESSTEST { get; set; }
        public string PULMONARYFUNCTIONTESTING { get; set; }
        public string PhysicalTheraphy { get; set; }
        public string MinorLaseractions { get; set; }
        public string AnesthesiaAdministered { get; set; }
        public string ClassCategory { get; set; }
        public string AdministerLastName { get; set; }
        public string AdministerFirstName { get; set; }
        public string PractiseType { get; set; }
        public string AdditionalOfficeProcedures { get; set; }
        #endregion

        #region Partners / Associates1
        public string PartnersLastName1 { get; set; }
        public string PartnersFirstName1 { get; set; }
        public string PartnersSpecialtyCode1 { get; set; }
        public string PartnersCoveringCollegue1 { get; set; }
        public string PartnersMI1 { get; set; }
        public string PartnersProviderType1 { get; set; }
        #endregion

        #region Partners / Associates2
        public string PartnersLastName2 { get; set; }
        public string PartnersFirstName2 { get; set; }
        public string PartnersSpecialtyCode2 { get; set; }
        public string PartnersCoveringCollegue2 { get; set; }
        public string PartnersMI2 { get; set; }
        public string PartnersProviderType2 { get; set; }
        #endregion

        #region Partners / Associates3
        public string PartnersLastName3 { get; set; }
        public string PartnersFirstName3 { get; set; }
        public string PartnersSpecialtyCode3 { get; set; }
        public string PartnersCoveringCollegue3 { get; set; }
        public string PartnersMI3 { get; set; }
        public string PartnersProviderType3 { get; set; }
        #endregion

        #region Covering Colleagues

        public string[] CoveringColleguesLastName { get; set; }
        public string[] CoveringColleguesFirstName { get; set; }
        public string[] CoveringColleguesSpecialtyCode { get; set; }
        public string[] CoveringColleguesMI { get; set; }
        public string[] CoveringColleguesProviderType { get; set; }

        #endregion

        #region CoveringCollegues1
        public string CoveringColleguesLastName1 { get; set; }
        public string CoveringColleguesFirstName1 { get; set; }
        public string CoveringColleguesSpecialtyCode1 { get; set; }
        public string CoveringColleguesMI1 { get; set; }
        public string CoveringColleguesProviderType1 { get; set; }
        #endregion

        #region CoveringCollegues2
        public string CoveringColleguesLastName2 { get; set; }
        public string CoveringColleguesFirstName2 { get; set; }
        public string CoveringColleguesSpecialtyCode2 { get; set; }
        public string CoveringColleguesMI2 { get; set; }
        public string CoveringColleguesProviderType2 { get; set; }
        #endregion

        #region CoveringCollegues3
        public string CoveringColleguesLastName3 { get; set; }
        public string CoveringColleguesFirstName3 { get; set; }
        public string CoveringColleguesSpecialtyCode3 { get; set; }
        public string CoveringColleguesMI3 { get; set; }
        public string CoveringColleguesProviderType3 { get; set; }
        #endregion

#endregion

        #region Hospital Affiliations

        #region Admitting Arrangements
        public string HaveHospitalPrivilege { get; set; }
        public string AdmittingArrangementType { get; set; }
        #endregion

        #region Hospital Privileges
        public string HospitalName { get; set; }
        public string HospitalNumber { get; set; }
        public string HospitalStreet { get; set; }
        public string HospitalSuiteBuilding { get; set; }
        public string HospitalCity { get; set; }
        public string HospitalState { get; set; }
        public string HospitalZip { get; set; }
        public string HospitalTelephone { get; set; }
        public string HospitalFax { get; set; }
        public string HospitalDepartmentName { get; set; }
        public string HospitalDepartmentDirectorLastName { get; set; }
        public string HospitalDepartmentDirectorFirstName { get; set; }
        public string HospitalAffiliationStartDate { get; set; }
        public string HospitalAffiliationEndDate { get; set; }
        public string HospitalUnrestrictedPrivilege { get; set; }
        public string HospitalPrivilegesTemporary { get; set; }
        public string HospitalAdmittingPrivilegeStatus { get; set; }
        public double? HospitalPercentageAnnualAdmission { get; set; }
        #endregion

        #region Other Hospital Privileges
        public string OtherHospitalName { get; set; }
        public string OtherHospitalNumber { get; set; }
        public string OtherHospitalStreet { get; set; }
        public string OtherHospitalSuiteBuilding { get; set; }
        public string OtherHospitalCity { get; set; }
        public string OtherHospitalState { get; set; }
        public string OtherHospitalZip { get; set; }
        public string OtherHospitalTelephone { get; set; }
        public string OtherHospitalFax { get; set; }
        public string OtherHospitalDepartmentName { get; set; }
        public string OtherHospitalDepartmentDirectorLastName { get; set; }
        public string OtherHospitalDepartmentDirectorFirstName { get; set; }
        public string OtherHospitalAffiliationStartDate { get; set; }
        public string OtherHospitalAffiliationEndDate { get; set; }
        public string OtherHospitalUnrestrictedPrivilege { get; set; }
        public string OtherHospitalPrivilegesTemporary { get; set; }
        public string OtherHospitalAdmittingPrivilegeStatus { get; set; }
        public double? OtherHospitalPercentageAnnualAdmission { get; set; }
        public string OtherHospitalExplainTerminatedAffiliation { get; set; }
        #endregion
        
        #endregion

        #region Professional Liability Insurance Carrier1
        public string ProfessionalLiabilityCarrierName1 { get; set; }
        public string ProfessionalLiabilitySelfInsured1 { get; set; }
        public string ProfessionalLiabilityNumber1 { get; set; }
        public string ProfessionalLiabilityStreet1 { get; set; }
        public string ProfessionalLiabilitySuiteBuilding1 { get; set; }
        public string ProfessionalLiabilityCity1 { get; set; }
        public string ProfessionalLiabilityState1 { get; set; }
        public string ProfessionalLiabilityZip1 { get; set; }
        public string ProfessionalLiabilityOriginalEffectiveDate1 { get; set; }
        public string ProfessionalLiabilityEffectiveDate1 { get; set; }
        public string ProfessionalLiabilityExpirationDate1 { get; set; }
        public string ProfessionalLiabilityCoverageType1 { get; set; }
        public string ProfessionalLiabilityHAVEUNLIMITEDCOVERAGE1  { get; set; }
        public string ProfessionalLiabilityTAILCOVERAGE1 { get; set; }
        public double? AMOUNTOFCOVERAGEPEROCCURRENCE1 { get; set; }
        public double? AMOUNTOFCOVERAGEAGGREGATE1 { get; set; }
        public string POLICYNUMBER1 { get; set; }
        #endregion

        #region Professional Liability Insurance Carrier2
        public string ProfessionalLiabilityCarrierName2 { get; set; }
        public string ProfessionalLiabilitySelfInsured2 { get; set; }
        public string ProfessionalLiabilityNumber2 { get; set; }
        public string ProfessionalLiabilityStreet2 { get; set; }
        public string ProfessionalLiabilitySuiteBuilding2 { get; set; }
        public string ProfessionalLiabilityCity2 { get; set; }
        public string ProfessionalLiabilityState2 { get; set; }
        public string ProfessionalLiabilityZip2 { get; set; }
        public string ProfessionalLiabilityOriginalEffectiveDate2 { get; set; }
        public string ProfessionalLiabilityEffectiveDate2 { get; set; }
        public string ProfessionalLiabilityExpirationDate2 { get; set; }
        public string ProfessionalLiabilityCoverageType2 { get; set; }
        public string ProfessionalLiabilityHAVEUNLIMITEDCOVERAGE2 { get; set; }
        public string ProfessionalLiabilityTAILCOVERAGE2 { get; set; }
        public double? AMOUNTOFCOVERAGEPEROCCURRENCE2 { get; set; }
        public double? AMOUNTOFCOVERAGEAGGREGATE2 { get; set; }
        public string POLICYNUMBER2 { get; set; }
        #endregion

        #region Work History and Reference

        #region Military Duty
        public string ActiveMilitaryDuty { get; set; }
        #endregion

        #region Work History1
        public string WorkHistoryPractiseName1 { get; set; }
        public string WorkHistoryNumber1 { get; set; }
        public string WorkHistoryStreet1 { get; set; }
        public string WorkHistorySuiteBuilding1 { get; set; }
        public string WorkHistoryCity1 { get; set; }
        public string WorkHistoryState1 { get; set; }
        public string WorkHistoryZip1 { get; set; }
        public string WorkHistoryTelephone1 { get; set; }
        public string WorkHistoryFax1 { get; set; }
        public string WorkHistoryCountryCode1 { get; set; }
        public string WorkHistoryStartDate1 { get; set; }
        public string WorkHistoryEndDate1 { get; set; }
        public string WorkHistoryReason1 { get; set; }
        #endregion

        #region Work History2
        public string WorkHistoryPractiseName2 { get; set; }
        public string WorkHistoryNumber2 { get; set; }
        public string WorkHistoryStreet2 { get; set; }
        public string WorkHistorySuiteBuilding2 { get; set; }
        public string WorkHistoryCity2 { get; set; }
        public string WorkHistoryState2 { get; set; }
        public string WorkHistoryZip2 { get; set; }
        public string WorkHistoryTelephone2 { get; set; }
        public string WorkHistoryFax2 { get; set; }
        public string WorkHistoryCountryCode2 { get; set; }
        public string WorkHistoryStartDate2 { get; set; }
        public string WorkHistoryEndDate2 { get; set; }
        public string WorkHistoryReason2 { get; set; }
        #endregion

        #region Work History3
        public string WorkHistoryPractiseName3 { get; set; }
        public string WorkHistoryNumber3 { get; set; }
        public string WorkHistoryStreet3 { get; set; }
        public string WorkHistorySuiteBuilding3 { get; set; }
        public string WorkHistoryCity3 { get; set; }
        public string WorkHistoryState3 { get; set; }
        public string WorkHistoryZip3 { get; set; }
        public string WorkHistoryTelephone3 { get; set; }
        public string WorkHistoryFax3 { get; set; }
        public string WorkHistoryCountryCode3 { get; set; }
        public string WorkHistoryStartDate3 { get; set; }
        public string WorkHistoryEndDate3 { get; set; }
        public string WorkHistoryReason3 { get; set; }
        #endregion


        #region Gaps in Professional / Work History
        public string GapStartDate { get; set; }
        public string GapEndDate { get; set; }
        public string GapReason { get; set; }
        #endregion

        #endregion

        #region Professional References
        public string[] ProfessionalReferenceLastName { get; set; }
        public string[] ProfessionalReferenceFirstName { get; set; }
        public string[] ProfessionalReferenceProviderType { get; set; }
        public string[] ProfessionalReferenceNumber { get; set; }
        public string[] ProfessionalReferenceStreet { get; set; }
        public string[] ProfessionalReferenceSuiteBuilding { get; set; }
        public string[] ProfessionalReferenceCity { get; set; }
        public string[] ProfessionalReferenceState { get; set; }
        public string[] ProfessionalReferenceZip { get; set; }
        public string[] ProfessionalReferenceTelephone { get; set; }
        public string[] ProfessionalReferenceFax { get; set; }
        #endregion

        #region Professional References1
        public string ProfessionalReferenceLastName1 { get; set; }
        public string ProfessionalReferenceFirstName1 { get; set; }
        public string ProfessionalReferenceProviderType1 { get; set; }
        public string ProfessionalReferenceNumber1 { get; set; }
        public string ProfessionalReferenceStreet1 { get; set; }
        public string ProfessionalReferenceSuiteBuilding1 { get; set; }
        public string ProfessionalReferenceCity1 { get; set; }
        public string ProfessionalReferenceState1 { get; set; }
        public string ProfessionalReferenceZip1 { get; set; }
        public string ProfessionalReferenceTelephone1 { get; set; }
        public string ProfessionalReferenceFax1 { get; set; }
        #endregion

        #region Professional References2
        public string ProfessionalReferenceLastName2 { get; set; }
        public string ProfessionalReferenceFirstName2 { get; set; }
        public string ProfessionalReferenceProviderType2 { get; set; }
        public string ProfessionalReferenceNumber2 { get; set; }
        public string ProfessionalReferenceStreet2 { get; set; }
        public string ProfessionalReferenceSuiteBuilding2 { get; set; }
        public string ProfessionalReferenceCity2 { get; set; }
        public string ProfessionalReferenceState2 { get; set; }
        public string ProfessionalReferenceZip2 { get; set; }
        public string ProfessionalReferenceTelephone2 { get; set; }
        public string ProfessionalReferenceFax2 { get; set; }
        #endregion

        #region Professional References3
        public string ProfessionalReferenceLastName3 { get; set; }
        public string ProfessionalReferenceFirstName3 { get; set; }
        public string ProfessionalReferenceProviderType3 { get; set; }
        public string ProfessionalReferenceNumber3 { get; set; }
        public string ProfessionalReferenceStreet3 { get; set; }
        public string ProfessionalReferenceSuiteBuilding3 { get; set; }
        public string ProfessionalReferenceCity3 { get; set; }
        public string ProfessionalReferenceState3 { get; set; }
        public string ProfessionalReferenceZip3 { get; set; }
        public string ProfessionalReferenceTelephone3 { get; set; }
        public string ProfessionalReferenceFax3 { get; set; }
        #endregion

        #region Disclosure Questions

        #region Licensure
        public string LicensureQn1 { get; set; }
        public string LicensureQn2 { get; set; }
        #endregion

        #region HOSPITAL PRIVILEGES AND OTHER  AFFILIATIONS
        public string HOSPITALPRIVILEGESANDOTHERAFFILIATIONSQn1 { get; set; }
        public string HOSPITALPRIVILEGESANDOTHERAFFILIATIONSQn2 { get; set; }
        public string HOSPITALPRIVILEGESANDOTHERAFFILIATIONSQn3 { get; set; }
        #endregion

        #region EDUCATION, TRAINING AND BOARD CERTIFICATION
        public string EDUCATIONTRAININGANDBOARDCERTIFICATIONQn1 { get; set; }
        public string EDUCATIONTRAININGANDBOARDCERTIFICATIONQn2 { get; set; }
        public string EDUCATIONTRAININGANDBOARDCERTIFICATIONQn3 { get; set; }
        public string EDUCATIONTRAININGANDBOARDCERTIFICATIONQn4 { get; set; }
        #endregion

        #region DEA OR STATE CONTROLLED SUBSTANCE REGISTRATION
        public string DEAORSTATECONTROLLEDSUBSTANCEREGISTRATIONQn1 { get; set; }
        #endregion

        #region MEDICARE, MEDICAID OR OTHER GOVERNMENTAL PROGRAM PARTICIPATION
        public string MEDICAREMEDICAIDOROTHERGOVERNMENTALPROGRAMPARTICIPATIONQn1 { get; set; }
        #endregion

        #region OTHER SANCTIONS OR INVESTIGATIONS
        public string OTHERSANCTIONSORINVESTIGATIONSQn1 { get; set; }
        public string OTHERSANCTIONSORINVESTIGATIONSQn2 { get; set; }
        public string OTHERSANCTIONSORINVESTIGATIONSQn3 { get; set; }
        public string OTHERSANCTIONSORINVESTIGATIONSQn4 { get; set; }
        public string OTHERSANCTIONSORINVESTIGATIONSQn5 { get; set; }
        #endregion

        #region PROFESSIONAL LIABILITY INSURANCE INFORMATION AND CLAIMS HISTORY
        public string PROFESSIONALLIABILITYINSURANCEINFORMATIONANDCLAIMSHISTORYQn1 { get; set; }
        public string PROFESSIONALLIABILITYINSURANCEINFORMATIONANDCLAIMSHISTORYQn2 { get; set; }
        #endregion

        #region Malpractise Claim History
        public string MalpractiseClaimHistoryQn1 { get; set; }
        #endregion

        #region CRIMINAL/CIVIL HISTORY
        public string CRIMINALCIVILHISTORYQn1 { get; set; }
        public string CRIMINALCIVILHISTORYQn2 { get; set; }
        public string CRIMINALCIVILHISTORYQn3 { get; set; }
        #endregion

        #region ABILITY TO PERFORM JOB
        public string ABILITYTOPERFORMJOBQn1 { get; set; }
        public string ABILITYTOPERFORMJOBQn2 { get; set; }
        public string ABILITYTOPERFORMJOBQn3 { get; set; }
        public string ABILITYTOPERFORMJOBQn4 { get; set; }
        #endregion

        #endregion

    }
}
