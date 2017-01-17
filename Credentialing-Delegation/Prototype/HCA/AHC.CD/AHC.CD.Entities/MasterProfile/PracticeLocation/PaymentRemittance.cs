using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PaymentRemittance
    {
        public PaymentRemittance()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int PaymentRemittanceID { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public PracticeAddress PracticeAddress { get; set; }

        [Required]
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string EmailID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
