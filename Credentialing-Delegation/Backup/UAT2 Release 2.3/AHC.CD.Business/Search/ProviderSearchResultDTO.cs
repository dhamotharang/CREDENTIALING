using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Search
{
    public class ProviderSearchResultDTO
    {
        public int ProfileID { get; set; }
        public string NPINumber { get; set; }
        public List<string> Titles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> ProviderRelationships { get; set; }
        public List<string> IPAGroupNames { get; set; }
        public string ProviderLevel { get; set; }
        
    }
}
