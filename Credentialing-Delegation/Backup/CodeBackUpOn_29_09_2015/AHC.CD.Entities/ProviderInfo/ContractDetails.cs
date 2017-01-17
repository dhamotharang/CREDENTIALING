using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProviderInfo
{
    public class ContractDetail
    {
        public ContractDetail()
        {
            LastUpdatedDateTime = DateTime.Now;
        }
        public int ContractDetailID {get;set;}

        public DateTime TransactionDate{get;set;}

        public string Remarks{get;set;}

        [Column(TypeName = "datetime2")]
        public DateTime? LastUpdatedDateTime{get;set;}

        public virtual ContractStatus ContractStatus{get;set;}

        public virtual MailTemplate MailTemplate{get;set;}
    }
}
