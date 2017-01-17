using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.ProfessionalLiability
{
    public class ProfessionalLiabilityInfoHistory
    {
        public ProfessionalLiabilityInfoHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProfessionalLiabilityInfoHistoryID { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpiryDate { get; set; }

        public string InsuranceCertificatePath { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
