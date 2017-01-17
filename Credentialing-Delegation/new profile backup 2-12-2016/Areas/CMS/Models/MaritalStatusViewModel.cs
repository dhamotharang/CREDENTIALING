using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class MaritalStatusViewModel : CommonPropViewModel
    {
        [Display(Name = "MaritalStatusID")]
        public int MaritalStatusID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}