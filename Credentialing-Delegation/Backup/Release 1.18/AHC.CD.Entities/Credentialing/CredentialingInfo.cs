using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing
{
    public class CredentialingInfo
    {

        public CredentialingInfo()
        {
            DateCredentialed = DateTime.Now;
            CredentialingLogs = new List<CredentialingLog>();
        }
        public int CredentialingInfoID
        {
            get;
            set;
        }

        [Column(TypeName="datetime2")]
        public DateTime? DateCredentialed
        {
            get;
            set;
        }

        public string Remarks
        {
            get;
            set;
        }

        [Required]
        public CredentialingStatus CredentialingStatus
        {
            get;
            set;
        }

        [Required]
        [ForeignKey("Plan")]
        public int PlanID { get; set; }

        public virtual Plan Plan
        {
            get;
            set;
        }

        public virtual ICollection<CredentialingLog> CredentialingLogs
        {
            get;
            
            set;
            
        }

        public AHC.CD.Entities.CredentialingType CredentialingType
        {
            get;
            set;
            
        }
    }
}
