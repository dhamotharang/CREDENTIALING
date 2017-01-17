using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.ProviderInfo
{
    public class EmploymentInfo
    {
        public int EmploymentInfoID { get; set; }
        public bool CanContactEmployer { get; set; }
        public string Department { get; set; }
        public bool IsCurrentEmployer { get; set; }
        public string SupervisorName { get; set; }
        public string Title { get; set; }
        public virtual Employer Employer { get; set; }
    }
}
