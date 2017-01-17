using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MemberCOBViewModel
    {
        [Display(Name = "InsuranceType",ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        [JsonProperty("insuranceType")]
        public string InsuranceType { get; set; }

        [Display(Name = "Order", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Order { get; set; }

        [Display(Name = "SupplementalDrugType", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        [JsonProperty("supplementalDrugType")]
        public string SupplementalDrugType { get; set; }

        [Display(Name = "Carrier", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        [JsonProperty("carrier")]
        public string Carrier { get; set; }

        [Display(Name = "Effective", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        [JsonProperty("effective")]
        public DateTime? Effective { get; set; }
    }
}