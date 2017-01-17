using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DeductionTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "DeductionTypeID")]
        public int DeductionTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}