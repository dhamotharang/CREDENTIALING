using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class NDCQuantityQualifierViewModel : CommonPropViewModel
    {
        [Display(Name = "NDCQuantityQualifierID")]
        public int NDCQuantityQualifierID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}