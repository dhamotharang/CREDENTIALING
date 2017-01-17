using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Provider
    {
        [Key]
        public long ProviderID { get; set; }
        public string ProviderCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<long> ContactInfo_FK_Id { get; set; }
        public string NPI { get; set; }
        public string TaxonomyCode { get; set; }
        public string FederalTaxNumber { get; set; }
        public string SSN { get; set; }
        public Nullable<long> Facility_FK_Id { get; set; }

        [ForeignKey("ContactInfo_FK_Id")]
        public virtual ContactInfo ContactInfo { get; set; }
        [ForeignKey("Facility_FK_Id")]
        public virtual Facility Facility { get; set; }
    }
}