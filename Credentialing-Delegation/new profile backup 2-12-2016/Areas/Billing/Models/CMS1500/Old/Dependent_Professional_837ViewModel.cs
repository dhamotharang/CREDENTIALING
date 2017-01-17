using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.Old
{
    public class Dependent_Professional_837ViewModel
    {
        public int Dependentkey { get; set; }
        public Nullable<int> Subscriberkey { get; set; }
        public string PAT01_IndividualRelationshipCode { get; set; }
        public string PAT06_PatientDeathDate { get; set; }
        public string PAT08_PatientWeightPounds { get; set; }
        public string PAT09_Pregnant { get; set; }
        public string NM102_PatientTypeQualifier { get; set; }
        public string NM103_PatientLastOrOrganizationName { get; set; }
        public string NM104_PatientFirst { get; set; }
        public string NM105_PatientMiddle { get; set; }
        public string NM107_PatientSuffix { get; set; }
        public string N301_PatientAddr1 { get; set; }
        public string N302_PatientAddr2 { get; set; }
        public string N401_PatientCity { get; set; }
        public string N402_PatientState { get; set; }
        public string N403_PatientZip { get; set; }
        public string N404_PatientCountry { get; set; }
        public string N407_PatientCountrySubdivision { get; set; }
        public string DMG02_PatientBirthDate { get; set; }
        public string DMG03_PatientGenderCode { get; set; }
        public string REF02_PropertyCasualtyClaimNo { get; set; }
        public string REF02_PatientSocialSecurityNo { get; set; }
        public string REF02_MemberIdNo { get; set; }
        public string PER02_PatientContactName { get; set; }
        public string PER04_PatientPhoneNo { get; set; }
        public string PER06_PatientPhoneExtNo { get; set; }
        public int Subscriber_Professional_837ID { get; set; }

        //[ForeignKey("Subscriber_Professional_837ID")]
        //public Subscriber_Professional_837 Subscriber_Professional_837 { get; set; }
    }
}
