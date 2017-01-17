using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DiseaseNameViewModel : CommonPropViewModel
    {
        [Display(Name = "DiseaseNameID")]
        public int DiseaseNameID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}