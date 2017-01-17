using PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview;
using PortalTemplate.Areas.UM.Models.ViewModels.CPT;
using PortalTemplate.Areas.UM.Models.ViewModels.ICD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview
{
    public class AuthPreviewViewModal
    {

        //[Display(Name = "Submitted By")]
        //public string FullNameCurrentUser { get; set; }

        [Display(Name = "AUTH DETAILS")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ReferenceNumber { get; set; }

        [Display(Name = "MEMBER INFORMATION")]
        [DisplayFormat(NullDisplayText = "-")]
        public MemberInformationPreviewViewModel Member { get; set; }

        [Display(Name = "POS: ")]
        public string PlaceOfService { get; set; }

        [Display(Name = "Req: ")]
        public string RequestType { get; set; }

        [Display(Name = "Auth: ")]
        public string AuthorizationType { get; set; }

        [Display(Name = "Type Of Care: ", ShortName = "TOC: ")]
        public string TypeOfCare { get; set; }

        [Display(Name = "OutPatient Type: ", ShortName = "OP Type: ")]
        public string OutPatientType { get; set; }

        #region Dates

        [Display(Name = "RECD: ")]
        public DateTime? ReceivedDate { get; set; }

        [Display(Name = "From: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "To: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? ToDate { get; set; }

        [Display(Name = "Expected DOS/DOA: ", ShortName = "Expected DOS: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? ExpectedDOS { get; set; }

        [Display(Name = "Next Rev Dt: ")]
        public DateTime? NextReviewDate { get; set; }

        [Display(Name = "Review: ")]
        public string ReviewType { get; set; }

        #endregion

        #region DRG and MDC

        [Display(Name = "DRG: ", ShortName = "RUG: ")]
        public string DRGCode { get; set; }

        [Display(Name = "DRG Desc: ", ShortName = "RUG Desc: ")]
        public string DRGDescription { get; set; }


        [Display(Name = "MDC Code: ")]
        public string MDCCode { get; set; }

        [Display(Name = "MDC Desc: ")]
        public string MDCDescription { get; set; }
        #endregion

        #region Providers

       
        public ProviderPreviewViewModel PCP { get; set; }

        [Display(Name = "Requesting Provider")]
        public ProviderPreviewViewModel RequestingProvider { get; set; }

        [Display(Name = "Servicing Provider")]
        //[DisplayFormat(NullDisplayText = "-")]
        public ProviderPreviewViewModel ServicingProvider { get; set; }

        [Display(Name = "Attending Provider")]
        public ProviderPreviewViewModel AttendingProvider { get; set; }

        [Display(Name = "Admitting Provider")]
        public ProviderPreviewViewModel AdmittingProvider { get; set; }

        [Display(Name = "Surgeon")]
        public ProviderPreviewViewModel SurgeonProvider { get; set; }

        //[Display(Name = "Provider")]
        //public ProviderPreviewViewModel CoManagementProvider { get; set; }

        #endregion

        #region Facility

        [Display(Name = "Facility/Agency/Supplier", ShortName = "Facility")]
        public FacilityPreviewViewModel Facility { get; set; }

        #endregion

        #region AdmissionDischarge

        [Display(Name = "Admission", ShortName = "SVC")]
         public AdmissionPreviewViewModel Admission { get; set; }

        [Display(Name = "DC Dates", ShortName = "SVC To Dates")]
        public DischargePreviewViewModel Discharge { get; set; }

        #endregion

        #region LOS

        [Display(Name = "Actual LOS: ")]
        public int? TotalActualLOS { get; set; }

        [Display(Name = "Req LOS: ")]
        public int? TotalRequestedLOS { get; set; }

        public int? TotalDenialLOS { get; set; }

        [Display(Name = "CO-MANAGEMENT OBTAINED: ")]
        public bool IsComanagementObtained { get; set; }

        #endregion

        #region ICDCodes

         [Display(Name = "PRIMARY ICD DETAILS")]
        public List<ICDPreviewViewModel> ICDCodes { get; set; }
        #endregion

         #region CPTCodes
         [Display(Name = "CPT/HCPCS CODE")]
        public List<CPTPreviewViewModel> CPTCodes { get; set; }
        #endregion

         public string CPTPreviewPage { get; set; }

         public AuthPreviewConstraintViewModel Constraint { get; set; }

         public AuthPreviewContentViewModel Content { get; set; }

         public AuthPreviewViewModal()
         {
             Member = new MemberInformationPreviewViewModel();
           //  Member.ReferenceNumber = ReferenceNumber;
             //RequestingProvider = new ProviderPreviewViewModel();
             //ServicingProvider = new ProviderPreviewViewModel();
             //AttendingProvider = new ProviderPreviewViewModel();
             //AdmittingProvider = new ProviderPreviewViewModel();
             //SurgeonProvider = new ProviderPreviewViewModel();
             //Facility = new FacilityPreviewViewModel();
             Admission = new AdmissionPreviewViewModel();
             Discharge = new DischargePreviewViewModel();
             ICDCodes = new List<ICDPreviewViewModel>();
             CPTCodes = new List<CPTPreviewViewModel>();
             Constraint = new AuthPreviewConstraintViewModel();
             Content = new AuthPreviewContentViewModel();

         }
    }
}