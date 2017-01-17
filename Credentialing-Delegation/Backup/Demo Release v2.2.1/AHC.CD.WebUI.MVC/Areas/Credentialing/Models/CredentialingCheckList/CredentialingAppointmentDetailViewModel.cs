using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHC.CD.Entities.MasterData.Enums;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingCheckList
{
    public class CredentialingAppointmentDetailViewModel
    {
        public int CredentialingAppointmentDetailID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public AppointmentProviderType? AppointmentProviderType { get; set; }

        public int? SpecialtyID { get; set; }

        public YesNoOption? BoardCertifiedYesNoOption { get; set; }

        public string RemarksForBoardCertification { get; set; }

        public YesNoOption? HospitalPrivilegesYesNoOption { get; set; }

        public string RemarksForHospitalPrivileges { get; set; }

        public YesNoOption? GapsInPracticeYesNoOption { get; set; }

        public string RemarksForGapsInPractice { get; set; }

        public YesNoOption? CleanLicenseYesNoOption { get; set; }

        public string RemarksForCleanLicense { get; set; }

        public YesNoOption? NPDBIssueYesNoOption { get; set; }

        public string RemarksForNPDBIssue { get; set; }

        public YesNoOption? MalpracticeIssueYesNoOption { get; set; }

        public string RemarksForMalpracticeIssue { get; set; }

        public string YearsInPractice { get; set; }

        public YesNoOption? SiteVisitRequiredYesNoOption { get; set; }

        public string RemarksForSiteVisitRequired { get; set; }

        public CredentialingLevel? RecommendedCredentialingLevel { get; set; }        

        public StatusType? StatusType { get; private set; }
    }
}