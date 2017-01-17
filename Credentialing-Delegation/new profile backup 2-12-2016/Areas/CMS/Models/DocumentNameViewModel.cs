using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DocumentNameViewModel : CommonPropViewModel
    {
        [Display(Name = "DocumentNameID")]
        public int DocumentNameID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}