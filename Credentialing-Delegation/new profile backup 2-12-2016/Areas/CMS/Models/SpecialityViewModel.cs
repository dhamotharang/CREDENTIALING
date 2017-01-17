using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class SpecialityViewModel : CommonPropViewModel
    {
        [Display(Name = "SpecialityID")]
        public int SpecialityID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}