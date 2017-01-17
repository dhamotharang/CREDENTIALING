using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.IdentificationAndLicenses
{
    public class StateLicenseInfoHistory
    {
        public StateLicenseInfoHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int StateLicenseInfoHistoryID { get; set; }

        [Column(TypeName = "datetime2")]
       [Required]
        public DateTime CurrentIssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpiryDate { get; set; }

        public string StateLicenseDocumentPath { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
