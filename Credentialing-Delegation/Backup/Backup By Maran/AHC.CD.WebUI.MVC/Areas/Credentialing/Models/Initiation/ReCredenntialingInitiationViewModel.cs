using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Initiation
{
    public class ReCredenntialingInitiationViewModel
    {
        public int CredentialingInitiationInfoID { get; set; }

        public int ProfileID { get; set; }

        public int? PlanID { get; set; }

        public DateTime? InitiationDate { get; set; }

        public int? InitiatedByID { get; set; }

        public YesNoOption? IsDelegatedYesNoOption { get; set; }

        public StatusType? StatusType { get; set; }

        public ICollection<CredentialingContractRequestViewModel> CredentialingContractRequests { get; set; }

    }
}