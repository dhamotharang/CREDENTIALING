using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.DTO
{
    public class PractitionerDTO
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NPINumber { get; set; }
        public string ProviderType { get; set; }
        public string ProviderLevel { get; set; }
    }
}
