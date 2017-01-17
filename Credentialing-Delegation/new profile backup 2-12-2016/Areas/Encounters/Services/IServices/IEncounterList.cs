using PortalTemplate.Areas.Encounters.Models;
using PortalTemplate.Areas.Encounters.Models.EncounterList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Encounters.Services.IServices
{
    public interface IEncounterList
    {
        List<ScheduleListViewModel> GetScheduleEncounterList();
        List<ScheduleListViewModel> GetScheduleList();
        List<ScheduleListViewModel> GetActiveEncounterList();
        List<ScheduleListViewModel> GetOpenEncounterList();
        List<EncounterListViewModel> GetDraftEncounterList();
        List<EncounterListViewModel> GetClosedEncounterList();
        List<EncounterListViewModel> GetEncounterClosedList();
        List<EncounterRejectionListViewModel> GetInactiveEncounterList();
        List<EncounterRejectionListViewModel> GetRejectedEncounterList();
        List<EncounterRejectionListViewModel> GetEncounterRejectedList();
        EncounterListCountViewModel GetAllListCounts();
    }
}
