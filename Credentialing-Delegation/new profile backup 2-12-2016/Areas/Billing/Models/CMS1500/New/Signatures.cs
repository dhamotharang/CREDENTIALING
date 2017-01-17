using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Signature
    {
        public int Id { get; set; }

        public bool IsPatientsOrAuthorizedPersonsSignature { get; set; }

        public DateTime PatientsOrAuthorizedPersonsSignatureDate { get; set; }

        public bool  IsInsuredOrAuthorizedPersonsSignature { get; set; }


        public DateTime InsuredOrAuthorizedPersonsSignatureDate { get; set; }
    }
}