using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.HospitalPrivilege
{
    public class HospitalAddress
    {

        public HospitalAddress()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int HospitalAddressID { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        public string Number { get; set; }
        public string Building { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string Country { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

    }
}

