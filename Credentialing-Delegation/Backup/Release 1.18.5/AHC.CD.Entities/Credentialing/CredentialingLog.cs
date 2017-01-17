using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing
{
    public class CredentialingLog
    {

        public CredentialingLog()
        {
            LogDate = DateTime.Now;
        }

        public int CredentialingLogID
        {
            get;
            set;
        }

        [Column(TypeName="datetime2")]
        public DateTime? LogDate
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
        public string DoneBy
        {
            get;
            set;
        }
    }
}
