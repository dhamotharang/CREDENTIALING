using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class BankAccountTypeViewModel : CommonPropViewModel
    {
        [Display(Name = "BankAccountTypeID")]
        public int BankAccountTypeID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}