using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class QuestionCategoryViewModel : CommonPropViewModel
    {
        [Display(Name = "QuestionCategoryID")]
        public int QuestionCategoryID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}