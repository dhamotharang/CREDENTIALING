using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.Note
{
    public class NoteViewModel
    {       
        public int? AuthorizationID { get; set; }

        public int NoteID { get; set; }

        [Display(Name = "Note Type", ShortName = "Type")]
        [DisplayFormat(NullDisplayText = "-")]
        public string NoteType { get; set; }

        [Display(Name = "DATE &TIME")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? Date { get; set; }

        [Display(Name = "User Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string UserName { get; set; }

        public string Subject { get; set; }

        [Display(Name = "Note")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Description { get; set; }

        [Display(Name = "Inc Fax")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IncludeFax { get; set; }

        [Display(Name = "Note Status", ShortName = "Status")]
        [DisplayFormat(NullDisplayText = "-")]
        public string NoteStatus { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string Status { get; set; }

        [Display(Name = "Rationale")]
        [DisplayFormat(NullDisplayText = "-")]
        public string RationaleDescription { get; set; }

        [Display(Name = "Alternate Plan")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AlternatePlanDescription { get; set; }

        [Display(Name = "Criteria Used")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CriteriaUsedDescription { get; set; }

        [Display(Name = "Service Subject To Notice")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ServiceSubjectToNotice { get; set; }

        public bool IsPrivate { get; set; }

        public string MemberID { get; set; }

        [Display(Name = "Module Name", ShortName = "Module")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ModuleName { get; set; }

        [Display(Name = "Created By")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CreatedBy { get; set; }

        #region Notes Label
        [Display(Name = "NOTES")]
        public string NOTESLabel { get; set; }

        [Display(Name = "Action")]
        public string ActionLabel { get; set; }
        #endregion
    }
}