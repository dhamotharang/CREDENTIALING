using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Loading
{
    public class ContractGridViewModel
    {
        public int ContractGridID { get; set; }

        public int? ProfileSpecialtyID { get; set; }

        public int? ProfilePracticeLocationID { get; set; }

        public int? LOBID { get; set; }

        public int? CredentialingInfoID { get; set; }

        public int? BusinessEntityID { get; set; }

        public DateTime? InitialCredentialingDate { get; set; }

        public CredentialingContractInfoFromPlanViewModel Report { get; set; }

        public string ReasonForPanelChange { get; set; }
    }
}