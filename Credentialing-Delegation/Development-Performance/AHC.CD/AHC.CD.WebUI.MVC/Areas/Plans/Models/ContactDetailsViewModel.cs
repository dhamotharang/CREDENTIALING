using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Plans.Models
{
    public class ContactDetailsViewModel
    {
        public int? ContactDetailID { get; set; }

        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        [Display(Name = "Pager Number")]
        public string PagerNumber { get; set; }

        public string CountryCode { get; set; }

        public virtual IList<PhoneDetailsViewModel> PhoneDetails { get; set; }

        public IList<EmailDetailsViewModel> EmailIDs { get; set; }

        public ICollection<PreferredWrittenContactsViewModel> PreferredWrittenContacts { get; set; }

        [Display(Name = "Preferred method for contact")]
        public IList<PreferredContactsViewModel> PreferredContacts { get; set; }
    }
}