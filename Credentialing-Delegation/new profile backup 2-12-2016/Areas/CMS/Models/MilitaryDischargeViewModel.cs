using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class MilitaryDischargeViewModel : CommonPropViewModel
    {
        [Display(Name = "MilitaryDischargeID")]
        public int MilitaryDischargeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}