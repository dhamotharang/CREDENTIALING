using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.LoadingInformation
{
    public class CredentialingContractInfoFromPlan
    {
        public CredentialingContractInfoFromPlan()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CredentialingContractInfoFromPlanID { get; set; }

        public string ProviderID { get; set; }

        public string GroupID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? InitiatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CredentialedDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? TerminationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ReCredentialingDate { get; set; }

        public string AdminFee { get; set; }

        public string StopLossFee { get; set; }

        public string PercentageOfRisk { get; set; }

        public string CAP { get; set; }

        public string ContractDocumentPath { get; set; }

        public string WelcomeLetterPath { get; set; }

        #region Panel Status

        public string PanelStatus { get; private set; }

        [NotMapped]
        public OpenCloseOption? PanelStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.PanelStatus))
                    return null;

                if (this.PanelStatus.Equals("Not Available"))
                    return null;

                return (OpenCloseOption)Enum.Parse(typeof(OpenCloseOption), this.PanelStatus);
            }
            set
            {
                this.PanelStatus = value.ToString();
            }
        }

        #endregion

        #region Credentialing Approval Status

        public string CredentialingApprovalStatus { get; private set; }

        [NotMapped]
        public ApprovalStatusType? CredentialingApprovalStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.CredentialingApprovalStatus))
                    return null;

                if (this.CredentialingApprovalStatus.Equals("Not Available"))
                    return null;

                return (ApprovalStatusType)Enum.Parse(typeof(ApprovalStatusType), this.CredentialingApprovalStatus);
            }
            set
            {
                this.CredentialingApprovalStatus = value.ToString();
            }
        }

        #endregion

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
