using PortalTemplate.Areas.CredAxis.Models.ProfessionalLiabilityViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CredAxis.Services.IServices
{
    public interface IProfessionalLiabilityService
    {
        List<ProfessionalLiabilityViewModel> GetAllProfessionalLiability();
        ProfessionalLiabilityViewModel AddEditProfessionalLiability(ProfessionalLiabilityViewModel professionalLiabilityViewModel);  
    }
}
