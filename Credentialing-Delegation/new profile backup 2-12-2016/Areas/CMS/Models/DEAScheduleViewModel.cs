using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DEAScheduleViewModel : CommonPropViewModel
    {
        [Display(Name = "DEAScheduleID")]
        public int DEAScheduleID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}