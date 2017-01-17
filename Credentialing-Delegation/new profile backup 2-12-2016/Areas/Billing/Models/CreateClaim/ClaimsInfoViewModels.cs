using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim
{
    public class ClaimsInfoViewModels
    {
        [DisplayName("From")]
        public DateTime DateOfServiceFrom { get; set; }

        [DisplayName("To")]
        public DateTime DateOfServiceTo { get; set; }

        [DisplayName("Claim Type")]
        public string ClaimType { get; set; }

        [DisplayName(" Date of Current Illness/Injury/Pregnancy")]
        public DateTime? DateOfCurrentIllness { get; set; }

        [DisplayName("Qual")]
        public string DateOfCurrentIllnessQual { get; set; }

        [DisplayName("Other Date")]
        public DateTime? OtherDate { get; set; }

        [DisplayName("Qual")]
        public string OtherDateQual { get; set; }

        [DisplayName("Place of Service")]
        public string PlaceOfService { get; set; }

        [DisplayName("Prior Authorization No")]
        public string PriorAuthorizationNo { get; set; }

        [DisplayName("Additional Claim Info(By NUCC)")]
        public string AdditionalClaimsInfo { get; set; }

        [DisplayName("Resubmission Code")]
        public string ResubmissionCode { get; set; }

        [DisplayName("Original Reference No")]
        public string OriginalReferenceNo { get; set; }

        [DisplayName("Outside Lab?")]
        public string OutSideLab { get; set; }

        [DisplayName("Charges")]
        public double Charges { get; set; }

        [DisplayName("From")]
        public DateTime? PatientUnableToWorkFrom { get; set; }

        [DisplayName("To")]
        public DateTime? PatientUnableToWorkTo { get; set; }

        [DisplayName("From")]
        public DateTime? CurrentServiceFrom { get; set; }

        [DisplayName("To")]
        public DateTime? CurrentServiceTo { get; set; }

        [DisplayName("Qual")]
        public string OtherClaimQual { get; set; }

        [DisplayName("Other Claim ID")]
        public string OtherClaimId { get; set; }

        [DisplayName("A. Employment?(Current Or Previous)")]
        public string Employment { get; set; }

        [DisplayName("B. Auto Accident?")]
        public string AutoAccident { get; set; }

        [DisplayName("C. Other Accident?")]
        public string OtherAccident { get; set; }

        [DisplayName("D. Claim Codes(Designated By NUCC)")]
        public string ClaimCodes { get; set; }

        //[DisplayName("QUAL")]
        public string ICDIndicator { get; set; }

        
        public string ClaimsNatureOfIllness1 { get; set; }

        
        public string ClaimsNatureOfIllness2 { get; set; }

       
        public string ClaimsNatureOfIllness3 { get; set; }

        public string ClaimsNatureOfIllness4 { get; set; }

      
        public string ClaimsNatureOfIllness5 { get; set; }
        
       
        public string ClaimsNatureOfIllness6 { get; set; }

       
        public string ClaimsNatureOfIllness7 { get; set; }

        
        public string ClaimsNatureOfIllness8 { get; set; }

        
        public string ClaimsNatureOfIllness9 { get; set; }

       
        public string ClaimsNatureOfIllness10 { get; set; }

        
        public string ClaimsNatureOfIllness11{ get; set; }

        
        public string ClaimsNatureOfIllness12{ get; set; }

       
        public List<ServiceLineViewModels> ServiceLines { get; set; }

        [DisplayName("Total Charges($)")]
        public double TotalCharges { get; set; }

        [DisplayName("Amount Paid ($)")]
        public double AmountPaid { get; set; }
    }
}