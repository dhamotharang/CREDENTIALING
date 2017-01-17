using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class FeeScheduleViewModel : CommonPropViewModel
    {
        [Display(Name = "FeeScheduleID")]
        public int FeeScheduleID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}