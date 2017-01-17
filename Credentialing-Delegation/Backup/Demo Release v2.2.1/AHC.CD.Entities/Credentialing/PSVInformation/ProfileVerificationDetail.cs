using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.PSVInformation
{
    public class ProfileVerificationDetail
    {
        public ProfileVerificationDetail()
        {
            LastModifiedDate = DateTime.Now;
            VerificationDate = DateTime.Now;
        }

        public int ProfileVerificationDetailId { get; set; }

        public int? ProfileVerificationParameterId { get; set; }
        [ForeignKey("ProfileVerificationParameterId")]
        public ProfileVerificationParameter ProfileVerificationParameter { get; set; }

        public int? VerificationResultId { get; set; }
        [ForeignKey("VerificationResultId")]
        public VerificationResult VerificationResult { get; set; }

        public string VerificationData { get; set; }

        public int? VerifiedById { get; set; }
        [ForeignKey("VerifiedById")]
        public CDUser VerifiedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? VerificationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
