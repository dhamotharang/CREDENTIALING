using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.CredAxis.Models.PofessionalReferenceViewModel;
using PortalTemplate.Areas.CredAxis.Models.ProviderProfileViewModel.PofessionalReferenceViewModel;

namespace PortalTemplate.Areas.CredAxis.Models.PofessionalReferenceViewModel
{
    public class ProfessionalReferenceMainModel
    {
        public ProfessionalReferenceMainModel()
        {
            professionalReferenceDetail = new List<ProfessionalReferenceViewModel>();

            //boardDetail = new List<BoardDetailViewModel>();

        }

        public List<ProfessionalReferenceViewModel> professionalReferenceDetail { get; set; }
    }
}