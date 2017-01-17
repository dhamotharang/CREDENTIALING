using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class MilitaryPresentDutyViewModel : CommonPropViewModel
    {
        [Display(Name = "MilitaryPresentDutyID")]
        public int MilitaryPresentDutyID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}