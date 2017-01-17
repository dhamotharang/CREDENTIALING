using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ClaimFormStatusViewModel : CommonPropViewModel
    {
        [Display(Name = "ClaimFormStatusID")]
        public int ClaimFormStatusID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}