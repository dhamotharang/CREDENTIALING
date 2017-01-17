using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Portal.IServices.Facility
{
    public interface IFacilityService
    {
        List<FacilityViewModel> GetAllFacility();

        List<FacilityViewModel> SearchFacility(string searchTerm, int limit);
    }
}
