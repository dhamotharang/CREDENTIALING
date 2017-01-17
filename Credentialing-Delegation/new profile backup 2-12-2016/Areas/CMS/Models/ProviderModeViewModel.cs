using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ProviderModeViewModel : CommonPropViewModel
    {
        [Display(Name = "ProviderModeID")]
        public int ProviderModeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}