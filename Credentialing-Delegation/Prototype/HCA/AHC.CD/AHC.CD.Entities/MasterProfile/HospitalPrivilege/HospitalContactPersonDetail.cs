using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.HospitalPrivilege
{
    public class HospitalContactPersonDetail
    {
        public HospitalContactPersonDetail()
        {
            LastModifiedDate = DateTime.Now;
        }
        public int HospitalContactPersonDetailID { get; set; }
        
        [Required]
        public string ContactPerson { get; set; }

        [Required]
        public string ContactPersonPhone { get; set; }

        [Required]
        public string ContactPersonFax { get; set; }

        [Required]
        [EmailAddress]
        public string ContactPersonEmailID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
