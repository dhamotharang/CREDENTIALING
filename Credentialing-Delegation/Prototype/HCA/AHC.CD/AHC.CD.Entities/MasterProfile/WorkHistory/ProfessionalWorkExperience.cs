using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.WorkHistory
{
    public class ProfessionalWorkExperience
    {
        public ProfessionalWorkExperience()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int ProfessionalWorkExperienceID { get; set; }

        public EmployerDetail EmployerDetail { get; set; }

        public WorkAddress WorkAddress { get; set; }

        public OtherWorkInfo OtherWorkInfo { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
