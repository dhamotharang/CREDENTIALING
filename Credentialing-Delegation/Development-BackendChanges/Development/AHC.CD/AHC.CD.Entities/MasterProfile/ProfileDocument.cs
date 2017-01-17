using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile
{
    public class ProfileDocument
    {
        public ProfileDocument()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int ProfileDocumentID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string DocPath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpiryDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
