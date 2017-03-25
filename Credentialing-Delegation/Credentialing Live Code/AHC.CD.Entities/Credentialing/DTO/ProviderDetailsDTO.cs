using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.DTO
{
  public  class ProviderDetailsDTO
    {

      public ProviderDetailsDTO()
      {
          this.PracticeLocations = new List<PracticeLocationDTO>();
          this.StateLicenseNumbers = new List<string>();
      }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string NPI { get; set; }

        public DateTime? DOB { get; set; }

        public List<PracticeLocationDTO> PracticeLocations { get; set; }

        public string Speciality { get; set; }

        public string Taxonomy { get; set; }

        public string PCPID { get; set; }

        public List<string> StateLicenseNumbers { get; set; }
        public string SLN { get; set; }
    }
}
