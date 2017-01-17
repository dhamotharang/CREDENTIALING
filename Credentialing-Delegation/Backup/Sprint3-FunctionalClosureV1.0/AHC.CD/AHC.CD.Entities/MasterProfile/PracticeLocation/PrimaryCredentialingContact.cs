using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PrimaryCredentialingContact
    {
        public PrimaryCredentialingContact()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PrimaryCredentialingContactID { get; set; }

        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }

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
        
        [Required]
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string EmailID { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
