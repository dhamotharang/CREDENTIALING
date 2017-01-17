using PortalTemplate.Areas.CredAxisProduct.Models.ProviderProfileViewModel.WorkHistoryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.WorkHistoryViewModel
{
    public class WorkHistoryMainViewModel
    {
        public WorkHistoryMainViewModel()
        {
            militaryService = new List<MilitaryServiceViewModel>();
            workGap = new List<WorkGapViewModel>();
            professionalWorkExperience = new List<ProfessionalWorkExperienceViewModel>();
            publicHealthService = new List<PublicHealthServicesViewModel>();


        }
        public List<MilitaryServiceViewModel> militaryService { get; set; }
        public List<WorkGapViewModel> workGap { get; set; }
        public List<ProfessionalWorkExperienceViewModel> professionalWorkExperience { get; set; }
        public List<PublicHealthServicesViewModel> publicHealthService { get; set; }
    }
}