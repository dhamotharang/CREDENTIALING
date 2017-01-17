using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class RequestTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "RequestTypeID")]
        public int RequestTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}