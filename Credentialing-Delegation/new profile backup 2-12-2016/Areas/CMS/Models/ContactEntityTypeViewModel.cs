using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ContactEntityTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "ContactEntityTypeID")]
        public int ContactEntityTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}