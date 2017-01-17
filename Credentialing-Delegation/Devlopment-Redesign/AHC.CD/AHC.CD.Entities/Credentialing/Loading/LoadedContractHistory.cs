using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing.Loading
{
    public class LoadedContractHistory
    {
        public LoadedContractHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int LoadedContractHistoryID { get; set; }

        #region Loader

        public int? LoadedByID { get; set; }
        [ForeignKey("LoadedByID")]
        public CDUser Loader { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LoadedDate { get; set; }

        #endregion

        #region Credentialing Type

        public string Credentialing { get; private set; }

        public CredentialingType? CredentialingType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Credentialing))
                    return null;

                if (this.Credentialing.Equals("Not Available"))
                    return null;

                return (CredentialingType)Enum.Parse(typeof(CredentialingType), this.Credentialing);
            }
            set
            {
                this.Credentialing = value.ToString();
            }
        }

        #endregion

        #region Business Entity

        public int BusinessEntityID { get; set; }
        [ForeignKey("BusinessEntityID")]
        public OrganizationGroup BusinessEntity { get; set; }

        #endregion

        #region Line Of Business

        public int LOBID { get; set; }
        [ForeignKey("LOBID")]
        public LOB LOB { get; set; }

        #endregion

        #region Specialty

        public int SpecialtyID { get; set; }
        [ForeignKey("SpecialtyID")]
        public Specialty Specialty { get; set; }

        #endregion

        public virtual LoadedLocation Location { get; set; }

        public virtual Docket Docket { get; set; }

        public PlanReportHistory PlanReportHistory { get; set; }

        #region Credentialing Request Status

        public string CredentialingRequestStatus { get; private set; }

        [NotMapped]
        public CredentialingRequestStatusType? CredentialingRequestStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.CredentialingRequestStatus))
                    return null;

                if (this.CredentialingRequestStatus.Equals("Not Available"))
                    return null;

                return (CredentialingRequestStatusType)Enum.Parse(typeof(CredentialingRequestStatusType), this.CredentialingRequestStatus);
            }
            set
            {
                this.CredentialingRequestStatus = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
