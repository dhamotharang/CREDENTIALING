using PortalTemplate.Areas.CredAxis.Models.ProviderProfileViewModel.EmploymentInformationVieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.EmploymentInformationVieModel
{
    public class EmploymentInformationMainViewModel
    {
        public EmploymentInformationMainViewModel()
        {
            employmentInformation = new EmploymentInformationViewModel();
            groupInformation = new List<GroupInformationViewModel>();
        }
        public EmploymentInformationViewModel employmentInformation { get; set; }
        public List<GroupInformationViewModel> groupInformation { get; set; }

    }
}