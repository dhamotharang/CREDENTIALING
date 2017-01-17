using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class AdjustmentGroupCodeViewModel : CommonPropViewModel
    {
        [Display(Name = "AdjustmentGroupCodeID")]
        public int AdjustmentGroupCodeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}