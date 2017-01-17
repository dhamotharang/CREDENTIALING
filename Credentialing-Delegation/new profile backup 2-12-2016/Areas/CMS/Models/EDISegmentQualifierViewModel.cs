using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class EDISegmentQualifierViewModel : CommonPropViewModel
    {
        [Display(Name = "EDISegmentQualifierID")]
        public int EDISegmentQualifierID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}