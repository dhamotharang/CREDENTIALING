using PortalTemplate.Areas.Encounters.Models;
using PortalTemplate.Areas.Encounters.Models.SchedulerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Services.IServices
{
    public interface ISchedulerEvents
    {
        List<CalendarEventDTO> GetAllSchedules();
        List<CalendarEventDTO> GetSchedulesForDay(int year, int month, int day);
        List<CalendarEventDTO> GetSchedulesForMonth(int year, int month);
        List<CalendarEventDTO> GetSchedulesForYear(int year);
    }
}