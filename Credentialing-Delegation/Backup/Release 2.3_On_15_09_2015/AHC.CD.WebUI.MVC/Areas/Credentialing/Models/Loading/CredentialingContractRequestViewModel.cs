using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Loading
{
    public class CredentialingContractRequestViewModel
    {
        public int CredentialingContractRequestID { get; set; }

        public DateTime? InitialCredentialingDate { get; set; }

        public ICollection<ContractSpecialtyViewModel> ContractSpecialties { get; set; }

        public YesNoOption? AllSpecialtiesSelectedYesNoOption { get; set; }

        public ICollection<ContractPracticeLocationViewModel> ContractPracticeLocations { get; set; }

        public YesNoOption? AllPracticeLocationsSelectedYesNoOption { get; set; }

        public ICollection<ContractLOBViewModel> ContractLOBs { get; set; }

        public YesNoOption? AllLOBsSelectedYesNoOption { get; set; }

        public int? BusinessEntityID { get; set; }

        public ICollection<ContractGridViewModel> ContractGrid { get; set; }

        public StatusType? StatusType { get; set; }

        public StatusType? ContractRequestStatusType { get; set; }
    }
}