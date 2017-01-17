using PortalTemplate.Areas.CredAxis.Models.EducationViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CredAxis.Services.IServices
{
     public interface IEducationServices
    {
         EducationMainViewModel GetAllEducations();
        List<EducationMainViewModel> AddEditEducations(EducationMainViewModel educationsCode);
    }
}
