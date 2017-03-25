using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Add
{
    public class ProviderContractDetailsViewModel
    {
        public DateTime LastUpdatedDateTime { get; set; }
        public string Remarks { get; set; }
        public DateTime TransactionDate { get; set; }
        public AHC.CD.Entities.ProviderInfo.ContractStatus ContractStatus { get; set; }
        public MailTemplateViewModel MailTemplate { get; set; }
    }
}