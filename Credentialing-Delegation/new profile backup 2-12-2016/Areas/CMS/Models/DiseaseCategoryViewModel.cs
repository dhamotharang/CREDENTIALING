using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DiseaseCategoryViewModel : CommonPropViewModel
    {
        [Display(Name = "DiseaseCategoryID")]
        public int DiseaseCategoryID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}