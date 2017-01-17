using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class PlainLanguageViewModel : CommonPropViewModel
    {
        [Display(Name = "PlainLanguageID")]
        public int PlainLanguageID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}