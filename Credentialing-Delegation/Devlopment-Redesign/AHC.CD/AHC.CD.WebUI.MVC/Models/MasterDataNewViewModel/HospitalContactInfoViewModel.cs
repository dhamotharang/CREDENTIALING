using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class HospitalContactInfoViewModel
    {
        public int HospitalID;
        public int HospitalContactInfoID { get; set; }

        public string LocationName { get; set; }

        public StatusType? StatusType { get; set; }

        #region Address

        //[Required]
        public string UnitNumber { get; set; }

        //[Required]
        public string Street { get; set; }

        //[Required]
        public string Country { get; set; }

        //[Required]
        public string State { get; set; }

        public string County { get; set; }

        //[Required]
        public string City { get; set; }

        public string ZipCode { get; set; }

        #endregion

        #region Phone Number

        public string PhoneNumber { get; set; }

        public string PhoneCountryCode { get; set; }

        #endregion

        #region Fax Number

        public string Fax { get; set; }

        public string FaxNumber { get; set; }

        public string FaxCountryCode { get; set; }

        #endregion

        public string Email { get; set; }

        public HospitalContactPersonViewModel HospitalContactPersonViewModel { get; set; }
    }
}