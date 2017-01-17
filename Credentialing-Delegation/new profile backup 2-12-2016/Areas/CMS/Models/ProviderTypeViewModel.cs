using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ProviderTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "ProviderTypeID")]
        public int ProviderTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}