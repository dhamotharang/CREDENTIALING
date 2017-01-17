using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.MasterData.Enums;
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

        public StatusType? StatusType { get; set; }

        public ICollection<LoadedContract> LoadedContracts { get; set; }


    }
}