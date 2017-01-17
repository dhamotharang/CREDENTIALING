using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileDemographicInfo
{
    public class VisaInformation
    {
        public int VisaInformationID { get; set; }
        public string CountryOfIssue { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime VisaExpiryDate { get; set; }
        public string VisaNumber { get; set; }
        public string VisaSponsor { get; set; }
        public string VisaStatus { get; set; }
        public VisaType VisaType { get; set; }
    }
}
