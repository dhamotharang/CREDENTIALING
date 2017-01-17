using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class NotesCategoryViewModel : CommonPropViewModel
    {
        [Display(Name = "NotesCategoryID")]
        public int NotesCategoryID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}