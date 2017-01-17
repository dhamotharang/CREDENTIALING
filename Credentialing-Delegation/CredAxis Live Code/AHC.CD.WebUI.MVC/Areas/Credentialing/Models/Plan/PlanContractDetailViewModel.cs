using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Plan
{
    public class PlanContractDetailViewModel
    {
        public int? PlanContractDetailID { get; set; }

        public int LOBID { get; set; }

        public LOBContactDetailViewModel ContactDetail { get; set; }

        public LOBAddressDetailViewModel AddressDetail { get; set; }

        public ICollection<int> BEs { get; set; }   

        #region Status

        public StatusType? StatusType { get; set; }

        #endregion  
    }
}
