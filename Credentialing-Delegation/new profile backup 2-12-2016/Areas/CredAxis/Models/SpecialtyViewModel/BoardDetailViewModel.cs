using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.SpecialtyViewModel
{
    public class BoardDetailViewModel
    {

        [Key]
        public int? BoardDetailId { get; set; }


        [Display(Name = "BOARD CERTIFIED")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool BoardCertified { get; set; }

        [Display(Name = "BOARD NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string BoardName { get; set; }

        [Display(Name = "CERTIFICATE NUMBER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CertificateNumber { get; set; }

        [Display(Name = "CEWRTIFICATION DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CertificationDate { get; set; }

        [Display(Name = "RECERTIFICATION DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ReCertificationDate { get; set; }

        [Display(Name = "EXPIRATION DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ExpirationDate { get; set; }


    }
}