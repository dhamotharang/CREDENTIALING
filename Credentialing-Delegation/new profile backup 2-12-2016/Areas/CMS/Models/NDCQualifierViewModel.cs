using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class NDCQualifierViewModel : CommonPropViewModel
    {
        [Display(Name = "NDCQualifierID")]
        public int NDCQualifierID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}