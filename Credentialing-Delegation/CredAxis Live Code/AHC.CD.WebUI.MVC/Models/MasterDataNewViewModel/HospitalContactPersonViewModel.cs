using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class HospitalContactPersonViewModel
    {

        
        public int HospitalContactPersonID { get; set; }

        //[Required]
        public string ContactPersonName { get; set; }

        #region Phone Number

        public string ContactPersonPhone{ get; set; }
    
        public string Phone { get; set; }
   
        public string PhoneCountryCode { get; set; }

        #endregion

        #region Fax Number

        public string ContactPersonFax { get; set; }
      
        public string Fax { get; set; }
 
        public string FaxCountryCode { get; set; }

        #endregion

        public StatusType? StatusType { get; set; }        
       
    }
}