using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Note
{
    public class NoteViewModel
    {
        public NoteViewModel()
        {
            MedicalNecessaries = new List<MedicalNecessaries>();
        }

        public int? AuthorizationID { get; set; }

        public int NoteID { get; set; }

        [Display(Name = "Note Type", ShortName = "Type")]
        public string NoteType { get; set; }

        [Display(Name = "Date Time")]
        public DateTime? Date { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Subject { get; set; }

        [Display(Name = "Note")]
        public string Description { get; set; }

        [Display(Name = "Inc Fax")]
        public bool IncludeFax { get; set; }

        [Display(Name = "Note Status", ShortName = "Status")]
        public string NoteStatus { get; set; }

        public string Status { get; set; }

        [Display(Name = "Rationale")]
        public string RationaleDescription { get; set; }

        [Display(Name = "Alternate Plan")]
        public string AlternatePlanDescription { get; set; }

        [Display(Name = "Criteria Used")]
        public string CriteriaUsedDescription { get; set; }

        [Display(Name = "Service Subject To Notice")]
        public string ServiceSubjectToNotice { get; set; }

        [Display(Name = "List of Medical Services")]
        public List<MedicalNecessaries> MedicalNecessaries { get; set; }

        public bool IsPrivate { get; set; }

        public string MemberID { get; set; }

        [Display(Name = "Module Name", ShortName = "Module")]
        public string ModuleName { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}