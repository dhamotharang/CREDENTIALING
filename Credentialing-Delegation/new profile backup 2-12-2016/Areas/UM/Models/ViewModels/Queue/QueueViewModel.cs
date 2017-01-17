using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.UM.Models.ViewModels.Note;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Queue
{
    public class QueueViewModel
    {
        //Authorization Info

        public int? AuthorizationId { get; set; }

        public bool HasSelectAuthorization { get; set; }

        [Display(Name = "ABV")]
        public string Abbrevation { get; set; }

        [Display(Name = "REF", ResourceType = typeof(App_LocalResources.Content))]
        public string ReferenceNumber { get; set; }

        [Display(Name = "MBRID", ResourceType = typeof(App_LocalResources.Content))]
        public string MemberID { get; set; }

        [Display(Name = "MBRNAME", ResourceType = typeof(App_LocalResources.Content))]
        public string MemberName { get; set; }

        [Display(Name = "FROM")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "TO")]
        public DateTime? ToDate { get; set; }


        //[Display(Name = "PRV ID")]
        //[DisplayFormat( NullDisplayText = "-")]
        //public string ProviderID { get; set; }

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

        [Display(Name = "DURATION", ResourceType = typeof(App_LocalResources.Content))]
        public string DurationLeft { get; set; }

        [Display(Name = "DOS", ResourceType = typeof(App_LocalResources.Content))]
        public string ExpectedDOS { get; set; }

        [Display(Name = "UNITS", ResourceType = typeof(App_LocalResources.Content))]
        public int? RequestedUnit { get; set; }

        [Display(Name = "STATUS", ResourceType = typeof(App_LocalResources.Content))]
        public string AuthStatus { get; set; } //Will be used to show the authorization status to user i.e.. Status for view purpose
        public string AuthorizationQueue { get; set; } //Will be used to take business decision if required. i.e.. status for flow purpose

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

        
        public string UserRole { get; set; }

        public string UserName { get; set; }

        public string QueueName { get; set; }

        public string UserId { get; set; }

        //Model information to control Flow
       // public string Action { get; set; }
        //Info to Store color Logic 
        public string RowColor { get; set; }
        public string RequestColor { get; set; }
    }
}