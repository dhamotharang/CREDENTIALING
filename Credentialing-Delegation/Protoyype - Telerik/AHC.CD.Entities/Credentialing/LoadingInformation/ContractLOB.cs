using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.LoadingInformation
{
    public class ContractLOB
    {
        public ContractLOB()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ContractLOBID { get; set; }

        public int? LOBID { get; set; }
        [ForeignKey("LOBID")]
        public LOB LOB { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
