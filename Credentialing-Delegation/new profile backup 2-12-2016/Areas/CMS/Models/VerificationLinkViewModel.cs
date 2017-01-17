using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class VerificationLinkViewModel : CommonPropViewModel
    {
        [Display(Name = "VerificationLinkID")]
        public int VerificationLinkID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}