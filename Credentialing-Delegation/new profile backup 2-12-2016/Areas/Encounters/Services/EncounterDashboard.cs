using PortalTemplate.Areas.Encounters.Models.Dashboard;
using PortalTemplate.Areas.Encounters.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Services
{
    public class EncounterDashboard : IEncounterDashboard
    {
        public EncounterDashboardViewModel GetEncounterDashboard()
        {
            EncounterDashboardViewModel EncounterDashboardViewModel = new EncounterDashboardViewModel();
            EnounterBiscuitViewModel EncounterBiscuits = new EnounterBiscuitViewModel();

            EncounterBiscuits.Scheduled = 60;
            EncounterBiscuits.Active = 60;
            EncounterBiscuits.NoShow = 8;
            EncounterBiscuits.ReSchedule = 12;
            EncounterBiscuits.Open = 40;
            EncounterBiscuits.Draft = 7;
            EncounterBiscuits.ReadyToCode = 33;
            EncounterBiscuits.OnHold = 16;



            List<ReasonsForOnHoldEncountersViewModel> ReasonsForOnHoldEncountersList = new List<ReasonsForOnHoldEncountersViewModel>();
            ReasonsForOnHoldEncountersList.Add(new ReasonsForOnHoldEncountersViewModel { Reason = "SIGNATURE MISSING", Count = 78 });
            ReasonsForOnHoldEncountersList.Add(new ReasonsForOnHoldEncountersViewModel { Reason = "SOCIAL HISTORY", Count = 46 });
            ReasonsForOnHoldEncountersList.Add(new ReasonsForOnHoldEncountersViewModel { Reason = "HISTORY OF PATIENT ILLNESS", Count = 25 });
            ReasonsForOnHoldEncountersList.Add(new ReasonsForOnHoldEncountersViewModel { Reason = "INCOMPLETE NOTES", Count = 13 });
            ReasonsForOnHoldEncountersList.Add(new ReasonsForOnHoldEncountersViewModel { Reason = "CHIEF COMPLAINT", Count = 12 });
            ReasonsForOnHoldEncountersList.Add(new ReasonsForOnHoldEncountersViewModel { Reason = "AGREED", Count = 11 });
            ReasonsForOnHoldEncountersList.Add(new ReasonsForOnHoldEncountersViewModel { Reason = "REVIEW OF SYSTEMS", Count = 9 });
            ReasonsForOnHoldEncountersList.Add(new ReasonsForOnHoldEncountersViewModel { Reason = "PAST, FAMILY AND/OR SOCIAL HISTORY", Count = 7 });
            ReasonsForOnHoldEncountersList.Add(new ReasonsForOnHoldEncountersViewModel { Reason = "HISTORY REFERENCES", Count = 5 });
            ReasonsForOnHoldEncountersList.Add(new ReasonsForOnHoldEncountersViewModel { Reason = "MEDICAL DECISION MAKING LEVEL", Count = 4 });
            ReasonsForOnHoldEncountersList.Add(new ReasonsForOnHoldEncountersViewModel { Reason = "OTHERS	", Count = 2 });

            EncounterDashboardViewModel.EnounterBiscuitViewModel = EncounterBiscuits;
            EncounterDashboardViewModel.ReasonsForOnHoldEncountersViewModel = ReasonsForOnHoldEncountersList;
            return EncounterDashboardViewModel;
        }
    }
}