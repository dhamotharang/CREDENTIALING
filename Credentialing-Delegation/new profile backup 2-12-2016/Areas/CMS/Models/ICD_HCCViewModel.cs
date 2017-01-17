using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ICD_HCCViewModel : CommonPropViewModel
    {
        [Display(Name = "ICD_HCCID")]
        public int ICD_HCCID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}