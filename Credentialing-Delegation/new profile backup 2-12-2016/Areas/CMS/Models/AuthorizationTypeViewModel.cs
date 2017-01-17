using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class AuthorizationTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "AuthorizationTypeID")]
        public int AuthorizationTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}