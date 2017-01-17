using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class OutcomeTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "OutcomeTypeID")]
        public int OutcomeTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}