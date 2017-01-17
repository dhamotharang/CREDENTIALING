using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class InsuranceCarrierAddressViewModel
    {

        public int InsuranceCarrierAddressID { get; set; }

        public string LocationName { get; set; }

        #region Address

     
        public string Building { get; set; }

        public string Street { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        #endregion

        public StatusType? StatusType { get; set; }
        
    }
}