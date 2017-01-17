using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ClaimFrequencyCodeViewModel : CommonPropViewModel
    {
        [Display(Name = "ClaimFrequencyCodeID")]
        public int ClaimFrequencyCodeID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}