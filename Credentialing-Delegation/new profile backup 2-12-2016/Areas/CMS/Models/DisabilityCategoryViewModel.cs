using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DisabilityCategoryViewModel : CommonPropViewModel
    {
        [Display(Name = "DisabilityCategoryID")]
        public int DisabilityCategoryID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}