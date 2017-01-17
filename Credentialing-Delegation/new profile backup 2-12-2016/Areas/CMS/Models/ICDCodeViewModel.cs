using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ICDCodeViewModel : CommonPropViewModel
    {
        [Display(Name = "ICDCodeID")]
        public int ICDCodeID { get; set; }

        [Display(Name = "ICD Code")]
        [JsonProperty("ICD")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Version")]
        [Required]
        public string ICDVersion { get; set; }
    }
}