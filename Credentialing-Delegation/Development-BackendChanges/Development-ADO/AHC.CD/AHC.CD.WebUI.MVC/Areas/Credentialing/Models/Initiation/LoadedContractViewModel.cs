using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Initiation
{
    public class LoadedContractViewModel
    {
        public int LoadedContractID { get; set; }

        public int? LoadedByID { get; set; }

        public DateTime? LoadedDate { get; set; }

        public CredentialingType? CredentialingType { get; set; }

        public int BusinessEntityID { get; set; }

        public int SpecialtyID { get; set; }

        public CredentialingRequestStatusType? CredentialingRequestStatusType { get; set; }

        public int LOBID { get; set; }
    }
}