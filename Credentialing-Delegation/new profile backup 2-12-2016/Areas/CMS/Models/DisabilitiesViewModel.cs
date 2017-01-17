using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DisabilitiesViewModel : CommonPropViewModel
    {
        [Display(Name = "DisabilitiesID")]
        public int DisabilitiesID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}