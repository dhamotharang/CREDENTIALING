using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class NoteSubjectViewModel : CommonPropViewModel
    {
        [Display(Name = "NoteSubjectID")]
        public int NoteSubjectID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}