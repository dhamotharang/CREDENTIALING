using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Contact
{
    public class AuthorizationContactViewModel
    {
        public int? AuthorizationID { get; set; }

        public int? AuthorizationContactID { get; set; }

        [Display(Name = "ContactName", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactName { get; set; }

        [Display(Name = "ContactDetail", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string EMailFaxOther { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string Direction { get; set; }

        [Display(Name = "IncFax", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IncludeFax { get; set; }

        [Display(Name = "DateandTimeofCall", ShortName = "Date Time", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        //[Column(TypeName = "datetime2")]
        public DateTime? CallDateTime { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Entity", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactEntity { get; set; }

        [Display(Name = "ContactType", ShortName = "Type", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactType { get; set; }

        [Display(Name = "OutcomeType", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string OutcomeType { get; set; }

        [Display(Name = "Outcome", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Outcome { get; set; }

        [Display(Name = "Note", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Description { get; set; }

         [DisplayFormat(NullDisplayText = "-")]
        public string Reason { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string CreatedBy { get; set; }

        public string MemberID { get; set; }

        [Display(Name = "ModuleName", ShortName = "Module", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ModuleName { get; set; }

        [Display(Name = "EntityType", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactEntityType { get; set; }

        public string PreviewFilePath { get; set; }

        public string Status { get; set; }

        public string ContactViewType { get; set; }

    }
}