using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ClaimValueCodeViewModel : CommonPropViewModel
    {
        [Display(Name = "ClaimValueCodeID")]
        public int ClaimValueCodeID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}