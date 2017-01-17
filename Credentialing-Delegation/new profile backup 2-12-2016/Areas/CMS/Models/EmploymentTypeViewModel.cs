using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class EmploymentTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "EmploymentTypeID")]
        public int EmploymentTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}