using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class PatientRelationViewModel : CommonPropViewModel
    {
        [Display(Name = "PatientRelationID")]
        public int PatientRelationID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}