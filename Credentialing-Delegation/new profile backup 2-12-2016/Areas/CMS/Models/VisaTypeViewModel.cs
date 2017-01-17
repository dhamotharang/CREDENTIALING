using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class VisaTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "VisaTypeID")]
        public int VisaTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}