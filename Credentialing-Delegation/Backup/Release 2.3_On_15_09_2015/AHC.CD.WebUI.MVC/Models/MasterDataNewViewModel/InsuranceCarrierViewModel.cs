using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class InsuranceCarrierViewModel
    {

        public int InsuranceCarrierID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public StatusType? StatusType { get; set; }

        public InsuranceCarrierAddressViewModel InsuranceCarrierAddress { get; set; }
    }
}