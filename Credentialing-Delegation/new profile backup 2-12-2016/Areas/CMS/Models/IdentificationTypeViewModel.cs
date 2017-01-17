using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class IdentificationTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "IdentificationTypeID")]
        public int IdentificationTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}