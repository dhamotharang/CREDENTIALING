using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DateQualifierViewModel : CommonPropViewModel
    {
        [Display(Name = "DateQualifierID")]
        public int DateQualifierID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}