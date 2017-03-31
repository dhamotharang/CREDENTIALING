using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.DTO
{
    public class CredentialingInfoDTO
    {
        public int CredentialingInfoID { get; set; }

        public string Insurance { get; set; }

        public string Lob { get; set; }

        public string ParticipatingStatus { get; set; }

        public string GroupID { get; set; }

        public string IndividualID { get; set; }

        public string EffectiveDate { get; set; }

        public string TerminationDate { get; set; }
    }
}
