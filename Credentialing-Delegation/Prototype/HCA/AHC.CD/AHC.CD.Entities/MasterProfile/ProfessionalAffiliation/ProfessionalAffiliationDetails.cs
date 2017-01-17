using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.ProfessionalAffiliation
{
    public class ProfessionalAffiliationInfo
    {
        public ProfessionalAffiliationInfo()
        {
            LastModifiedDate = DateTime.Now;
        }
        public int ProfessionalAffiliationInfoID { get; set; }

        [Required]
        public string OrganizationName { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        [Required]
        public string PositionOfficeHeld { get; set; }

        [Required]
        public string Member { get; set; }

        public string Function { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

    }
}
