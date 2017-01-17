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
        public int ContactDetailID { get; set; }

        public virtual ICollection<PhoneDetailViewModel> PhoneDetails { get; set; }

        [Display(Name = "Email Id *")]
        public ICollection<EmailDetailViewModel> EmailIDs { get; set; }

        [Display(Name = "Preferred method for written contact *")]
        public ICollection<PreferredWrittenContactViewModel> PreferredWrittenContacts { get; set; }

        [Display(Name = "Preferred method for contact *")]
        public ICollection<PreferredContactViewModel> PreferredContacts { get; set; }
    }

    public class PhoneDetailViewModel
    {
        public int PhoneDetailID { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string CountryCode { get; set; }

        [Required]
        public string PhoneType { get; set; }

        [Required]
        public string Preference { get; set; }

        [Required]
        public string Status { get; set; }
    }

    public class EmailDetailViewModel
    {
        public int EmailDetailID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string EmailAddress { get; set; }

        [Required]
        public string Preference { get; set; }

        [Required]
        public string Status { get; set; }
    }

    public class PreferredWrittenContactViewModel
    {
        public int PreferredWrittenContactID { get; set; }

        [Required]
        public string ContactType { get; set; }

        [Required]
        public int PreferredIndex { get; set; }
    }
    
    public class PreferredContactViewModel
    {
        public int PreferredContactID { get; set; }

        [Required]
        public string ContactType { get; set; }
       
        [Required]
        public int PreferredIndex { get; set; }
    }
}
