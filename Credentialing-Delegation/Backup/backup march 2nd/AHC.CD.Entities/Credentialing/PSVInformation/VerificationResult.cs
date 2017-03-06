using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.PSVInformation
{
    public class VerificationResult
    {
        public VerificationResult()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int VerificationResultId { get; set; }

        public string Remark { get; set; }

        public string Source { get; set; }

        [NotMapped]
        public long VerificationDocumentSize { get; set; }

        public string VerificationDocumentPath { get; set; }

        #region VerificationResultStatus

        public string VerificationResultStatus { get; set; }

        [NotMapped]
        public VerificationResultStatusType? VerificationResultStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.VerificationResultStatus))
                    return null;

                if (this.VerificationResultStatus.Equals("Not Available"))
                    return null;

                return (VerificationResultStatusType)Enum.Parse(typeof(VerificationResultStatusType), this.VerificationResultStatus);
            }
            set
            {
                this.VerificationResultStatus = value.ToString();
            }
        }

        #endregion        

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
