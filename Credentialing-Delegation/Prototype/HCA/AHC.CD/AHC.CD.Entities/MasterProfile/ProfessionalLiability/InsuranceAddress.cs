using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.ProfessionalLiability
{
    public class InsuranceAddress
    {
        public InsuranceAddress()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int InsuranceAddressID { get; set; }
        [Required]
        public string Number { get; set; }

        [Required]
        public string Building { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string County { get; set; }

        [Required]
        public string Country { get; set; }
        
        [Required]
        public string Zipcode { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
