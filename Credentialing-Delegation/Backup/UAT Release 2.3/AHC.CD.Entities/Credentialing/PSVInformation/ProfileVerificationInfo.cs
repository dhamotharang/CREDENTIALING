using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.PSVInformation
{
    public class ProfileVerificationInfo
    {

        public ProfileVerificationInfo()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProfileVerificationInfoId { get; set; }

        public int? ProfileID { get; set; }
        [ForeignKey("ProfileID")]
        public Profile Profile { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CredentialingVerificationStartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CredentialingVerificationEndDate { get; set; }

        public int? VerifiedById { get; set; }
        [ForeignKey("VerifiedById")]
        public CDUser VerifiedBy { get; set; }

        #region ProfileVerificationStatus

        public string ProfileVerificationStatus { get; set; }

        [NotMapped]
        public ProfileVerificationStatusType? ProfileVerificationStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.ProfileVerificationStatus))
                    return null;

                if (this.ProfileVerificationStatus.Equals("Not Available"))
                    return null;

                return (ProfileVerificationStatusType)Enum.Parse(typeof(ProfileVerificationStatusType), this.ProfileVerificationStatus);
            }
            set
            {
                this.ProfileVerificationStatus = value.ToString();
            }
        }

        #endregion

        public ICollection<ProfileVerificationDetail> ProfileVerificationDetails { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
