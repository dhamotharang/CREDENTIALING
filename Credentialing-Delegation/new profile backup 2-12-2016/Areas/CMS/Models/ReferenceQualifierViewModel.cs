using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ReferenceQualifierViewModel : CommonPropViewModel
    {
        [Display(Name = "ReferenceQualifierID")]
        public int ReferenceQualifierID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}