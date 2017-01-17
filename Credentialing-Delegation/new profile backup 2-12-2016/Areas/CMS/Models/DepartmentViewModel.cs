using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DepartmentViewModel : CommonPropViewModel
    {
        [Display(Name = "DepartmentID")]
        public int DepartmentID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}