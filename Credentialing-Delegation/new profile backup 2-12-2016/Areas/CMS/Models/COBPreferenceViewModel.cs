using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class COBPreferenceViewModel : CommonPropViewModel
    {
        [Display(Name = "COBPreferenceID")]
        public int COBPreferenceID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}