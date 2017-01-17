using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim
{
    public class MemberInfoViewModels
    {

        [DisplayName("PAYER NAME")]
        public string PayerName { get; set; }

        [DisplayName("PAYER ID")]
        public string PayerID { get; set; }

        [DisplayName("1ST ADDRESS")]
        public string PayerFirstAddress { get; set; }

        [DisplayName("2ND ADDRESS")]
        public string PayerSecondAddress { get; set; }

        [DisplayName("CITY")]
        public string PayerCity { get; set; }

        [DisplayName("STATE")]
        public string PayerState { get; set; }

        [DisplayName("PAYER ZIP")]
        public string PayerZip { get; set; }

        public string ClaimFilingIndicatorCode { get; set; }

        [DisplayName("INSURED'S ID NUMBER")]
        public string InsuredIdNumber { get; set; }

        [DisplayName("LAST")]
        public string PatientLastOrOrganizationName { get; set; }

        [DisplayName("MIDDLE")]
        public string PatientMiddleName { get; set; }

        [DisplayName("FIRST")]
        public string PatientFirstName { get; set; }

        [DisplayName("PATIENT BIRTH DATE")]
        public DateTime PatientBirthDate { get; set; }

        [DisplayName("SEX")]
        public string PatientGenders { get; set; }

        [DisplayName("LAST")]
        public string SubscriberLastOrOrganizationName { get; set; }

        [DisplayName("MIDDLE")]
        public string SubscriberMiddleName { get; set; }

        [DisplayName("FIRST")]
        public string SubscriberFirstName { get; set; }

        [DisplayName("1ST ADDRESS")]
        public string PatientFirstAddress { get; set; }

        [DisplayName("2ND ADDRESS")]
        public string PatientSecondAddress { get; set; }

        [DisplayName("CITY")]
        public string PatientCity { get; set; }

        [DisplayName("STATE")]
        public string PatientState { get; set; }

        [DisplayName("ZIP CODE")]
        public string PatientZip { get; set; }

        [DisplayName("TELEPHONE")]
        public string PatientPhoneNo { get; set; }

        [DisplayName("PATIENT RELETION TO INSUERD")]
        public string IndividualRelationshipCode { get; set; }

        [DisplayName("1ST ADDRESS")]
        public string SubscriberFirstAddress { get; set; }

        [DisplayName("2ND ADDRESS")]
        public string SubscriberSecondAddress { get; set; }

        [DisplayName("CITY")]
        public string SubscriberCity { get; set; }

        [DisplayName("STATE")]
        public string SubscriberState { get; set; }

        [DisplayName("ZIP CODE")]
        public string SubscriberZip { get; set; }

        [DisplayName("TELEPHONE")]
        public string SubscriberPhoneNo { get; set; }

        [DisplayName("LAST")]
        public string OtherInsuredLastName { get; set; }

        [DisplayName("MIDDLE")]
        public string OtherInsuredMiddleName { get; set; }

        [DisplayName("FIRST")]
        public string OtherInsuredFirstName { get; set; }

        [DisplayName("OTHER INSURED'S POLICY OR GROUP NUMBER")]
        public string OtherInsuredPolicyNo { get; set; }

        [DisplayName("a. RESERVED FOR NUCC USE:")]
        public string OtherInsuredNUCCUse01 { get; set; }

        [DisplayName("b. RESERVED FOR NUCC USE:")]
        public string OtherInsuredNUCCUse02 { get; set; }

        [DisplayName("INSURANCE PLAN NAME OR PROGRAM NAME")]
        public string OtherInsuredPlanName { get; set; }

        [DisplayName("a. INSURED'S POLICY GROUP OR FECA NUMBER")]
        public string SubscriberGroup_PolicyNo { get; set; }

        [DisplayName("b. INSURED'S BIRTH DATE")]
        public DateTime SubscriberBirthDate { get; set; }

        [DisplayName("SEX")]
        public string SubscriberGenders { get; set; }

        [DisplayName("c. INSURANCE PLAN NAME OR PROGRAM NAME")]
        public string SubscriberGroup_PlanName { get; set; }

        [DisplayName("d. IS THERE ANOTHER HEALTH BENEFIT PLAN?")]
        public string HealthBenefitPlan { get; set; }

        [DisplayName("PATIENT’S OR AUTHORIZED PERSON’S SIGNATURE")]
        public string SupplierSignatureIndicator { get; set; }

        [DisplayName("DATE")]
        public DateTime PatientOrAuthorizedSignatureDate { get; set; }

        [DisplayName("INSURED’S OR AUTHORIZED PERSON’S SIGNATURE ")]
        public string InsuredSignatureIndicator { get; set; }

        [DisplayName("QUAL")]
        public string ReferringProviderQual { get; set; }

        [DisplayName("LAST")]
        public string ReferringProviderLastName { get; set; }

        [DisplayName("MIDDLE")]
        public string ReferringProviderMiddleName { get; set; }

        [DisplayName("FIRST")]
        public string ReferringProviderFirstName { get; set; }

        [DisplayName("QUAL")]
        public string PhysicianQualifier { get; set; }

        [DisplayName("DESCRIPTION")]
        public string PhysicianDescription { get; set; }

        [DisplayName("NPI")]
        public string ReferringProviderIdentifier { get; set; }

        [DisplayName("FEDERAL TAX NUMBER")]
        public string BillingProviderEmployeeID { get; set; }

        public string BillingProviderTaxIDQual { get; set; }

        [DisplayName("PATIENT’S ACCOUNT NUMBER")]
        public string PatientAccountNumber { get; set; }

        [DisplayName("ACCEPT ASSIGNMENT?")]
        public string AcceptanceAssignment { get; set; }

        [DisplayName("30. RESERVED FOR NUCC USE")]
        public string OtherInsuredNUCCUse03 { get; set; }

        [DisplayName("DATE OF INTIAL TREATMENT")]
        public DateTime InitialTreatmentDate { get; set; }

        [DisplayName("LATEST VISIT OR CONSULTATION DATE")]
        public DateTime LastSeenDate { get; set; }

        [DisplayName("LAST")]
        public string SupervisingProviderLastName { get; set; }

        [DisplayName("MIDDLE")]
        public string SupervisingProviderMiddleName { get; set; }

        [DisplayName("FIRST")]
        public string SupervisingProviderFirstName { get; set; }

        [DisplayName("NPI")]
        public string SupervisingProviderIdentifier1 { get; set; }

        [DisplayName("ID")]
        public string SupervisingProviderIdentifier2 { get; set; }

        [DisplayName("CLIA")]
        public string ReferringCLIA_Number { get; set; }

        [DisplayName("ACCIDENT DATE")]
        public DateTime AccidentDate { get; set; }

        [DisplayName("MAMMOGRAPHY CERTIFICATE")]
        public string MammographyCertificate { get; set; }

        [DisplayName("FACILITY NAME")]
        public string FacilityName { get; set; }

        [DisplayName("ADDRESS1")]
        public string FacilityAddress1 { get; set; }

        [DisplayName("ADDRESS2")]
        public string FacilityAddress2 { get; set; }

        [DisplayName("CITY")]
        public string FacilityCity { get; set; }

        [DisplayName("STATE")]
        public string FacilityState { get; set; }

        [DisplayName("ZIP CODE")]
        public string FacilityZip { get; set; }

        [DisplayName("NPI")]
        public string FacilityIdentifier1 { get; set; }

        [DisplayName("ID")]
        public string FacilityIdentifier2 { get; set; }

        [DisplayName("LAST")]
        public string BillingProviderLastOrOrganizationName { get; set; }

        [DisplayName("MIDDLE")]
        public string BillingProviderMiddleName { get; set; }

        [DisplayName("FIRST")]
        public string BillingProviderFirstName { get; set; }

        [DisplayName("1ST ADDRESS")]
        public string BillingProviderFirstAddress { get; set; }

        [DisplayName("2ND ADDRESS")]
        public string BillingProviderSecondAddress { get; set; }

        [DisplayName("CITY")]
        public string BillingProviderCity { get; set; }

        [DisplayName("STATE")]
        public string BillingProviderState { get; set; }

        [DisplayName("ZIP CODE")]
        public string BillingProviderZip { get; set; }

        [DisplayName("TELEPHONE")]
        public string BillingProviderPhoneNo { get; set; }

        [DisplayName("TAXONOMY")]
        public string BillingProviderTaxonomy { get; set; }

        [DisplayName("LAST")]
        public string RenderingProviderLastOrOrganizationName { get; set; }

        [DisplayName("MIDDLE")]
        public string RenderingProviderMiddleName { get; set; }

        [DisplayName("FIRST")]
        public string RenderingProviderFirstName { get; set; }

        [DisplayName("RENDERING PROVIDER SPECIALITY")]
        public string RenderingProviderTaxonomy { get; set; }

        [DisplayName("RENDERING PROVIDER NPI")]
        public string RenderingProviderNPI { get; set; }

        [DisplayName("RENDERING PROVIDER PIN")]
        public string RenderingProviderPin { get; set; }

        [DisplayName("BILLING/GROUP NPI")]
        public string BillingGroupNPI { get; set; }

        [DisplayName("BILLING/GROUP NUMBER")]
        public string BillingGroupNumber { get; set; }

        [DisplayName("QUAL")]
        public string BillingGroupIdQual { get; set; }
    }
}