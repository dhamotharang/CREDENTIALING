using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Loading
{
    public class CredentialingContractInfoFromPlanViewModel
    {
        public int CredentialingContractInfoFromPlanID { get; set; }

        public string ProviderID { get; set; }

        public string GroupID { get; set; }

        public DateTime? InitiatedDate { get; set; }

        public DateTime? CredentialedDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public DateTime? ReCredentialingDate { get; set; }

        public string AdminFee { get; set; }

        public string StopLossFee { get; set; }

        public string PercentageOfRisk { get; set; }

        public string CAP { get; set; }

        public HttpPostedFileBase ContractDocument { get; set; }

        public string ContractDocumentPath { get; set; }

        public HttpPostedFileBase WelcomeLetter { get; set; }

        public string WelcomeLetterPath { get; set; }

        public OpenCloseOption? PanelStatusType { get; set; }

        public string ParticipatingStatus { get; set; }

        public ApprovalStatusType? CredentialingApprovalStatusType { get; set; }
    }
}