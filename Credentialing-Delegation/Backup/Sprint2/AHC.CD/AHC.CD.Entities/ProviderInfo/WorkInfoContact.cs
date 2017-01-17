using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.ProviderInfo
{
    public class WorkInfoContact
    {
        public int WorkInfoContactID { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Email { get; set; }
        public long FaxNo { get; set; }
        public long PhoneNo { get; set; }
        public string State { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }

    }
}
