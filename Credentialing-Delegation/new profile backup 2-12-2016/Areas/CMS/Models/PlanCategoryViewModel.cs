using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class PlanCategoryViewModel : CommonPropViewModel
    {
        [Display(Name = "PlanCategoryID")]
        public int PlanCategoryID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}