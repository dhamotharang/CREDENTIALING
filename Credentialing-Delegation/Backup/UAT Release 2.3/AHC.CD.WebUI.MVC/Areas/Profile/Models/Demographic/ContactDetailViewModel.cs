using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class ContactDetailViewModel
    {
        public int? ContactDetailID { get; set; }

        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        [StringLength(10, MinimumLength=10, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        [Display(Name = "Pager Number")]
        public string PagerNumber { get; set; }

        public string CountryCode { get; set; }

        //[Display(Name = "Phone Number *")]
        public virtual IList<PhoneDetailViewModel> PhoneDetails { get; set; }

        //[Display(Name = "Email Id *")]
        public IList<EmailDetailViewModel> EmailIDs { get; set; }

        //[Display(Name = "Preferred method for written contact")]
        public ICollection<PreferredWrittenContactViewModel> PreferredWrittenContacts { get; set; }

        [Display(Name = "Preferred method for contact")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        public IList<PreferredContactViewModel> PreferredContacts { get; set; }
    }
}
