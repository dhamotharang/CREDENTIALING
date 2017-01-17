using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ReviewLinkViewModel : CommonPropViewModel
    {
        [Display(Name = "ReviewLinkID")]
        public int ReviewLinkID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}