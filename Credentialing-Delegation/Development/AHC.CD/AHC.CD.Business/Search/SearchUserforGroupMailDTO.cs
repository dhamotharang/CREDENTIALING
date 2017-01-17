using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Search
{
    public class SearchUserforGroupMailDTO
    {
        public int CDuserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmailIds { get; set; }
        public string ProviderLevel { get; set; }
        public string NPINumber { get; set; }
        public List<string> IPA { get; set; }
        public List<string> Roles { get; set; }
        public string ProviderRelationship { get; set; }
        public string ProfileImagePath { get; set; }
        public string UserType { get; set; }
    }
}
