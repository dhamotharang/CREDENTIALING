using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class EducationViewModel
    {
        public int EducationId { get; set; }

        public string InstitutionCode { get; set; }

        public string InstitutionName { get; set; }

        public string CourseCode { get; set; }

        public string CourseName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Status { get; set; }

        public string SourceCode { get; set; }

        public string SourceName { get; set; }

        public string TimeStamp { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedByEmail { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }

    }
}