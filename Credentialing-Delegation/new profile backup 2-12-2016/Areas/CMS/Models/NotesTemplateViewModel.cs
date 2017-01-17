using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class NotesTemplateViewModel : CommonPropViewModel
    {
        [Display(Name = "NotesTemplateID")]
        public int NotesTemplateID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}