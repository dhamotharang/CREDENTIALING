using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class VisaStatusViewModel : CommonPropViewModel
    {
        [Display(Name = "VisaStatusID")]
        public int VisaStatusID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}