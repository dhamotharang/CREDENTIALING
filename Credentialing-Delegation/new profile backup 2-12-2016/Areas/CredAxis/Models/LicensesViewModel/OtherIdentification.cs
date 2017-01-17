using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.LicensesViewModel
{
    public class OtherIdentification
    {
        [Key]
        public int? OtherIdentificationID { get; set; }

        [Display(Name = "NPI #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string NPIID { get; set; }

        [Display(Name = "NPI USER NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string NPIUsername { get; set; }

        [Display(Name = "NPI PASSWORD")]
        [DisplayFormat(NullDisplayText = "-")]
        public string NPIPassword { get; set; }

        [Display(Name = "CAQH #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CAQH { get; set; }

        [Display(Name = "CAQH USER NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CAQHUsername { get; set; }

        [Display(Name = "CAQH PASSWORD")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CAQHPassword { get; set; }

        [Display(Name = "UPIN #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string UPIN { get; set; }

        [Display(Name = "USMLE #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string USMLE { get; set; }


        [Display(Name = "LAST CAQH ATTESTATION DATE :")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastCAQHAttestationDate { get; set; }

        [Display(Name = "NEXT ATTESTATION DATE ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string NextAttestationDate { get; set; }
    }
}