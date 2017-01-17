using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DocumentCategoryViewModel : CommonPropViewModel
    {
        [Display(Name = "DocumentCategoryID")]
        public int DocumentCategoryID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}