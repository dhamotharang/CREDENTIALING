using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.PSVInformation
{
    public class CredentialingVerificationInfo
    {
        public CredentialingVerificationInfo()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CredentialingVerificationInfoId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CredentialingVerificationStartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CredentialingVerificationEndDate { get; set; }

        //public int? ProfileVerificationInfoId { get; set; }
        //[ForeignKey("ProfileVerificationInfoId")]
        //public ProfileVerificationInfo ProfileVerificationInfo { get; set; }

        public int? VerifiedById { get; set; }
        [ForeignKey("VerifiedById")]
        public CDUser VerifiedBy { get; set; }

        public ICollection<CredentialingProfileVerificationDetail> ProfileVerificationDetails { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
