using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MemberEnrollmentViewModel
    {
        #region Basic Details
        [Display(Name = "MemberNameEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string MemberName { get; set; }

        [Display(Name = "RelationshipEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Relationship { get; set; }

        [Display(Name = "GenderEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Gender { get; set; }

        [Display(Name = "ConfCommunicationEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ConfCommunication { get; set; }

        [Display(Name = "BirthDateEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BirthDate { get; set; }



        [Display(Name = "SSNEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string SSN { get; set; }


        [Display(Name = "EffDateEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EffDate { get; set; }

        [Display(Name = "StatusEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Status { get; set; }

        [Display(Name = "MaritalStatusEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string MaritalStatus { get; set; }

        [Display(Name = "MedicareNumberEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string MedicareNumber { get; set; }

        [Display(Name = "FamilylinkIDEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string FamilylinkID { get; set; }

        [Display(Name = "HistoryLinkIDEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string HistoryLinkID { get; set; }

        [Display(Name = "StandardUniqueHealthIDEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string StandardUniqueHealthID { get; set; }

        [Display(Name = "ExclusionaryPeriodCreditDaysEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public int ExclusionaryPeriodCreditDays { get; set; }
        #endregion

        #region Hippa 
        [Display(Name = "HipaaStartDateEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? HipaaStartDate { get; set; }

        [Display(Name = "HipaaEndDateEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? HipaaEndDate { get; set; }
        #endregion
        #region PRE -ex
        [Display(Name = "PreExStartDateEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? PreExStartDate { get; set; }


        [Display(Name = "PreExEndDateEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? PreExEndDate { get; set; }

        [Display(Name = "PreExCreditDaysEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public int PreExCreditDays { get; set; }

        [Display(Name = "LimitsEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Limits { get; set; }

        [Display(Name = "ApplicantTypeEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ApplicantType { get; set; }

        [Display(Name = "EligibilityDateEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EligibilityDate { get; set; }

        [Display(Name = "QualifyingEventDateEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? QualifyingEventDate { get; set; }

        [Display(Name = "EOITerminationDateEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EOITerminationDate { get; set; }

        [Display(Name = "NewSignatureDateEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? NewSignatureDate { get; set; }

         [Display(Name = "PriorBillingIndicatorEnrollment", ResourceType = typeof(App_LocalResources.Content))]
         [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? PriorBillingIndicator { get; set; }

        [Display(Name = "PrioBillingEffDatedEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
         public DateTime? PrioBillingEffDated { get; set; }

        [Display(Name = "RecordNumberEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public int RecordNumber { get; set; }

        [Display(Name = "LanguageEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Language { get; set; }
        #endregion
        
        #region Address
        [Display(Name = "HomeAddressEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string HomeAddress { get; set; }

         [Display(Name = "MailingAddressEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string MailingAddress { get; set; }

        [Display(Name = "WorkAddressEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string WorkAddress { get; set; }

        [Display(Name = "CellPhoneEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string CellPhone { get; set; }

        [Display(Name = "MemoEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Memo { get; set; }
        
        #endregion
        
        
        #region Labels
        /*Just to maintain localization in labels*/
        [Display(Name = "BasicDetailsTitleEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string BasicDetailsTitle { get; set; }

        [Display(Name = "HipaaTitleEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string HipaaTitle { get; set; }

        [Display(Name = "PreExTitleEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PreExTitle { get; set; }

        [Display(Name = "AddressTitleEnrollment", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string AddressTitle { get; set; }
        #endregion
    }
}