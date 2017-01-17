using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class StateLicenseStatusViewModel : CommonPropViewModel
    {
        [Display(Name = "StateLicenseStatusID")]
        public int StateLicenseStatusID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}