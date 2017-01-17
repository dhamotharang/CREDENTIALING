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
        public ContactDetailViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ContactDetailID { get; set; }

        [Display(Name = "Pager Number")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Pager Number must be of 10 digits.")]
        public string PagerNumber { get; set; }

        public string CountryCode { get; set; }
        //[Display(Name = "Phone Number *")]
        public virtual ICollection<PhoneDetailViewModel> PhoneDetails { get; set; }

        [Display(Name = "Email Id *")]
        public ICollection<EmailDetailViewModel> EmailIDs { get; set; }

        [Display(Name = "Preferred method for written contact")]
        public ICollection<PreferredWrittenContactViewModel> PreferredWrittenContacts { get; set; }

        [Display(Name = "Preferred method for contact")]
        public ICollection<PreferredContactViewModel> PreferredContacts { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
