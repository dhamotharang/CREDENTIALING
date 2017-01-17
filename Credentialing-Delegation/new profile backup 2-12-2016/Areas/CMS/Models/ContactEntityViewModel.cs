using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ContactEntityViewModel : CommonPropViewModel
    {
        [Display(Name = "ContactEntityID")]
        public int ContactEntityID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}