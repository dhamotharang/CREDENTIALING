using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class OutcomeViewModel : CommonPropViewModel
    {
        [Display(Name = "OutcomeID")]
        public int OutcomeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}