using PortalTemplate.Areas.Encounters.Models.SchedulerDTO;
using PortalTemplate.Areas.Encounters.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Services
{
    public class SchedulerEvents : ISchedulerEvents
    {
        private static List<CalendarEventDTO> AllEvents = new List<CalendarEventDTO>{
            new CalendarEventDTO { Status="Active", StartTime = new DateTime(2016, 10, 27, 9, 0, 0), EndTime = new DateTime(2016, 10, 27, 10, 30, 0), 
                                   Description = new CalendarEventDescription(){ MemberName="Carol Adams", ProviderName="Nishat Seema", Facility = "5344 Spring Hill Drive", ChiefComplaint = "Fussiness" } 
            },
            new CalendarEventDTO { Status="NoShow", StartTime = new DateTime(2016, 10, 27, 10, 0, 0), EndTime = new DateTime(2016, 10, 27, 10, 15, 0), 
                                   Description = new CalendarEventDescription(){ MemberName="Alicea Reina", ProviderName="Nishat Seema", Facility = "12678 Frank Dr North", ChiefComplaint = "Congestion" } 
            },
            new CalendarEventDTO { Status="Rescheduled", StartTime = new DateTime(2016, 10, 27, 9, 20, 0), EndTime = new DateTime(2016, 10, 27, 9, 25, 0), 
                                   Description = new CalendarEventDescription(){ MemberName="Barnard Robert", ProviderName="Deam David", Facility = "10794 108TH ST", ChiefComplaint = "Ear Pain Bilateral" } 
            },
            new CalendarEventDTO { Status="Open", StartTime = new DateTime(2016, 10, 27, 11, 2, 0), EndTime = new DateTime(2016, 10, 27, 11, 45, 0), 
                                   Description = new CalendarEventDescription(){ MemberName="Barnard Barbara", ProviderName="Masood Asif", Facility = "14555 Cortez Blvd", ChiefComplaint = "Diabetes" } 
            },
            new CalendarEventDTO { Status="Draft", StartTime = new DateTime(2016, 10, 27, 13, 0, 0), EndTime = new DateTime(2016, 10, 27, 14, 0, 0), 
                                   Description = new CalendarEventDescription(){ MemberName="Boucher Bash", ProviderName="Deam David", Facility = "1517 GREENBRIAR VILLA CIR", ChiefComplaint = "Skin Problems" } 
            },
            new CalendarEventDTO { Status="NoShow", StartTime = new DateTime(2016, 10, 27, 14, 16, 0), EndTime = new DateTime(2016, 10, 27, 14, 30, 0), 
                                   Description = new CalendarEventDescription(){ MemberName="Janet Satterfield Jr.", ProviderName="Masood Asif", Facility = "10794 108TH ST", ChiefComplaint = "Congestion" } 
            },
            new CalendarEventDTO { Status="ReadyToCode", StartTime = new DateTime(2016, 10, 27, 14, 50, 0), EndTime = new DateTime(2016, 10, 27, 15, 0, 0), 
                                   Description = new CalendarEventDescription(){ MemberName="Clair John", ProviderName="Masood Asif", Facility = "5340 Spring Hill Drive", ChiefComplaint = "Liver Disease" } 
            }
        };
        List<Models.SchedulerDTO.CalendarEventDTO> ISchedulerEvents.GetAllSchedules()
        {
            return AllEvents;
        }


        public List<CalendarEventDTO> GetSchedulesForDay(int year, int month, int day)
        {
            List<CalendarEventDTO> dayEvents = new List<CalendarEventDTO>();
            foreach (var Event in AllEvents)
            {
                if (Event.StartTime.Year == year
                    && Event.StartTime.Month == month
                    && Event.StartTime.Day == day)

                    dayEvents.Add(Event);
            }
            return dayEvents;
        }

        public List<CalendarEventDTO> GetSchedulesForMonth(int year, int month)
        {
            List<CalendarEventDTO> monthEvents = new List<CalendarEventDTO>();
            foreach (var Event in AllEvents)
            {
                if (Event.StartTime.Year == year && Event.StartTime.Month == month)
                    monthEvents.Add(Event);
            }

            return monthEvents;
        }

        public List<CalendarEventDTO> GetSchedulesForYear(int year)
        {
            List<CalendarEventDTO> yearEvents = new List<CalendarEventDTO>();
            foreach (var Event in AllEvents)
            {
                if (Event.StartTime.Year == year)
                    yearEvents.Add(Event);
            }

            return yearEvents;
        }
    }
}