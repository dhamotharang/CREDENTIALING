using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DEAScheduleTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "DEAScheduleTypeID")]
        public int DEAScheduleTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}