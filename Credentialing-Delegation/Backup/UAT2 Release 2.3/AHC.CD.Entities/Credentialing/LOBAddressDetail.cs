using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing
{
    public class LOBAddressDetail
    {
        public LOBAddressDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int LOBAddressDetailID { get; set; } 

        public string Street { get; set; }

        public string Appartment { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string County { get; set; }

        public string ZipCode { get; set; }

        public bool IsPrimary { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
