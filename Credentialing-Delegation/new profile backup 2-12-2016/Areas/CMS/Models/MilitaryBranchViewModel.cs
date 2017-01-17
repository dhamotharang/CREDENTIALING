using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class MilitaryBranchViewModel : CommonPropViewModel
    {
        [Display(Name = "MilitaryBranchID")]
        public int MilitaryBranchID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}