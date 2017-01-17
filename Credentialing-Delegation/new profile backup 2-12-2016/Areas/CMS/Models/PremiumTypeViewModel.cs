using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class PremiumTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "PremiumTypeID")]
        public int PremiumTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}