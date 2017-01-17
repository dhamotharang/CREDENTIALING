using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class TypeOfCareViewModel : CommonPropViewModel
    {
        [Display(Name = "TypeOfCareID")]
        public int TypeOfCareID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}