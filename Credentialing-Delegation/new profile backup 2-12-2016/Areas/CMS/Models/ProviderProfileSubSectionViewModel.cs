using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ProviderProfileSubSectionViewModel : CommonPropViewModel
    {
        [Display(Name = "ProviderProfileSubSectionID")]
        public int ProviderProfileSubSectionID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}