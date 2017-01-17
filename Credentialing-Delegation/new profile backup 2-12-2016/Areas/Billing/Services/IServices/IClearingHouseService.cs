using PortalTemplate.Areas.Billing.Models.Clearing_House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Billing.Services.IServices
{
    public interface IClearingHouseService
    {
        List<ClearingHouseViewModel> GetClearingHouseList();

        ClearingHouseViewModel AddClearingHouse();

        ClearingHouseViewModel ViewClearingHouse(string ClearingHouseId);

        ClearingHouseViewModel EditClearingHouse(string ClearingHouseId);
    }
}
