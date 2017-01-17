using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class QualificationDegreeViewModel : CommonPropViewModel
    {
        [Display(Name = "QualificationDegreeID")]
        public int QualificationDegreeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}