using PortalTemplate.Areas.CredAxis.Models.PofessionalReferenceViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalTemplate.Areas.CredAxis.Models.ProviderProfileViewModel.PofessionalReferenceViewModel;

namespace PortalTemplate.Areas.CredAxis.Services.IServices
{
    interface IProfessionalReferencesService
    {
        List<ProfessionalReferenceViewModel> GetAllProfessionalRef();
        ProfessionalReferenceMainModel AddEditProfessionalRef(ProfessionalReferenceViewModel professionalReferenceViewModel);  
    }
}
