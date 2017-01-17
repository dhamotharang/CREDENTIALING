using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class LevelOfCareViewModel : CommonPropViewModel
    {
        [Display(Name = "LevelOfCareID")]
        public int LevelOfCareID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}