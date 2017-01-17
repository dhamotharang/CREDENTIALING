using PortalTemplate.Areas.CredAxis.Models.WorkHistoryViewModel;
using PortalTemplate.Areas.CredAxisProduct.Models.ProviderProfileViewModel.WorkHistoryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CredAxis.Services.IServices
{
    public interface IWorkHistoryService
    {
        //WorkHistoryMainViewModel GetAllWorkHistory(int ProfileId);
        ////WorkHistoryMainViewModel AddEditWorkHistory(WorkHistoryMainViewModel workHistoryMainViewModel);  
        //ProfessionalWorkExperienceViewModel AddEditWorkHistory(ProfessionalWorkExperienceViewModel workHistoryMainViewModel, int ProfileId,int workHistoryId);
        //MilitaryServiceViewModel AddEditMilitaryService(MilitaryServiceViewModel militaryServiceViewModel, int ProfileId, int MilitaryServiceId);
        //PublicHealthServicesViewModel AddEditPublicHealthService(PublicHealthServicesViewModel publicHealthServiceViewModel, int ProfileId, int PublicHealthServiceId);
        //WorkGapViewModel AddEditWorkGap(WorkGapViewModel workGapViewModel, int ProfileId, int WorkGapId);
        WorkHistoryMainViewModel GetAllWorkHistory();
        WorkHistoryMainViewModel AddEditWorkHistory(WorkHistoryMainViewModel workHistoryMainViewModel);  
    }
}
