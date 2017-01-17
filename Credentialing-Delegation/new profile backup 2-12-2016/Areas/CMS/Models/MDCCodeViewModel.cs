using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class MDCCodeViewModel : CommonPropViewModel
    {
        [Display(Name = "MDCCodeID")]
        public int MDCCodeID { get; set; }

        [Display(Name = "Code")]
        [JsonProperty("MDC")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}