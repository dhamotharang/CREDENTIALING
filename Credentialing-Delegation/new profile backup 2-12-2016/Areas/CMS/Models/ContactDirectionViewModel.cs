using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ContactDirectionViewModel : CommonPropViewModel
    {
        [Display(Name = "ContactDirectionID")]
        public int ContactDirectionID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}