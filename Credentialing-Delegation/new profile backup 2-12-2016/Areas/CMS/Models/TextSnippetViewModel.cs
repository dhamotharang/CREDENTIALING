using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class TextSnippetViewModel : CommonPropViewModel
    {
        [Display(Name = "TextSnippetID")]
        public int TextSnippetID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}