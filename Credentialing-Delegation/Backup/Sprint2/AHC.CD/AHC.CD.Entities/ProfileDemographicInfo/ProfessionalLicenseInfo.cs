using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileDemographicInfo
{
    public abstract class ProfessionalLicenseInfo
    {
        public int ProfessionalLicenseInfoID { get; set; }
        public string CurrentPracticeState { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ExpiryDate { get; set; }
        public bool IsInGoodStanding { get; set; }
        public bool IsLicenseRelinquished { get; set; }
        public bool IsSanctionApplied { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? IssueDateCurrent { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? IssueDateOriginal { get; set; }
        public string LicenseIssueState { get; set; }
        public string LicenseNo { get; set; }
        public DateTime? RelinquishedDate { get; set; }
        public virtual LicenseType LicenseType { get; set; }

    }
}
