using PortalTemplate.Areas.UM.Models.ViewModels.Attachment;
using PortalTemplate.Areas.UM.Models.ViewModels.Contact;
using PortalTemplate.Areas.UM.Models.ViewModels.CPT;
using PortalTemplate.Areas.UM.Models.ViewModels.ICD;
using PortalTemplate.Areas.UM.Models.ViewModels.Letter;
using PortalTemplate.Areas.UM.Models.ViewModels.Note;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class AuthorizationViewModel
    {
        public AuthorizationViewModel()
        {
            Attachments = new List<AttachmentViewModel>();
            Contacts = new List<AuthorizationContactViewModel>();
            Notes = new List<NoteViewModel>();
            Letters = new List<LetterViewModel>();
            ICDCodes = new List<ICDViewModel>();
            CPTCodes = new List<CPTViewModel>();
            ODAGs = new List<ODAGViewModel>();
            LOSs = new List<LengthOfStayViewModel>();
            //RequestingProvider = new AuthorizationProviderViewModel();
            //ServicingProvider = new AuthorizationProviderViewModel();
            //AdmittingProvider = new AuthorizationProviderViewModel();
            //AttendingProvider = new AuthorizationProviderViewModel();
            //SurgeonProvider = new AuthorizationProviderViewModel();
        }

        public string SubscriberID { get; set; }

        public int? AuthorizationID { get; set; }

        [Display(Name = "POS")]
        public string PlaceOfService { get; set; }

        [Display(Name = "Request")]
        public string RequestType { get; set; }

        [Display(Name = "Authorization Type")]
        public string AuthorizationType { get; set; }

        [Display(Name = "Type of Care", ShortName = "TOC")]
        public string TypeOfCare { get; set; }

        #region Dates

        [Display(Name = "Received")]
        public DateTime? ReceivedDate { get; set; }

        [Display(Name = "From Date", ShortName = "From")]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "To Date", ShortName = "TO")]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ToDate { get; set; }

        [Display(Name = "Expected DOS/DOA", ShortName = "Expected DOS")]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ExpectedDOS { get; set; }

        [Display(Name = "NEXT REVIEW DATE")]
        public DateTime? NextReviewDate { get; set; }

        #endregion
        #region Label for Auth Summary
        [Display(Name = "EXP DOS/DOA")]
        public DateTime? ExpectedDOSLabelForSummary { get; set; }

        [Display(Name = "EXP DC DT")]
        public DateTime? ExpectedDCLabelForSummary { get; set; }
        [Display(Name = "FROM DATE")]
        public DateTime? AdmissionFromDateLabel { get; set; }
        [Display(Name = "TO DATE")]
        public DateTime? DischargeToDateLabel { get; set; }
        [Display(Name = "SVC DATE")]
        public DateTime? ExpectedDOSLabelForSummaryPOS22 { get; set; }
     
             [Display(Name = "APPROVED DAYS")]
        public DateTime? ApprovedDaysLabel{ get; set; }
        [Display(Name = "SVC TO DATE")]
        public DateTime? ExpectedDCLabelForSummaryPOS22 { get; set; }

        [Display(Name = "OP TYPE")]
        public string OutPatientTypeLabel { get; set; }
        

        #endregion
        #region Providers

        [Display(Name = "PCP")]
        public AuthorizationProviderViewModel PCP { get; set; }

        [Display(Name = "Req Provider")]
        public AuthorizationProviderViewModel RequestingProvider { get; set; }

        [Display(Name = "Svc Provider")]
        [DisplayFormat(NullDisplayText = "-")]
        public AuthorizationProviderViewModel ServicingProvider { get; set; }

        [Display(Name = "Att Provider")]
        [DisplayFormat(NullDisplayText = "-")]
        public AuthorizationProviderViewModel AttendingProvider { get; set; }

        [Display(Name = "Adm Provider")]
        public AuthorizationProviderViewModel AdmittingProvider { get; set; }

        [Display(Name = "Surgeon")]
        public AuthorizationProviderViewModel SurgeonProvider { get; set; }

        [Display(Name = "Provider")]
        public AuthorizationProviderViewModel CoManagementProvider { get; set; }

        #endregion

        #region DRG and MDC
        public int DRGCodeID { get; set; }

        [Display(Name = "DRG", ShortName = "RUG")]
        public string DRGCode { get; set; }

        [Display(Name = "DRG Desc", ShortName = "RUG Desc")]
        public string DRGDescription { get; set; }

        public int MDCCodeID { get; set; }

        [Display(Name = "MDC Code")]
        public string MDCCode { get; set; }

        [Display(Name = "MDC Classification Desc")]
        public string MDCDescription { get; set; }
        #endregion

        #region Attachment
        public List<AttachmentViewModel> Attachments { get; set; }
        #endregion

        #region Contacts
        public List<AuthorizationContactViewModel> Contacts { get; set; }
        #endregion

        #region Notes
        public List<NoteViewModel> Notes { get; set; }
        #endregion

        #region Letters
        public List<LetterViewModel> Letters { get; set; }
        #endregion

        #region ICDCodes
        [Display(Name = "ICD Version")]
        public string ICDVersion { get; set; }

        public List<ICDViewModel> ICDCodes { get; set; }
        #endregion

        #region CPTCodes
        public List<CPTViewModel> CPTCodes { get; set; }
        #endregion

        #region ODAG
        public List<ODAGViewModel> ODAGs { get; set; }
        #endregion

        #region Admission

        [Display(Name = "ADMISSION DATE", ShortName = "SVC DATE")]
        public AdmissionViewModel Admission { get; set; }

        #endregion

        #region Discharge

        [Display(Name = "DISCHARGE DATE", ShortName = "SVC TO DATE")]
        public DischargeViewModel Discharge { get; set; }

        #endregion

        #region LOS

        [Display(Name = "LOS Details")]
        public List<LengthOfStayViewModel> LOSs {get; set;}

        [Display(Name = "Actual LOS")]
        public int? TotalActualLOS { get; set; }

        [Display(Name = "Req LOS")]
        public int? TotalRequestedLOS { get; set; }

        public int? TotalDenialLOS { get; set; }

        [Display(Name = "CO-MANAGEMENT OBTAINED")]
        public bool IsComanagementObtained { get; set; }

        #endregion

        #region Pend for Reasons

        public string ReasonDescription { get; set; }

        public int ReasonID { get; set; }

        #endregion

        [Display(Name = "OutPatient Type", ShortName = "OP Type")]
        [DisplayFormat(NullDisplayText = "-")]
        public string OutPatientType { get; set; }

        [Display(Name = "FACILITY/AGENCY/SUPPLIER", ShortName = "Facility")]
        public FacilityViewModel Facility { get; set; }

        [Display(Name = "Type of Service", ShortName = "TOS")]
        public string TypeOfService { get; set; }

        [Display(Name = "UM SVC GRP")]
        [DisplayFormat(NullDisplayText = "-")]
        public string UMServiceGroup { get; set; }

        [Display(Name = "Level of Care")]
        public string LevelOfCare { get; set; }

        [Display(Name = "Expected Charges")]
        public decimal? ExpectedCharge { get; set; }

        [Display(Name = "Room Type")]
        public string RoomType { get; set; }

        public string ReferenceNumber { get; set; }

        public string PlanAuthorizationNumber { get; set; }

        public int? AuthorizationParentID { get; set; }

        public int ParentType { get; set; }

        [Display(Name = "Review")]
        public string ReviewType { get; set; }

        [Display(Name = "LEVEL/RATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LevelRate { get; set; }

        public ServiceRequestedViewModel ServiceRequested { get; set; }

        [Display(Name = "Days Used/Benefit")]
        public int? DaysUsed { get; set; }

        [Display(Name = "Remaining Days")]
        public int? RemainingDays { get; set; }

        public MemberViewModel Member { get; set; }

        public bool IsPreAuth;

        #region AuthorizationStatus

        public string CurrentStatus { get; set; }

        public string CurrentQueue { get; set; }


        public string AssignedTo { get; set; }
    
        #endregion

        public string EntryType { get; set; }

        public AuthorizationStatusViewModel AuthorizationStatus { get; set; }
    }
}