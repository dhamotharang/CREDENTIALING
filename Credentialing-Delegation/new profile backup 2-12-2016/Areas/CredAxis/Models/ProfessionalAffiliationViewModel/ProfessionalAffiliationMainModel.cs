using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.ProfessionalAffiliationViewModel
{
    public class ProfessionalAffiliationMainModel
    {
        public ProfessionalAffiliationMainModel()
        {
            professionalAffiliationDetail = new List<ProfessionalAffiliationViewModel>();
            //boardDetail = new List<BoardDetailViewModel>();

        }


        public List<ProfessionalAffiliationViewModel> professionalAffiliationDetail { get; set; }
    }
}