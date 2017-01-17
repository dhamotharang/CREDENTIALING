using PortalTemplate.Areas.Facility.Models;
using PortalTemplate.Areas.Facility.Models.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Facility.Services.IServices
{
    public interface IFacilityService
    {
        List<FacilityBridgeQueueViewModel> GetOpenedQueueData();
        List<FacilityBridgeQueueViewModel> GetAssignedQueueData();
        List<FacilityBridgeQueueViewModel> GetPendingQueueData();
        List<FacilityBridgeQueueViewModel> GetApprovedQueueData();
        List<FacilityBridgeQueueViewModel> GetAllFacilities();
        FacilityBridgeQueueViewModel GetQueueData(string id);
        int GetApprovedQueueDataCount();
        int GetRequestedQueueDataCount();
        int GetOpenedQueueDataCount();
        int GetAssignedQueueDataCount();
        int GetPendingQueueDataCount();
        string GetAllFacilityData();
        string GetAllCountries(string searchTerm);
        string GetAllCities(string searchTerm);
        string GetAllStates(string searchTerm);
        string GetAllCounties(string searchTerm);
        string GetAllOrganizations(string searchTerm);
    }
}
