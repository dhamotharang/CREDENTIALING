using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MemberMedicareViewModel
    {
        [Display(Name = "EventMedicare", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Event { get; set; }

        [Display(Name = "EffectiveDateMedicare", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EffectiveDate { get; set; }

        [Display(Name = "HICNMedicare", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string HICN { get; set; }

        [Display(Name = "StateMedicare", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        [Display(Name = "CountyMedicare", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string County { get; set; }

        [Display(Name = "PBPIDMedicare", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PBPID { get; set; }

        [Display(Name = "FromDateMedicare", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "VerifiedDateMedicare", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? VerifiedDate { get; set; }

        [Display(Name = "SegmentIDMedicare", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string SegmentID { get; set; }

        [Display(Name = "InputMedicare", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Input { get; set; }

         [Display(Name = "ResultMedicare", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Result { get; set; }
    }
}