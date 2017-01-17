using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ClaimRelatedConditionCodeViewModel : CommonPropViewModel
    {
        [Display(Name = "ClaimRelatedConditionCodeID")]
        public int ClaimRelatedConditionCodeID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}