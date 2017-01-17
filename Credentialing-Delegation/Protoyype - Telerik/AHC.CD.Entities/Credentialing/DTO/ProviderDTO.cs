using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.Entities.Credentialing.DTO
{
    public class ProviderDTO
    {
        public string NPI { get; set; }

        public string SSN { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Type { get; set; }

        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public string ContactName { get; set; }

        public string Speciality { get; set; }
    }
}