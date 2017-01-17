using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class AdjustmentReasonCodeViewModel : CommonPropViewModel
    {
        [Display(Name = "AdjustmentReasonCodeID")]
        public int AdjustmentReasonCodeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}