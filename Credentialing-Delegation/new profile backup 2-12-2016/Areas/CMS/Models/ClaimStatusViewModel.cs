using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ClaimStatusViewModel : CommonPropViewModel
    {
        [Display(Name = "ClaimStatusID")]
        public int ClaimStatusID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}