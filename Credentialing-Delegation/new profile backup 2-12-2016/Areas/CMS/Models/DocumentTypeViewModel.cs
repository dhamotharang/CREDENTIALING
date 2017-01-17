using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class DocumentTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "DocumentTypeID")]
        public int DocumentTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}