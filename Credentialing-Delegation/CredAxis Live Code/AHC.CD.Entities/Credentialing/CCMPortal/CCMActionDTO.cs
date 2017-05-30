using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.CCMPortal
{
    public class CCMActionDTO
    {
        public int CredentialingInfoID { get; set; }

        public int CredentialingAppointmentDetailID { get; set; }

        public int ProfileID { get; set; }

        public string Name { get; set; }

        public string ProviderType { get; set; }

        public string County { get; set; }

        public string BoardCertified { get; set; }

        public string RemarksForBoardCertification { get; set; }

        public string HospitalPrivileges { get; set; }

        public string RemarksForHospitalPrivileges { get; set; }

        public string CoveringPhysicians { get; set; }

        public string GapsInPractice { get; set; }

        public string RemarksForGapsInPractice { get; set; }

        public string CleanLicense { get; set; }

        public string RemarksForCleanLicense { get; set; }

        public string NPDBIssue { get; set; }

        public string RemarksForNPDBIssue { get; set; }

        public string MalpracticeIssue { get; set; }

        public string RemarksForMalpracticeIssue { get; set; }

        public string YearsInPractice { get; set; }

        public string SiteVisitRequired { get; set; }

        public string AppointmentDate { get; set; }

        public string RemarksForSiteVisitRequired { get; set; }

        public string RecommendedLevel { get; set; }

        public string Status { get; set; }

        public string FileUploadPath { get; set; }

        public string LastModifiedDate { get; set; }

        public string AssignedToCCOID { get; set; }

        public string WelcomeLetterMailedDate { get; set; }

        public string WelcomeLetterPath { get; set; }

        public string ServiceCommencingDate { get; set; }

        public string WelcomeLetterPreparedDate { get; set; }

        public string SignaturePath { get; set; }

        public DateTime? SignedDate { get; set; }

        public string ApprovalStatus { get; set; }

        public string RemarkForApprovalStatus { get; set; }

        public string Specialty { get; set; }

        public string AssignedCCO { get; set; }

    }
}
