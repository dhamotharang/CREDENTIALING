using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class CAScodeViewModel : CommonPropViewModel
    {
        [Display(Name = "CAScodeID")]
        public int CAScodeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}