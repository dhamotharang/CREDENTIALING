using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ProviderRelationshipViewModel : CommonPropViewModel
    {
        [Display(Name = "ProviderRelationshipID")]
        public int ProviderRelationshipID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}