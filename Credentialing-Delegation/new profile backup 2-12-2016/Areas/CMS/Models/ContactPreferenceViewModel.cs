using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ContactPreferenceViewModel : CommonPropViewModel
    {
        [Display(Name = "ContactPreferenceID")]
        public int ContactPreferenceID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}