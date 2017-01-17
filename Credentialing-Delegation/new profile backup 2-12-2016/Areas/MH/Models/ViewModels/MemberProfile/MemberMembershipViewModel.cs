using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class MemberMembershipViewModel
    {

        public MemberProfileViewModel Member { get; set; }

        public PrimaryProviderViewModel PrimaryProvider { get; set; }

        public MembershipInformationViewModel MembershipInformation { get; set; }

        public string MemberID { get; set; }

        public int MembershipId { get; set; }

        public string RelationshipCode { get; set; }

        [Display(Name = "Patient Relation")]
        [DisplayFormat(NullDisplayText = "-")]
        public string RelationshipName { get; set; }

        public string EmergencyContactId { get; set; } //Emergency Contact

        public string InsuranceCompanyCode { get; set; }

        public string InsuranceCompanyName { get; set; }

        public string PlanCode { get; set; }

        public string PlanName { get; set; }

        public string PremiumDetailId { get; set; } //Premium Details

        public string LobCode { get; set; }

        public string LobName { get; set; }

        public string EffectiveDate { get; set; }

        public string TerminationDate { get; set; }

        public string Preference { get; set; }

        //public OtherPersonInformationViewModel OtherPerson { get; set; }

        public string EnrollmentStatus { get; set; }

        public string EligibilityStatus { get; set; }

        public string Status { get; set; }

        public string SourceCode { get; set; }

        public string SourceName { get; set; }

        public string TimeStamp { get; set; }

        //public string LastEncounterDate { get; set; }

        //public string LastEnrollmentDate { get; set; }



        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }

        //Other Properties
        public bool IsSubscriber { get; set; }

        public bool HasOtherInsurance { get; set; }

    }
}