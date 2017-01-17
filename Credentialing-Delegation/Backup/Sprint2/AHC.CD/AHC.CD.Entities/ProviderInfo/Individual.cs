using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.ProfileDemographicInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProviderInfo
{
    public class Individual : Provider
    {
                
        public virtual ICollection<AddressInfo> AddressInfos { get; set; }

        public virtual PersonalInfo PersonalInfo { get; set; }

        public virtual ICollection<ContactInfo> ContactInfos { get; set; }

        public virtual ICollection<IndividualPlan> IndividualPlans {get;set;}

        public virtual ICollection<CredentialingInfo> CredentialingHistory {get;set;}

        public virtual BirthInfo BirthInfo { get; set; }

        public virtual LanguageInfo LanguageInfo { get; set; }

        public virtual CitizenshipInfo CitizenshipInfo { get; set; }

        public virtual ICollection<ProfessionalLicenseInfo> ProfessionalLicenseInfos { get; set; }

        public virtual ICollection<ProfessionalIdentificationInfo> ProfessionalIdentificationInfos { get; set; }

        public virtual WorkHistoryInfo WorkHistoryInfo { get; set; }

    }
}
