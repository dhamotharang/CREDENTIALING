using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.IdentificationAndLicenses
{
    public class FederalDEAInfoHistory
    {
        public FederalDEAInfoHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int FederalDEAInfoHistoryID { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpiryDate { get; set; }

        public ICollection<DEAScheduleInfoHistory> DEAScheduleInfoHistory { get; set; }

        public string FederalDEADocumentPath { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
