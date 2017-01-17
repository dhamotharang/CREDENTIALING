using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.ProfessionalLiability
{
    public class ProfessionalLiabilityInsurance
    {
        public ProfessionalLiabilityInsurance()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int ProfessionalLiabilityInsuranceID { get; set; }

        public InsuranceInfo InsuranceInfo { get; set; }
        public InsuranceAddress InsuranceAddress { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
