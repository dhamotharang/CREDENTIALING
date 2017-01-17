using PortalTemplate.Areas.UM.Models.ViewModels.Attachment;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using PortalTemplate.Areas.UM.Models.ViewModels.CPT;
using PortalTemplate.Areas.UM.Models.ViewModels.ICD;
using PortalTemplate.Areas.UM.Models.ViewModels.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Portal
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

        public int PriorAuthorizationID { get; set; }

        [Display(Name="AUTHORIZATION TYPE")]       
        public string AuthorizationType { get; set; }

        public MemberViewModel Member { get; set; }

        [Display(Name="Request Date")]
        public DateTime? ReceivedDate { get; set; }
       
        public List<ServiceRequestedViewModel> ServiceRequesteds { get; set; }

        #region Provider
        public AuthorizationProviderViewModel RequestingProvider { get; set; }

        public AuthorizationProviderViewModel ServicingProvider { get; set; }
        #endregion

        [Display(Name="Type")]
        public bool IsPCP { get; set; }

        public bool IsApprovedByPCP { get; set; }

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