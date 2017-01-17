using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class LengthOfStayViewModel
    {
        public LengthOfStayViewModel()
        {
            NEGFeeDetail = new NEGFeeDetailViewModel();
        }
        public int LengthOfStayID { get; set; }

        [Display(Name = "REQ LOS")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? RequiredLOS { get; set; }

        [Display(Name = "APP LOS")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? AuthorizedLOS { get; set; }

        [Display(Name = "APP LOS")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? ApprovedLos { get; set; }

        [Display(Name = "TOTAL APP")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? TotalApprovedLos { get; set; }

        [Display(Name = "TOTAL DENIED")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? TotalDeniedLos { get; set; }

        public int? ActualLOS { get; set; }

        [Display(Name = "DENIED")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? Denied { get; set; }

        [Display(Name = "AUTH TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AuthType { get; set; }

        [Display(Name = "LOS STATUS")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LOSStatus { get; set; }

        [Display(Name = "AUTH FROM")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? AuthFrom { get; set; }

        [Display(Name = "AUTH TO")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? AuthTo { get; set; }

        [Display(Name = "EXP DOS/DOA")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? ExpectedDos { get; set; }

        [Display(Name = "NEXT REVIEW DT")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? ReviewDT { get; set; }

        public string TypeOfService { get; set; }

        public ICollection<DenialLOSViewModel> DeniedLOSs { get; set; }

        [Display(Name = "ROOM TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string RoomType { get; set; }

        [Display(Name = "REVIEW TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ReviewType { get; set; }

        [Display(Name = "REVIEW")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Review { get; set; }

        public Decimal Charge { get; set; }

        [Display(Name = "REC DT")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? ReceivedDate { get; set; }

        [Display(Name = "NEXT REVIEW")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? NextReviewDate { get; set; }

        [Display(Name = "EXP DC")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? ExpectedDate { get; set; }

        [Display(Name = "AUTH THROUGH")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? AuthThrough { get; set; }

        public int? NEGFeeDetailID { get; set; }

        public NEGFeeDetailViewModel NEGFeeDetail { get; set; }
    }
}