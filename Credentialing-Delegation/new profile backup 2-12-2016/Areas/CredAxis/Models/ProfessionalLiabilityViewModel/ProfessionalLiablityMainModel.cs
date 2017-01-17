using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.ProfessionalLiabilityViewModel
{
    public class ProfessionalLiablityMainModel

    {
        public ProfessionalLiablityMainModel()
        {
            ProfLiability = new List<ProfessionalLiablityMainModel>();            
        }
        public List<ProfessionalLiablityMainModel> ProfLiability { get; set; }
    }
}