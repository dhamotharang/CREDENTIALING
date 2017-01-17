using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Add
{
    public class ProviderContractInfoViewModel
    {

        public AHC.CD.Entities.ProviderInfo.ContractStatus ContractStatus { get; set; }
        
        public ProviderContractDetailsViewModel ContractDetails { get; set; }

    }

    
}