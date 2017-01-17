using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Search
{
    public class ProviderSearchRequestDTO
    {
        public string NPINumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProviderRelationship { get; set; }
        public string IPAGroupName { get; set; }
    }
}
