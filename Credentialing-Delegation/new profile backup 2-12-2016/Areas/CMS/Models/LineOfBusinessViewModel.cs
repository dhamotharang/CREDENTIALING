using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class LineOfBusinessViewModel : CommonPropViewModel
    {
        [Display(Name = "LineOfBusinessID")]
        public int LineOfBusinessID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}