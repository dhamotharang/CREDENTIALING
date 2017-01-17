using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class RoomTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "RoomTypeID")]
        public int RoomTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}