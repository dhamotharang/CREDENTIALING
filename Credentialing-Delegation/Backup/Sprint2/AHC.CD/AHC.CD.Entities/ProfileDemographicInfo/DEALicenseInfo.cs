using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileDemographicInfo
{
    public class DEALicenseInfo : ProfessionalLicenseInfo
    {
        public bool IsControlSubstanceRegnCertAvail { get; set; }
        public string ScopeDescription { get; set; }
        public virtual ICollection<DEASchedule> DEASchedules { get; set; }
        public LicenseScope LicenseScope { get; set; }
    }
}
