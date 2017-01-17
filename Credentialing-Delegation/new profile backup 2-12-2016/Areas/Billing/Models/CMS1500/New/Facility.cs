using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
    

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Facility
    {
        [Key]
        public long Facility_PK_Id { get; set; }
        public string FacilityName { get; set; }
        public string FacilityCode { get; set; }
        public Nullable<long> FacilityAddress_FK_Id { get; set; }

        public virtual Address Address { get; set; }
    }
}
