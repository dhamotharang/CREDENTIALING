using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DisciplineViewModel : CommonPropViewModel
    {
        [Display(Name = "DisciplineID")]
        public int DisciplineID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}