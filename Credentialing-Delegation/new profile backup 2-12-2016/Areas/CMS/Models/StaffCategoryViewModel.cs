using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class StaffCategoryViewModel : CommonPropViewModel
    {
        [Display(Name = "StaffCategoryID")]
        public int StaffCategoryID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}