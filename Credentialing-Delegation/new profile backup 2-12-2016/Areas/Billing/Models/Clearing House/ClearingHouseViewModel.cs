using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.Clearing_House
{
    public class ClearingHouseViewModel
    {
        [DisplayName("Clearing House ID")]
        public string ClearingHouseId { get; set; }

        [DisplayName("Clearing House Name")]
        public string ClearingHouseName { get; set; }

        [DisplayName("Clearing House Address Line1")]
        public string ClearingHouseAddressLine1 { get; set; }

        [DisplayName("Clearing House Address Line2")]
        public string ClearingHouseAddressLine2 { get; set; }

        [DisplayName("Clearing House City")]
        public string ClearingHouseCity { get; set; }

        [DisplayName("Clearing House State")]
        public string ClearingHouseState { get; set; }

        [DisplayName("Clearing House Zip")]
        public string ClearingHouseZip { get; set; }


        public string PayersCount { get; set; }

        [DisplayName("Clearing House Payers")]
        public List<PayerViewModel> ClearingHousePayers{ get; set; }
    }
}