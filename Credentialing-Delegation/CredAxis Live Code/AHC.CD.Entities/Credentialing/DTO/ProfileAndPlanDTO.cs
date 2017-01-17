using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.DTO
{
    public class ProfileAndPlanDTO
    {
        public string NPINumber { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }



        public List<string> PlanNames { get; set; }
    }
}
