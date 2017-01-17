using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ReviewTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "ReviewTypeID")]
        public int ReviewTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}