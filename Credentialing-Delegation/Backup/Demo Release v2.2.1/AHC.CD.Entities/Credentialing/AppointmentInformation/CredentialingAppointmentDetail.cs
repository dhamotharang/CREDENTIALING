using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;

namespace AHC.CD.Entities.Credentialing.AppointmentInformation
{
    public class CredentialingAppointmentDetail
    {
        public CredentialingAppointmentDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CredentialingAppointmentDetailID { get; set; }

        #region Name

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        #endregion

        #region Provider Type

        public string ProviderType { get; set; }

        [NotMapped]
        public AppointmentProviderType? AppointmentProviderType
        {
            get
            {
                if (String.IsNullOrEmpty(this.ProviderType))
                    return null;

                if (this.ProviderType.Equals("Not Available"))
                    return null;

                return (AppointmentProviderType)Enum.Parse(typeof(AppointmentProviderType), this.ProviderType);
            }
            set
            {
                this.ProviderType = value.ToString();
            }
        }

        #endregion

        #region Specialty Information

        public int? SpecialtyID { get; set; }
        [ForeignKey("SpecialtyID")]
        public Specialty Specialty { get; set; }

        #region Board Certified Yes Or No

        public string BoardCertified { get; set; }

        [NotMapped]
        public YesNoOption? BoardCertifiedYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.BoardCertified))
                    return null;

                if (this.BoardCertified.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.BoardCertified);
            }
            set
            {
                this.BoardCertified = value.ToString();
            }
        }

        #endregion

        public string RemarksForBoardCertification { get; set; }

        #endregion

        #region Hospital Privileges

        #region Hospital Privileges Yes Or No

        public string HospitalPrivileges { get; set; }

        [NotMapped]
        public YesNoOption? HospitalPrivilegesYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.HospitalPrivileges))
                    return null;

                if (this.HospitalPrivileges.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.HospitalPrivileges);
            }
            set
            {
                this.HospitalPrivileges = value.ToString();
            }
        }

        #endregion

        public string RemarksForHospitalPrivileges { get; set; }

        #endregion

        public ICollection<CredentialingCoveringPhysician> CredentialingCoveringPhysicians { get; set; }

        #region Gaps In Practice

        #region Gaps In Practice Yes Or No

        public string GapsInPractice { get; set; }

        [NotMapped]
        public YesNoOption? GapsInPracticeYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.GapsInPractice))
                    return null;

                if (this.GapsInPractice.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.GapsInPractice);
            }
            set
            {
                this.GapsInPractice = value.ToString();
            }
        }

        #endregion

        public string RemarksForGapsInPractice { get; set; }

        #endregion

        #region Clean License

        #region Clean License Yes Or No

        public string CleanLicense { get; set; }

        [NotMapped]
        public YesNoOption? CleanLicenseYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.CleanLicense))
                    return null;

                if (this.CleanLicense.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.CleanLicense);
            }
            set
            {
                this.CleanLicense = value.ToString();
            }
        }

        #endregion

        public string RemarksForCleanLicense { get; set; }

        #endregion

        #region NPDB Issue

        #region NPDB Issue Yes Or No

        public string NPDBIssue { get; set; }

        [NotMapped]
        public YesNoOption? NPDBIssueYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.NPDBIssue))
                    return null;

                if (this.NPDBIssue.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.NPDBIssue);
            }
            set
            {
                this.NPDBIssue = value.ToString();
            }
        }

        #endregion

        public string RemarksForNPDBIssue { get; set; }

        #endregion

        #region Malpractice Issue

        #region Malpractice Issue Yes Or No

        public string MalpracticeIssue { get; set; }

        [NotMapped]
        public YesNoOption? MalpracticeIssueYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.MalpracticeIssue))
                    return null;

                if (this.MalpracticeIssue.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.MalpracticeIssue);
            }
            set
            {
                this.MalpracticeIssue = value.ToString();
            }
        }

        #endregion

        public string RemarksForMalpracticeIssue { get; set; }

        #endregion

        public string YearsInPractice { get; set; }

        #region Site Visit Required

        #region Site Visit Required Yes Or No

        public string SiteVisitRequired { get; set; }

        [NotMapped]
        public YesNoOption? SiteVisitRequiredYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.SiteVisitRequired))
                    return null;

                if (this.SiteVisitRequired.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.SiteVisitRequired);
            }
            set
            {
                this.SiteVisitRequired = value.ToString();
            }
        }

        #endregion

        public string RemarksForSiteVisitRequired { get; set; }

        #endregion

        #region Recommended Level

        public string RecommendedLevel { get; set; }

        [NotMapped]
        public CredentialingLevel? RecommendedCredentialingLevel
        {
            get
            {
                if (String.IsNullOrEmpty(this.RecommendedLevel))
                    return null;

                if (this.RecommendedLevel.Equals("Not Available"))
                    return null;

                return (CredentialingLevel)Enum.Parse(typeof(CredentialingLevel), this.RecommendedLevel);
            }
            set
            {
                this.RecommendedLevel = value.ToString();
            }
        }

        #endregion

        public CredentialingAppointmentSchedule CredentialingAppointmentSchedule { get; set; }

        public CredentialingAppointmentResult CredentialingAppointmentResult { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
