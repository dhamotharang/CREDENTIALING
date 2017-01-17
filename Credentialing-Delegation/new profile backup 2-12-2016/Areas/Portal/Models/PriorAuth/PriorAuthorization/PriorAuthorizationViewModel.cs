using PortalTemplate.Areas.Portal.Models.PriorAuth.Attachment;
using PortalTemplate.Areas.Portal.Models.PriorAuth.CPT;
using PortalTemplate.Areas.Portal.Models.PriorAuth.ICD;
using PortalTemplate.Areas.Portal.Models.PriorAuth.PriorAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.PriorAuthorization
{
    public class PriorAuthorizationViewModel
    {
        public PriorAuthorizationViewModel()
        {
            ServiceRequesteds = new List<ServiceRequestedViewModel>();
            ICDCodes = new List<ICDViewModel>();
            CPTCodes = new List<CPTViewModel>();
            Attachments = new List<AttachmentViewModel>();
        }
        public string SubscriberID { get; set; }
        public string TherapyType { get; set; }
        public int PriorAuthorizationID { get; set; }

        [Display(Name = "SERVICE REQUESTED")]   
        public string PlaceOfService { get; set; }

        [Display(Name="AUTHORIZATION TYPE")]       
        public string AuthorizationType { get; set; }

         [Display(Name = "MEMBER INFORMATION")]       
        public MemberViewModel Member { get; set; }

        [Display(Name="Request Date")]
        public DateTime? ReceivedDate { get; set; }
       
        public List<ServiceRequestedViewModel> ServiceRequesteds { get; set; }

        #region Provider
        [Display(Name = "REQUESTING PROVIDER")]
        public AuthorizationProviderViewModel RequestingProvider { get; set; }
        [Display(Name = "REFERRED TO/SERVICING PROVIDER")]
        public AuthorizationProviderViewModel ServicingProvider { get; set; }
        #endregion

        [Display(Name="Type")]
        public bool IsPCP { get; set; }

        [Display(Name = "HAS PCP APPROVED THIS REQUEST?")]
        public bool IsApprovedByPCP { get; set; }

         [Display(Name = "FACILITY")]
        public FacilityViewModel Facility { get; set; }

        [Display(Name="From Date")]
        public DateTime FromDate { get; set; }

        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }

        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        #region ICDCodes
        public List<ICDViewModel> ICDCodes { get; set; }
        #endregion

        #region CPTCodes
        public List<CPTViewModel> CPTCodes { get; set; }
        #endregion

        [Display(Name = "Provider Preferences")]
        public int UMServiceGroup { get; set; }

        #region Attachment
        public List<AttachmentViewModel> Attachments { get; set; }
        #endregion

        public string Note { get; set; }

        public bool IsAssigned { get; set; }

        public bool IsAuthCreated { get; set; }
    }
}