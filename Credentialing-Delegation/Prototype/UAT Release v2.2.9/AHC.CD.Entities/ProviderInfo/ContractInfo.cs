using System.Collections.Generic;

namespace AHC.CD.Entities.ProviderInfo
{
    public class ContractInfo
    {
        public  int ContractInfoID
        {
            get;
            set;
        }

        public virtual ContractStatus CurrentStatus
        {
            get;
            set;
        }

        public virtual ICollection<ContractDetail> ContractDetails
        {
            get;
            set;
        }

        
    }
}
