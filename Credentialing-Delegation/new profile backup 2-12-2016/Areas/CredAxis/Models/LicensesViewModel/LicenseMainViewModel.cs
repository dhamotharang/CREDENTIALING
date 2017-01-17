using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.LicensesViewModel
{
    public class LicenseMainViewModel
    {
        public LicenseMainViewModel()
        {
            stateLicense = new List<StateLicense>();
            federalDEA = new List<FederalDea>();
            mediciad = new List<MedicaidInfo>();
            medicare = new List<MedicareInfo>();
            CDS = new List<CdsInfo>();
            otherIdentification = new List<OtherIdentification>();

        }
        public List<StateLicense> stateLicense { get; set; }
        public List<FederalDea> federalDEA { get; set; }
        public List<MedicaidInfo> mediciad { get; set; }
        public List<MedicareInfo> medicare { get; set; }
        public List<CdsInfo> CDS { get; set; }
        public List<OtherIdentification> otherIdentification { get; set; }
    }
}