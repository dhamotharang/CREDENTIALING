using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
//using Foolproof;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Loading
{
    public class CredentialingContractInfoFromPlanViewModel
    {
        public int CredentialingContractInfoFromPlanID { get; set; }

       //[RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid Provider Id")]
        public string ProviderID { get; set; }

        public string GroupID { get; set; }

        public DateTime? InitiatedDate { get; set; }

        public DateTime? CredentialedDate { get; set; }

      // [DateEnd(DateStartProperty = "InitiatedDate", MaxYear = "50", IsRequired = false, ErrorMessage = "Termination Date should be greater than Initiation date")]
      //[GreaterThan("InitiatedDate")]

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