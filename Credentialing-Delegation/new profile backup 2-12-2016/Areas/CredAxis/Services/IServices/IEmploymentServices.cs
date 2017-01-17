using PortalTemplate.Areas.CredAxis.Models.EmploymentInformationVieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CredAxis.Services.IServices
{
    public interface IEmploymentServices
    {
        //EmploymentInformationMainViewModel GetAllEmploymentInformations(int ProfileId);
        ////EmploymentInformationMainViewModel AddEditEmploymentInformations(EmploymentInformationMainViewModel employmentInformations);
        //EmploymentInformationMainViewModel AddEditEmploymentInformations(EmploymentInformationMainViewModel employmentInformations, int ProfileId,int EmploymentInfoId);
        EmploymentInformationMainViewModel GetAllEmploymentInformations();
        EmploymentInformationMainViewModel AddEditEmploymentInformations(EmploymentInformationMainViewModel employmentInformations);
    }
}
