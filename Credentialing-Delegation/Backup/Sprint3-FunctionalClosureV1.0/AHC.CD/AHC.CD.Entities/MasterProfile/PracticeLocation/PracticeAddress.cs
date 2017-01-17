using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PracticeAddress
    {
        public PracticeAddress()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PracticeAddressID { get; set; }
        
        #region Address

        [Required]
        public string Building { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        public string County { get; set; }

        [Required]
        public string City { get; set; }

        public string ZipCode { get; set; }

        #endregion        

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
