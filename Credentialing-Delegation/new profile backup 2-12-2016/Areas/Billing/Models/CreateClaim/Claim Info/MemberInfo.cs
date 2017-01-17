using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim.Claim_Info
{
    public class MemberInfo
    {

        public string PatientLastOrOrganizationName { get; set; }

        public string PatientMiddleName { get; set; }

        public string PatientFirstName { get; set; }

        public DateTime PatientBirthDate { get; set; }

        public string PatientGenders { get; set; }

        public string IndividualRelationshipCode { get; set; }

        public string PatientFirstAddress { get; set; }

        public string PatientSecondAddress { get; set; }

        public string PatientCity { get; set; }

        public string PatientState { get; set; }

        public string PatientZip { get; set; }

        public string PatientPhoneNo { get; set; }

        public string SubscriberLastOrOrganizationName { get; set; }

        public string SubscriberMiddleName { get; set; }

        public string SubscriberFirstName { get; set; }

        public string SubscriberFirstAddress { get; set; }

        public string SubscriberSecondAddress { get; set; }

        public DateTime SubscriberBirthDate { get; set; }

        public string SubscriberGenders { get; set; }

        public string SubscriberCity { get; set; }

        public string SubscriberState { get; set; }

        public string SubscriberZip { get; set; }

        public string SubscriberPhoneNo { get; set; }

        public string InsuredIdNumber { get; set; }

        public string SubscriberGroup_PolicyNo { get; set; }

        public string SubscriberGroup_PlanName { get; set; }

        public string OtherInsuredLastName { get; set; }

        public string OtherInsuredMiddleName { get; set; }

        public string OtherInsuredFirstName { get; set; }

        public string OtherInsuredPolicyNo { get; set; }

        public string OtherInsuredPlanName { get; set; }

        public string PatientAccountNumber { get; set; }
    }
}