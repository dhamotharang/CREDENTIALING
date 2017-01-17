using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class POSRoomTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "POSRoomTypeID")]
        public int POSRoomTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}