using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Tables
{
    public class LocationDetail
    {
        public int LocationDetailID { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }

        public string CountryCode { get; set; }
        
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        
    }
}
