using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class LanguageViewModel
    {
        public int LanguageId { get; set; }

        public string LanguageCode { get; set; }

        public string LanguageName { get; set; }

        public string CanRead { get; set; }

        public string CanWrite { get; set; }

        public string CanSpeak { get; set; }

        public string IsPreferredSpokenLanguage { get; set; }

        public string IsPreferredWrittenLanguage { get; set; }

        public string Status { get; set; }

        public string SourceCode { get; set; }

        public string SourceName { get; set; }

        public string TimeStamp { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }
    }
}