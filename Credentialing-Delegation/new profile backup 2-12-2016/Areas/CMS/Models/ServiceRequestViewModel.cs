using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ServiceRequestViewModel : CommonPropViewModel
    {
        [Display(Name = "ServiceRequestID")]
        public int ServiceRequestID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}