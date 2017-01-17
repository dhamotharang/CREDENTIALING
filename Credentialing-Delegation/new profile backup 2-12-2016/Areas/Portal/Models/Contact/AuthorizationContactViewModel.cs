using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Contact
{
    public class AuthorizationContactViewModel
    {
        public int? AuthorizationID { get; set; }

        public int? AuthorizationContactID { get; set; }

        [Display(Name = "Contact Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactName { get; set; }

        [Display(Name = "Contact Detail")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EMailFaxOther { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string Direction { get; set; }

        [Display(Name = "Inc Fax")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IncludeFax { get; set; }

        [Display(Name = "Date and Time of Call", ShortName = "Date Time")]
        [DisplayFormat(NullDisplayText = "-")]
       // [Column(TypeName = "datetime2")]
        public DateTime? CallDateTime { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Entity")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactEntity { get; set; }

        [Display(Name = "Contact Type", ShortName = "Type")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactType { get; set; }

        [Display(Name = "Outcome Type")]
        [DisplayFormat(NullDisplayText = "-")]
        public string OutcomeType { get; set; }

        [Display(Name = "Outcome")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Outcome { get; set; }

        [Display(Name = "Note")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Description { get; set; }

         [DisplayFormat(NullDisplayText = "-")]
        public string Reason { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Created By")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CreatedBy { get; set; }

        public string MemberID { get; set; }

        [Display(Name = "Module Name", ShortName = "Module")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ModuleName { get; set; }

        [Display(Name = "Entity Type")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactEntityType { get; set; }

        public string PreviewFilePath { get; set; }

        public string Status { get; set; }

        public string ContactViewType { get; set; }
        #region Notes Label
        [Display(Name = "CONTACTS")]
        public string ContactsLabel { get; set; }

        [Display(Name = "Action")]
        public string ActionLabel { get; set; }
        #endregion

    }
}