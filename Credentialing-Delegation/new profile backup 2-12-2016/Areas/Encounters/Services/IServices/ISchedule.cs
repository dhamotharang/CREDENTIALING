using PortalTemplate.Areas.Encounters.Models;
using PortalTemplate.Areas.Encounters.Models.Schedule;
using PortalTemplate.Areas.Encounters.Models.ScheduleCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Encounters.Services.IServices
{
    public interface ISchedule
    {
        List<ProviderViewModel> GetProviderResultList(string ProviderSearchParameter);
        List<MemberViewModel> GetMemberResultList();
        ProviderSelectMemberViewModel GerProviderSelectMembers(string ProviderId);
        EncounterViewModel CreateEncounterDetails();
        ViewScheduleViewModel GetScheduleView();
        EditScheduleViewModel GetScheduleEdit();
    }
}
