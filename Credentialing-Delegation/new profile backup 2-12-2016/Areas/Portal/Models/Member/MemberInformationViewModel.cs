using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MemberInformationViewModel
    {
        [JsonProperty("memberInformationID")]
        public int MemberInformationID { get; set; }
        #region Labels
        [Display(Name = "MemberInformationTitle", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string MemberInformationTitle { get; set; }
        #endregion
        [Display(Name = "PlanName", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        [JsonProperty("planName")]
        public string PlanName { get; set; }

        [Display(Name = "StatusMember", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        [JsonProperty("status")]
        public string Status { get; set; }

        [Display(Name = "SubGroup", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string SubGroup { get; set; }
        // public DateTime? MemberEffectiveDate { get; set; }

        [Display(Name = "MemberEffectiveDate", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? MemberEffectiveDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public DateTime? PlanEffectiveDate { get; set; }

        [Display(Name = "PrimaryContact", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PrimaryContact { get; set; }

        [Display(Name = "AlternateContact", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string AlternateContact { get; set; }

        [Display(Name = "AddressLine1", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string AddressLine { get; set; }

        //public string AddressLine2 { get; set; }

        [Display(Name = "City", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }

        [Display(Name = "State", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        public string County { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ZipCode { get; set; }

        [Display(Name = "Email", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Email { get; set; }

        //public MembershipViewModel Membership { get; set; }

        //public MemberContactViewModel MemberContact{ get; set; }
        //public MemberInformationViewModel() {
        //    Membership = new MembershipViewModel();
        //    MemberContact = new MemberContactViewModel();
        //}
    }
}