using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterProfile;

namespace AHC.CD.Entities.Credentialing.PSV
{
    public class PSVDetail
    {
        public PSVDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PSVDetailID { get; set; }

        public int? ProfileID { get; set; }
        [ForeignKey("ProfileID")]
        public Profile Profile { get; set; }

        #region Verification Status

        public string VerificationStatus { get; private set; }

        [NotMapped]
        public VerificationResultStatusType? VerificationStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.VerificationStatus))
                    return null;

                if (this.VerificationStatus.Equals("Not Available"))
                    return null;

                return (VerificationResultStatusType)Enum.Parse(typeof(VerificationResultStatusType), this.VerificationStatus);
            }
            set
            {
                this.VerificationStatus = value.ToString();
            }
        }

        #endregion

        public string Remark { get; set; }

        public string VerificationSource { get; set; }

        public string VerificationDocumentPath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastVerifiedDate { get; set; }

        public string DataVerified { get; set; }

        public int? PSVMasterID { get; set; }
        [ForeignKey("PSVMasterID")]
        public PSVMaster PSVMaster { get; set; }

        public ICollection<PSVDetailHistory> PSVDetailHistory { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
