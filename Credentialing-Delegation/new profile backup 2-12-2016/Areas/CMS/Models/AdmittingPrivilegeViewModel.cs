using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class AdmittingPrivilegeViewModel : CommonPropViewModel
    {
        [Display(Name = "AdmittingPrivilegeID")]
        public int AdmittingPrivilegeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}