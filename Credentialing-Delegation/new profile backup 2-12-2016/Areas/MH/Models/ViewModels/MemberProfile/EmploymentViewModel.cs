using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class EmploymentViewModel
    {
        public int EmploymentId { get; set; }

        public string EmployerCode { get; set; }

        public string EmployerName { get; set; }

        public string IsPartTime { get; set; }

        public string TypeOfWorkCode { get; set; }

        public string TypeOfWork { get; set; }

        public string JobTitle { get; set; }

        public string IncomeFrequency { get; set; }

        public double Wages { get; set; }

        public double AverageHoursWorkedPerWeek { get; set; }

        public string IsCurrentJob { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string EmploymentStatus { get; set; }

        public string DepartureReason { get; set; }

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