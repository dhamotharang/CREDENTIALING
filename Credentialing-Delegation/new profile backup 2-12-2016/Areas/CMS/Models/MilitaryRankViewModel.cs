using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class MilitaryRankViewModel : CommonPropViewModel
    {
        [Display(Name = "MilitaryRankID")]
        public int MilitaryRankID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}