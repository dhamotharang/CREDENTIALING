using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.DTO
{

   public class ProviderDTOForUM
    {
       public ProviderDTOForUM()
       {
           this.ProviderPracticeLocationAddress = new List<LocationsDTO>();
       }
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
       
        public string MiddleName { get; set; }
      

        public string Practitioner_Type { get; set; }

        public string NPI { get; set; }

        public string Gender{ get; set; }

        public string Languages { get; set; }

        public List<LocationsDTO> ProviderPracticeLocationAddress { get; set; }
    }

    public class LocationsDTO
    {
        public LocationsDTO()
        {
            this.Facility = new List<FacilityDTO>();
        }
        public List<FacilityDTO> Facility { get; set; } 

        public string Location { get; set; }
    }
    public class FacilityDTO
        {
        public FacilityDTO()
        {
            this.Speciality = new List<SpecialtyDTO>();

        }
        public string In_Directory { get; set; }

            public string Accepting_Patients { get; set; }

         //   public bool Accepting_Medicaie { get; set; }

            public string Group_Tax_ID { get; set; }

            public string Address { get; set; }

            public string City { get; set; }

            public string County { get; set; }

            public string State { get; set; }

            public string Zip { get; set; }

            public string Phone { get; set; }

            public string Fax { get; set; }


            public List<SpecialtyDTO> Speciality { get; set; }

        }
    public class SpecialtyDTO
    {
        public string Specialty { get; set; }

        public string Practice_As { get; set; }
    }
}
