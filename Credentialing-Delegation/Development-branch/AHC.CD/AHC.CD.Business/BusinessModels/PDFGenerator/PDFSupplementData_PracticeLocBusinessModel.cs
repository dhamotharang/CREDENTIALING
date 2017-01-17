using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.PDFGenerator
{
    public class PDFSupplementData_PracticeLocBusinessModel
    {
        #region Practise Location Information

        #region  Practise Location
        public string SupplementCurrentlyPractisingAtThisAddress { get; set; }
        public string SupplementPractiseLocationStartDate { get; set; }
        public string SupplementPractiseLocationPhysicianGroup { get; set; }
        public string SupplementPractiseLocationCorporateName { get; set; }
        public string SupplementPractiseLocationNumber { get; set; }
        public string SupplementPractiseLocationStreet { get; set; }
        public string SupplementPractiseLocationSuiteBuilding { get; set; }
        public string SupplementPractiseLocationCity { get; set; }
        public string SupplementPractiseLocationState { get; set; }
        public string SupplementPractiseLocationZip { get; set; }
        public string SupplementPractiseLocationSendGeneralCorrespondance { get; set; }
        public string SupplementPractiseLocationTelephone { get; set; }
        public string SupplementPractiseLocationFax { get; set; }
        public string SupplementPractiseLocationOfficialEmail { get; set; }
        public string SupplementPractiseLocationIndividualTaxId { get; set; }
        public int? SupplementPractiseLocationGroupTaxId { get; set; }
        public string SupplementPractiseLocationPrimaryTaxId { get; set; }
        #endregion

        #region Office Manager
        public string SupplementOfficeManagerLastName { get; set; }
        public string SupplementOfficeManagerFirstName { get; set; }
        public string SupplementOfficeManagerMI { get; set; }
        public string SupplementOfficeManagerTelephone { get; set; }
        public string SupplementOfficeManagerFax { get; set; }
        public string SupplementOfficeManagerEmail { get; set; }
        #endregion

        #region Billing Contact
        public string SupplementBillingContactLastName { get; set; }
        public string SupplementBillingContactFirstName { get; set; }
        public string SupplementBillingContactMI { get; set; }
        public string SupplementBillingContactNumber { get; set; }
        public string SupplementBillingContactStreet { get; set; }
        public string SupplementBillingContactSuiteBuilding { get; set; }
        public string SupplementBillingContactCity { get; set; }
        public string SupplementBillingContactState { get; set; }
        public string SupplementBillingContactZip { get; set; }
        public string SupplementBillingContactTelephone { get; set; }
        public string SupplementBillingContactFax { get; set; }
        public string SupplementBillingContactEmail { get; set; }
        #endregion

        #region Payment Remittance
        public string SupplementPaymentRemittanceElectronicBillingCapabilities { get; set; }
        public string SupplementPaymentRemittancBillingDepartment { get; set; }
        public string SupplementPaymentRemittanceCheckPayableTo { get; set; }
        public string SupplementPaymentRemittanceLastName { get; set; }
        public string SupplementPaymentRemittanceFirstName { get; set; }
        public string SupplementPaymentRemittanceMI { get; set; }
        public string SupplementPaymentRemittanceNumber { get; set; }
        public string SupplementPaymentRemittanceStreet { get; set; }
        public string SupplementPaymentRemittanceSuiteBuilding { get; set; }
        public string SupplementPaymentRemittanceCity { get; set; }
        public string SupplementPaymentRemittanceState { get; set; }
        public string SupplementPaymentRemittanceZip { get; set; }
        public string SupplementPaymentRemittanceTelephone { get; set; }
        public string SupplementPaymentRemittanceFax { get; set; }
        public string SupplementPaymentRemittanceEmail { get; set; }
        #endregion

        #region OfficeHours
        public string SupplementStartMonday { get; set; }
        public string SupplementStartTuesday { get; set; }
        public string SupplementStartWednesday { get; set; }
        public string SupplementStartThursday { get; set; }
        public string SupplementStartFriday { get; set; }
        public string SupplementStartSaturday { get; set; }
        public string SupplementStartSunday { get; set; }

        public string SupplementEndMonday { get; set; }
        public string SupplementEndTuesday { get; set; }
        public string SupplementEndWednesday { get; set; }
        public string SupplementEndThursday { get; set; }
        public string SupplementEndFriday { get; set; }
        public string SupplementEndSaturday { get; set; }
        public string SupplementEndSunday { get; set; }
                      
        public string SupplementPhoneCoverage { get; set; }
        public string SupplementTypeOfAnsweringService { get; set; }
        public string SupplementAfterOfficeHoursOfficeTelephone { get; set; }

        #endregion

        #region OpenPractiseStatus
        public string SupplementOpenPractiseStatusACCEPTNEWPATIENTSINTOTHISPRACTICE { get; set; }
        public string SupplementOpenPractiseStatusACCEPTEXISTINGPATIENTSWITHCHANGEOFPAYOR { get; set; }
        public string SupplementOpenPractiseStatusACCEPTNEWPATIENTSWITHPHYSICIANREFERRAL { get; set; }
        public string SupplementOpenPractiseStatusACCEPTALLNEWPATIENTS { get; set; }
        public string SupplementOpenPractiseStatusACCEPTNEWMEDICAREPATIENTS { get; set; }
        public string SupplementOpenPractiseStatusACCEPTNEWMEDICAIDPATIENTS { get; set; }
        public string SupplementOpenPractiseStatusExplain { get; set; }
        public string SupplementOpenPractiseStatusPRACTICELIMITATIONS { get; set; }
        public string SupplementOpenPractiseStatusGENDERLIMITATIONS { get; set; }
        public int? SupplementOpenPractiseStatusMINIMUMAGELIMITATIONS { get; set; }
        public int? SupplementOpenPractiseStatusMAXIMUMAGELIMITATIONS { get; set; }
        public string SupplementOpenPractiseStatusOtherLIMITATIONS { get; set; }
        #endregion

        #region Mid-Level Practitioner

        public string SupplementCareForPatients { get; set; }

        #region Mid-Level Practitioner1
        public string SupplementMidLevelPractitionerLastName1 { get; set; }
        public string SupplementMidLevelPractitionerFirstName1 { get; set; }
        public string SupplementMidLevelPractitionerMI1 { get; set; }
        public string SupplementMidLevelPractitionerType1 { get; set; }
        public string SupplementMidLevelPractitionerLicenseNumber1 { get; set; }
        public string SupplementMidLevelPractitionerState1 { get; set; }
        #endregion

        #region Mid-Level Practitioner2
        public string SupplementMidLevelPractitionerLastName2 { get; set; }
        public string SupplementMidLevelPractitionerFirstName2 { get; set; }
        public string SupplementMidLevelPractitionerMI2 { get; set; }
        public string SupplementMidLevelPractitionerType2 { get; set; }
        public string SupplementMidLevelPractitionerLicenseNumber2 { get; set; }
        public string SupplementMidLevelPractitionerState2 { get; set; }
        #endregion

        #region Mid-Level Practitioner3
        public string SupplementMidLevelPractitionerLastName3 { get; set; }
        public string SupplementMidLevelPractitionerFirstName3 { get; set; }
        public string SupplementMidLevelPractitionerMI3 { get; set; }
        public string SupplementMidLevelPractitionerType3 { get; set; }
        public string SupplementMidLevelPractitionerLicenseNumber3 { get; set; }
        public string SupplementMidLevelPractitionerState3 { get; set; }
        #endregion

        #region Mid-Level Practitioner4
        public string SupplementMidLevelPractitionerLastName4 { get; set; }
        public string SupplementMidLevelPractitionerFirstName4 { get; set; }
        public string SupplementMidLevelPractitionerMI4 { get; set; }
        public string SupplementMidLevelPractitionerType4 { get; set; }
        public string SupplementMidLevelPractitionerLicenseNumber4 { get; set; }
        public string SupplementMidLevelPractitionerState4 { get; set; }
        #endregion

        #region Mid-Level Practitioner5
        public string SupplementMidLevelPractitionerLastName5 { get; set; }
        public string SupplementMidLevelPractitionerFirstName5 { get; set; }
        public string SupplementMidLevelPractitionerMI5 { get; set; }
        public string SupplementMidLevelPractitionerType5 { get; set; }
        public string SupplementMidLevelPractitionerLicenseNumber5 { get; set; }
        public string SupplementMidLevelPractitionerState5 { get; set; }
        #endregion
        #endregion

        #region Languages
        public string SupplementNonEnglishLanguage1 { get; set; }
        public string SupplementNonEnglishLanguage2 { get; set; }
        public string SupplementNonEnglishLanguage3 { get; set; }
        public string SupplementNonEnglishLanguage4 { get; set; }
        public string SupplementNonEnglishLanguage5 { get; set; }

        public string InterpreterAvailable { get; set; }

        public string SupplementLanguagesInterpreted1 { get; set; }
        public string SupplementLanguagesInterpreted2 { get; set; }
        public string SupplementLanguagesInterpreted3 { get; set; }
        public string SupplementLanguagesInterpreted4 { get; set; }

        #endregion

        #region Accessibilities
        public string SupplementADAREQUIREMENTS { get; set; }
        public string SupplementHANDICAPPEDACCESSForBuilding { get; set; }
        public string SupplementHANDICAPPEDACCESSForRESTROOM { get; set; }
        public string SupplementHANDICAPPEDACCESSForPARKING { get; set; }
        public string SupplementOTHERSERVICESForDISABLED { get; set; }
        public string SupplementTextTELEPHONY { get; set; }
        public string SupplementAMERICANSIGNLANGUAGE { get; set; }
        public string SupplementMENTALPhysicalIMPAIRMENTSERVICES { get; set; }
        public string SupplementAccessibleByPublicTransport { get; set; }
        public string SupplementBus { get; set; }
        public string SupplementSubway { get; set; }
        public string SupplementRegionalTrain { get; set; }
        public string SupplementOtherHANDICAPPEDAccess { get; set; }
        public string SupplementOtherDisabilityServices { get; set; }
        public string SupplementOtherTransportationAccess { get; set; }
        #endregion

        #region Services
        public string SupplementLaboratoryServices { get; set; }
        public string SupplementLaboratoryServicesAccreditingProgram { get; set; }
        public string SupplementRadiologyServices { get; set; }
        public string SupplementRadiologyServicesXrayCertificationType { get; set; }
        public string SupplementEKGS { get; set; }
        public string SupplementAllergyInjections { get; set; }
        public string SupplementAllergySkinTesting { get; set; }
        public string SupplementRoutineOfficeGynecology { get; set; }
        public string SupplementDrawingBlood { get; set; }
        public string SupplementAgeImmunizations { get; set; }
        public string SupplementFlexibleSIGMOIDOSCOPY { get; set; }
        public string SupplementTYMPANOMETRYAUDIOMETRYSCREENING { get; set; }
        public string SupplementASTHMATreatment { get; set; }
        public string SupplementOSTEOPATHICManipulation { get; set; }
        public string SupplementIVHYDRATIONTREATMENT { get; set; }
        public string SupplementCARDIACSTRESSTEST { get; set; }
        public string SupplementPULMONARYFUNCTIONTESTING { get; set; }
        public string SupplementPhysicalTheraphy { get; set; }
        public string SupplementMinorLaseractions { get; set; }
        public string SupplementAnesthesiaAdministered { get; set; }
        public string SupplementClassCategory { get; set; }
        public string SupplementAdministerLastName { get; set; }
        public string SupplementAdministerFirstName { get; set; }
        public string SupplementPractiseType { get; set; }
        public string SupplementAdditionalOfficeProcedures { get; set; }
        #endregion

        #region Partners / Associates1
        public string SupplementPartnersLastName1 { get; set; }
        public string SupplementPartnersFirstName1 { get; set; }
        public string SupplementPartnersSpecialtyCode1 { get; set; }
        public string SupplementPartnersCoveringCollegue1 { get; set; }
        public string SupplementPartnersMI1 { get; set; }
        public string SupplementPartnersProviderType1 { get; set; }
        #endregion

        #region Partners / Associates2
        public string SupplementPartnersLastName2 { get; set; }
        public string SupplementPartnersFirstName2 { get; set; }
        public string SupplementPartnersSpecialtyCode2 { get; set; }
        public string SupplementPartnersCoveringCollegue2 { get; set; }
        public string SupplementPartnersMI2 { get; set; }
        public string SupplementPartnersProviderType2 { get; set; }
        #endregion

        #region Partners / Associates3
        public string SupplementPartnersLastName3 { get; set; }
        public string SupplementPartnersFirstName3 { get; set; }
        public string SupplementPartnersSpecialtyCode3 { get; set; }
        public string SupplementPartnersCoveringCollegue3 { get; set; }
        public string SupplementPartnersMI3 { get; set; }
        public string SupplementPartnersProviderType3 { get; set; }
        #endregion

        #region Partners / Associates4
        public string SupplementPartnersLastName4 { get; set; }
        public string SupplementPartnersFirstName4 { get; set; }
        public string SupplementPartnersSpecialtyCode4 { get; set; }
        public string SupplementPartnersCoveringCollegue4 { get; set; }
        public string SupplementPartnersMI4 { get; set; }
        public string SupplementPartnersProviderType4 { get; set; }
        #endregion

        #region CoveringCollegues1
        public string SupplementCoveringColleguesLastName1 { get; set; }
        public string SupplementCoveringColleguesFirstName1 { get; set; }
        public string SupplementCoveringColleguesSpecialtyCode1 { get; set; }
        public string SupplementCoveringColleguesMI1 { get; set; }
        public string SupplementCoveringColleguesProviderType1 { get; set; }
        #endregion

        #region CoveringCollegues2
        public string SupplementCoveringColleguesLastName2 { get; set; }
        public string SupplementCoveringColleguesFirstName2 { get; set; }
        public string SupplementCoveringColleguesSpecialtyCode2 { get; set; }
        public string SupplementCoveringColleguesMI2 { get; set; }
        public string SupplementCoveringColleguesProviderType2 { get; set; }
        #endregion

        #region CoveringCollegues3
        public string SupplementCoveringColleguesLastName3 { get; set; }
        public string SupplementCoveringColleguesFirstName3 { get; set; }
        public string SupplementCoveringColleguesSpecialtyCode3 { get; set; }
        public string SupplementCoveringColleguesMI3 { get; set; }
        public string SupplementCoveringColleguesProviderType3 { get; set; }
        #endregion

        #region Covering Colleagues4

        public string SupplementCoveringColleguesLastName4 { get; set; }
        public string SupplementCoveringColleguesFirstName4 { get; set; }
        public string SupplementCoveringColleguesSpecialtyCode4 { get; set; }
        public string SupplementCoveringColleguesMI4 { get; set; }
        public string SupplementCoveringColleguesProviderType4 { get; set; }

        #endregion

        #endregion
    }
}
