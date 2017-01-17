using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.Queue
{
    public class QueueViewModel
    {
        //Authorization Info

        public int? AuthorizationId { get; set; }

        public bool HasSelectAuthorization { get; set; }

        [Display(Name = "ABV")]
        public string Abbrevation { get; set; }

        [Display(Name = "REF")]
        public string ReferenceNumber { get; set; }

        [Display(Name = "MBRID")]
        public string MemberID { get; set; }

        [Display(Name = "MBRNAME")]
        public string MemberName { get; set; }

        [Display(Name = "FROM")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "TO")]
        public DateTime? ToDate { get; set; }


        [Display(Name = "PRV ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ProviderID { get; set; }

        [Display(Name = "PROVIDER")]
        public string ProviderName { get; set; }

        [Display(Name = "FACILITY")]
        public string FacilityName { get; set; }

        [Display(Name = "SVCPROVIDER")]
        public string SVCAttProviderName { get; set; }

        [Display(Name = "REQUEST")]
        public string RequestType { get; set; }
        //public string Pos { get; set; }

        [Display(Name = "AUTH")]
        public string AuthorizationType { get; set; }

        [Display(Name = "TOC")]
        public string TypeOfCare { get; set; }

        [Display(Name = "DURATION")]
        public string DurationLeft { get; set; }

        [Display(Name = "DOS")]
        public string ExpectedDOS { get; set; }

        [Display(Name = "UNITS")]
        public int? RequestedUnit { get; set; }

        [Display(Name = "STATUS")]
        public string AuthStatus { get; set; } //Will be used to show the authorization status to user i.e.. Status for view purpose
        public string AuthorizationQueue { get; set; } //Will be used to take business decision if required. i.e.. status for flow purpose

        [Display(Name = "POS")]
        public string PosAbb { get; set; }

        [Display(Name = "DX")]
        public string PrimaryDx { get; set; }

        [Display(Name = "ASSIGNED")]
        public string Assigned { get; set; }

        [Display(Name = "ENTRY")]
        public string Entry { get; set; }

        [Display(Name = "AUTH NOTE")]
        public string AuthNote { get; set; }


        //Model information to control Flow
        // public string Action { get; set; }
        //Info to Store color Logic 
        public string RowColor { get; set; }
        public string RequestColor { get; set; }

    }
}