using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class POSUmServiceGroupViewModel : CommonPropViewModel
    {
        [Display(Name = "POSUmServiceGroupID")]
        public int POSUmServiceGroupID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}