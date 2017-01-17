using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DecredentialingReasonViewModel : CommonPropViewModel
    {
        [Display(Name = "DecredentialingReasonID")]
        public int DecredentialingReasonID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}