using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class AdmissionTypeCodeViewModel : CommonPropViewModel
    {
        [Display(Name = "AdmissionTypeCodeID")]
        public int AdmissionTypeCodeID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}