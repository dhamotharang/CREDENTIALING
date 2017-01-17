using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ComplaintCategoryViewModel : CommonPropViewModel
    {
        [Display(Name = "ComplaintCategoryID")]
        public int ComplaintCategoryID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}