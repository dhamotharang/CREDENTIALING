using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class POSTypeOfCareViewModel : CommonPropViewModel
    {
        [Display(Name = "POSTypeOfCareID")]
        public int POSTypeOfCareID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}