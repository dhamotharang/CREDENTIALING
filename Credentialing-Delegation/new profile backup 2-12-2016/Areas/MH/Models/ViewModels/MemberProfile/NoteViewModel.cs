using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class NoteViewModel
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string SourceCode { get; set; }

        public string SourceName { get; set; }

        public string TimeStamp { get; set; }

        public string AddedByEmail { get; set; }

        public string AddedOnDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }

    }
}