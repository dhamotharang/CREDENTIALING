using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProviderInfo
{
    public class OrganizationDetail
    {
        public int OrganizationDetailID { get; set; }
        public ProviderRelationship ProviderRelationship { get; set; }
        public bool PartOfGroup { get; set; }
        public string GroupName { get; set; }
        public string CVFilePath { get; set; }
        public string ContractFilePath { get; set; }
    }

    public enum ProviderRelationship
    {
        Employee=1,
        Affiliate
    }
}
