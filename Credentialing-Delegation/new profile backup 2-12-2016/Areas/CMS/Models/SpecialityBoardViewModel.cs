using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class SpecialityBoardViewModel : CommonPropViewModel
    {
        [Display(Name = "SpecialityBoardID")]
        public int SpecialityBoardID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}