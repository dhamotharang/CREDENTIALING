using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalTemplate.Areas.CredAxis.Models.ProfessionalLiabilityViewModel;
using PortalTemplate.Areas.CredAxis.Models.ProfessionalAffiliationViewModel;

namespace PortalTemplate.Areas.CredAxis.Services.IServices
{
    public interface IProfessionalAffiliationService
    {
        List<ProfessionalAffiliationViewModel> GetAllProfessionalAffiliationCode();
        ProfessionalAffiliationViewModel AddEditProfessionalAffiliationCode(ProfessionalAffiliationMainModel professionalAffiliationViewModel);
        
    }
}
