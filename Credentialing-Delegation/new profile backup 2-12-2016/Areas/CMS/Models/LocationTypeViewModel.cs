using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class LocationTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "LocationTypeID")]
        public int LocationTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}