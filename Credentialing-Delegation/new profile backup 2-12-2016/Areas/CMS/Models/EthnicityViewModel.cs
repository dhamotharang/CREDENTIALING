using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class EthnicityViewModel : CommonPropViewModel
    {
        [Display(Name = "EthnicityID")]
        public int EthnicityID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}