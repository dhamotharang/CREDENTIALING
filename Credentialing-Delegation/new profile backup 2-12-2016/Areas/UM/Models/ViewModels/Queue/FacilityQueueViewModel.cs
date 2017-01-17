using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Queue
{
    public class FacilityQueueViewModel
    {
        public string QueueLabel { get; set; }

        public int? AuthorizationId { get; set; }

        [Display(Name = "ABV")]
        public string Abbrevation { get; set; }

        [Display(Name = "REF", ResourceType = typeof(App_LocalResources.Content))]
        public string ReferenceNumber { get; set; }

        [Display(Name = "MBRID", ResourceType = typeof(App_LocalResources.Content))]
        public string MemberID { get; set; }

        [Display(Name = "MBRNAME", ResourceType = typeof(App_LocalResources.Content))]
        public string MemberName { get; set; }

        [Display(Name = "EXP DOA")]
        public DateTime? ExpectedDOA { get; set; }

        [Display(Name = "DOC")]
        public DateTime? DateOfConversion { get; set; }

        [Display(Name = "REVIEW DT")]
        public DateTime? ReviewDate { get; set; }
         
        [Display(Name = "EXP DC")]
        public DateTime? ExpectedDcDate { get; set; }

        [Display(Name = "PROVIDER", ResourceType = typeof(App_LocalResources.Content))]
        public string ProviderName { get; set; }

        [Display(Name = "FACILITY", ResourceType = typeof(App_LocalResources.Content))]
        public string FacilityName { get; set; }

        [Display(Name = "SVC PROVIDER")]
        public string SVCAttProviderName { get; set; }

        [Display(Name = "REQUEST", ResourceType = typeof(App_LocalResources.Content))]
        public string RequestType { get; set; }
        //public string Pos { get; set; }

        [Display(Name = "AUTH", ResourceType = typeof(App_LocalResources.Content))]
        public string AuthorizationType { get; set; }

        [Display(Name = "TOC", ResourceType = typeof(App_LocalResources.Content))]
        public string TypeOfCare { get; set; }

         [Display(Name = "REVIEW")]
        public DateTime Review { get; set; }

         [Display(Name = "ACT LOS")]
         public int ActualLOS { get; set; }

         [Display(Name = "DAYS REQ")]
         public int DaysRequested { get; set; }

         [Display(Name = "TOTAL AUTHED")]
         public int TotalAuthorized { get; set; }

         [Display(Name = "TOTAL DENIED")]
         public int TotalDenied { get; set; }

        [Display(Name = "STATUS", ResourceType = typeof(App_LocalResources.Content))]
        public string AuthStatus { get; set; } //Will be used to show the authorization status to user i.e.. Status for view purpose
      //  public string AuthorizationQueue { get; set; } //Will be used to take business decision if required. i.e.. status for flow purpose

        [Display(Name = "POS", ResourceType = typeof(App_LocalResources.Content))]
        public string PosAbb { get; set; }

        [Display(Name = "DX", ResourceType = typeof(App_LocalResources.Content))]
        public string PrimaryDx { get; set; }

        [Display(Name = "ASSIGNED TO")]
        public string Assigned { get; set; }

        [Display(Name = "ENTRY", ResourceType = typeof(App_LocalResources.Content))]
        public string Entry { get; set; }

        [Display(Name = "AUTH NOTE")]
        public string AuthNote { get; set; }
      
        public string RowColor { get; set; }

        public string RequestColor { get; set; }

        public string ReviewDateColor { get; set; }


    }
}