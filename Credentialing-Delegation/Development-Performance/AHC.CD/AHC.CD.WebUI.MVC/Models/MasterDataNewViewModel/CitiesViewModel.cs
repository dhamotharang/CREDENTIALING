using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class CitiesViewModel
    {
        public int CountryId { get; set; }

        public int StateID { get; set; }

        public int CityID { get; set; }


        public string Code { get; set; }
       
        public string Name { get; set; }


    }
}