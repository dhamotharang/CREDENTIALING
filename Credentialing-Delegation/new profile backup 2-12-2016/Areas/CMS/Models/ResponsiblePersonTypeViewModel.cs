using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class ResponsiblePersonTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "ResponsiblePersonTypeID")]
        public int ResponsiblePersonTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}