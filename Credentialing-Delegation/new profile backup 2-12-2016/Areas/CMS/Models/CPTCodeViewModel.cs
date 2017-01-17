using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class CPTCodeViewModel : CommonPropViewModel
    {
        [Display(Name = "CPTCodeID")]
        public int CPTCodeID { get; set; }

        [Display(Name = "CPT Code")]
        public string CPT { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Is E and M")] //evaluation and management
        public bool IsE_M { get; set; }
    }
}