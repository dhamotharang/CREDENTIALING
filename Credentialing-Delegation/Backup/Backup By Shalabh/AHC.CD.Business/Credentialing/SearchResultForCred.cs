using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing
{
    public class SearchResultForCred
    {
        public int ProfileID { get; set; }
        public string ProfilePhotoPath { get; set; }

        public string NPINumber { get; set; }
        public string CAQH { get; set; }
        public List<string> Titles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Specialties { get; set; }
        public List<string> IPAGroupNames { get; set; }
    }
}
