using PortalTemplate.Areas.Portal.Models.PriorAuth.Attachment;
using PortalTemplate.Areas.Portal.Models.PriorAuth.CPT;
using PortalTemplate.Areas.Portal.Models.PriorAuth.ICD;
using PortalTemplate.Areas.Portal.Models.PriorAuth.PriorAuthorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.View
{
    public class ViewPriorAuthorizationViewModel
    {
        public ViewPriorAuthorizationViewModel()
        {          
            Attachments = new List<AttachmentViewModel>();         
            ICDCodes = new List<ICDViewModel>();
            CPTCodes = new List<CPTViewModel>();           
        }

        public string SubscriberID { get; set; }

        public int AuthorizationID { get; set; }

        [Display(Name = "POS")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlaceOfService { get; set; }

        [Display(Name = "REQUEST")]
        [DisplayFormat(NullDisplayText = "-")]
        public string RequestType { get; set; }

        [Display(Name = "AUTHORIZATION")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AuthorizationType { get; set; }

        [Display(Name = "TYPE OF CARE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string TypeOfCare { get; set; }

        #region Dates

        [Display(Name = "RECEIVED")]
        public DateTime? ReceivedDate { get; set; }

        [Display(Name = "FROM DATE")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "TO")]
        public DateTime? ToDate { get; set; }

        #endregion

        #region Providers

        [Display(Name = "PCP")]
        public AuthorizationProviderViewModel PCP { get; set; }

        [Display(Name = "Req Provider")]
        public AuthorizationProviderViewModel RequestingProvider { get; set; }

        [Display(Name = "Svc Provider")]
        public AuthorizationProviderViewModel ServicingProvider { get; set; }

        #endregion

        #region Attachment
        public List<AttachmentViewModel> Attachments { get; set; }
        #endregion
           
        #region ICDCodes
        public List<ICDViewModel> ICDCodes { get; set; }
        #endregion

        #region CPTCodes
        public List<CPTViewModel> CPTCodes { get; set; }
        #endregion

        [Display(Name = "OP TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string OutPatientType { get; set; }

        public FacilityViewModel Facility { get; set; }

        [Display(Name = "TOS")]
        [DisplayFormat(NullDisplayText = "-")]
        public string TypeOfService { get; set; }

        [Display(Name = "UM SVC GRP")]
        [DisplayFormat(NullDisplayText = "-")]
        public string UMServiceGroup { get; set; }

        [Display(Name = "LEVEL OF CARE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LevelOfCare { get; set; }

        [Display(Name = "EXPECTED CHARGES")]
        [DisplayFormat(NullDisplayText = "-")]
        public decimal? ExpectedCharge { get; set; }

         [Display(Name = "ROOM TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string RoomType { get; set; }

        [Display(Name = "REF #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ReferenceID { get; set; }

        public string PlanAuthorizationNumber { get; set; }

        public int? AuthorizationParentID { get; set; }

        public string ParentType { get; set; }

        [Display(Name = "REVIEW TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ReviewType { get; set; }

        [Display(Name = "LEVEL / RATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LevelRate { get; set; }

        public ServiceRequestedViewModel ServiceRequested { get; set; }

        [Display(Name = "DAYS USED")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? DaysUsed { get; set; }

        [Display(Name = "REMAINING DAYS")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? RemainingDays { get; set; }

        public MemberViewModel Member { get; set; }

        //public bool IsPreAuth;

        [DisplayFormat(NullDisplayText = "-")]
        public string TOS { get; set; }    

        #region Pend for Reasons
        public string ReasonDescription { get; set; }
        public int ReasonID { get; set; }
        #endregion


        #region AuthorizationStatus

        [Display(Name = "CURRENT STATUS")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CurrentStatus { get; set; }

        [Display(Name = "CURRENT QUEUE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CurrentQueue { get; set; }

        [Display(Name = "ASSIGNED TO")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AssignedTo { get; set; }

        [Display(Name = "AUTHORIZATION STATUS")]
        public AuthorizationStatusViewModel AuthorizationStatus { get; set; }

        #endregion
        [Display(Name = "ENTRY TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EntryType { get; set; }
    }
}